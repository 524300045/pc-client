using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public partial class WeightForm : TabWindow
    {
        private long packId;

        private string taskCode;

        private long curTaskDetailId = 0;

        private string curOutStockCode;

        private string curSkuCode;

        private decimal orderCount;

        private int standNum;

        private string twoCategoryCode;


        private string processDes;

        private int orderNum;

        private string curStoreName;

        private string curGoodsName;

        private string curUnit;

        private string curPackageCode;

        private PackTaskDetail curPackTaskDetail;


        private decimal curWeight;
       private    decimal downWeight;
       decimal upWeight;

        private IWMSClient client = null;

        public string wareHouseName;

        public string topCategoryCode;

        public WeightForm()
        {
            InitializeComponent();
        }

        public WeightForm(long _packId, string _taskCode, decimal _orderCount, int _standNum, string _processDes, int _orderNum)
        {
            InitializeComponent();
            this.packId = _packId;
            this.taskCode = _taskCode;
            this.orderCount = _orderCount;
            standNum = _standNum;
            this.processDes = _processDes;
            this.orderNum = _orderNum;
            client = new DefalutWMSClient();
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void WeightForm_Load(object sender, EventArgs e)
        {
            ClearForm();

            ShowDetail();
            tbWeight.Focus();
        }

        /// <summary>
        /// 显示商品明细
        /// </summary>
        private void ShowDetail()
        {
            //获取可用的任务
            PackTaskCodeRequest request = new PackTaskCodeRequest();
            request.packTaskCode = taskCode;
            request.customerCode = UserInfo.CustomerCode;
            PackTaskDetailResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {

                    ClearForm();
                    curPackTaskDetail = response.result;
                    lbSkuInfo.Text = response.result.skuCode + "  " + response.result.goodsName + "  " + response.result.modelNum + response.result.goodsUnit + "/" + response.result.physicsUnit;

                    lbProcess.Text = response.result.finishNum + "/" + orderNum + "  " + (response.result.finishNum / orderNum).ToString() + "%";
                    lbStore.Text = response.result.storedName ;
                    lbOrderWeight.Text = orderCount.ToString("f0") + response.result.goodsUnit;//订单总量
                    lbStorePlanNumKe.Text = Util.ConvertJinToG(response.result.planNum).ToString("f0")+"克";//门店需求克数
                    //门店需求量
                    lbStoreWeight.Text = response.result.packageWeight+ response.result.goodsUnit+"/"+response.result.planNum + response.result.goodsUnit;


                     downWeight = response.result.modelNum - (response.result.downPlanNum * response.result.modelNum)/100;
                     upWeight = response.result.modelNum + (response.result.upPlanNum * response.result.modelNum)/100;


                    lbUpDown.Text = downWeight.ToString("f2") + response.result.goodsUnit + "--" + upWeight.ToString("f2") + response.result.goodsUnit;//上下限
                    lbPackNUM.Text = standNum.ToString();//标注包数
                    curTaskDetailId = response.result.id;//当前任务明细ID
                    curOutStockCode = response.result.outboundTaskCode;
                    curSkuCode = response.result.skuCode;

                    curStoreName = response.result.storedName;
                    curGoodsName = response.result.goodsName;
                    curUnit = response.result.goodsUnit;

                    twoCategoryCode = response.result.twoCategoryCode;
                    topCategoryCode = response.result.categoryCode;
                }
                else
                {
                    curPackTaskDetail = null;
                    MessageBox.Show("当前任务已经完成!");
                    ClearForm();
                    curTaskDetailId = 0;
                    curOutStockCode = "";
                    curSkuCode = "";
                    ClearForm();
                    this.DialogResult = DialogResult.OK;
                }
            }

        }

        private void ClearForm()
        {
            lbSkuInfo.Text = "";
            lbProcess.Text = "";
            lbStore.Text = "";
            lbOrderWeight.Text = "";
            lbUpDown.Text = "";
            lbPackNUM.Text = "";
            lbStoreWeight.Text = "";
        }

        private void tbWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(tbWeight.Text.Trim()))
                    {
                        MessageBox.Show("请录入重量!");
                        tbWeight.Focus();
                        return;
                    }

                    decimal weight = 0;
                    decimal.TryParse(tbWeight.Text.Trim(), out weight);
                    if (weight <= 0)
                    {
                        MessageBox.Show("录入重量必须大于0!");
                        tbWeight.Focus();
                        tbWeight.SelectAll();
                        return;
                    }

                    curWeight = Util.ConvertGToJin(weight);

                    //if (curWeight<downWeight)
                    //{
                    //    MessageBox.Show("重量不能小于浮动下限");
                    //    return;
                    //}

                    //if (curWeight>upWeight)
                    //{
                    //      MessageBox.Show("重量不能大于浮动上限");
                    //    return;
                    //}


                    tbWeight.Enabled = false;

                    PackageRequest request = new PackageRequest();
                    request.packTaskDetailId = curTaskDetailId;
                    request.processUser = UserInfo.RealName;
                  
                    request.weight = curWeight;
                    request.packTaskCode = taskCode;
                    request.outboundTaskCode = curOutStockCode;
                    request.skuCode = curSkuCode;
                    request.createUser = UserInfo.RealName;
                    request.updateUser = UserInfo.RealName;
                    request.partnerCode = UserInfo.PartnerCode;
                    request.partnerName = UserInfo.PartnerName;



                    PackageResponse response = client.Execute(request);
                    if (!response.IsError)
                    {
                        curPackageCode = response.result;
                        PrintDocument document = new PrintDocument();
                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        dialog.Document = document;
#else

                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        dialog.Document = document;

#endif
                        try
                        {
                            document.Print();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("打印异常" + exception);
                            document.PrintController.OnEndPrint(document, new PrintEventArgs());
                        }
                        //打印，加载下一个
                        //ClearForm();
                        tbWeight.Text = "";
                        ShowDetail();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tbWeight.Enabled = true;
                    tbWeight.Focus();
                }

            }
        }

        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {
            //   if (topCategoryCode == "10" || topCategoryCode == "11" || topCategoryCode == "12" || topCategoryCode == "13" || topCategoryCode == "17" || topCategoryCode == "20")
            if (topCategoryCode == "10")
            {
                Font fontCu = new Font("宋体", 12f, FontStyle.Bold);
                int height = 15;
                int heightRight = 15;

                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                int interval = 5;
                int pointX = 35;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(200, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                heightRight = image.Width - 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(wareHouseName, font, brush, layoutRectangleRight);

                heightRight += 20;

                // layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight); 


                heightRight += 15;
                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                // g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);

                RectangleF layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(lbStore.Text, fontCu, brush, layoutRectangle);

                height += 10;
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 180f, 30f);
                g.Graphics.DrawString(curPackTaskDetail.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height - 10, 120f, 40f);
                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                height += interval + 13;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 80 + image.Height - 15;
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(curPackageCode, font, brush, layoutRectangle);

                //流通号
                layoutRectangleRight = new RectangleF(pointX, 60, 300f, 85f);
                g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产厂家
                layoutRectangleRight = new RectangleF(pointX, 75, 300f, 85f);
                g.Graphics.DrawString("厂家:" + UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产日期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 110, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString().ToString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                //保质期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 130, 300f, 85f);
                double day = 0;
                if (curPackTaskDetail!=null)
                {
                    day = curPackTaskDetail.expiryDate;
                }
                g.Graphics.DrawString("保 质 期: "+day+"天", new Font("宋体", 10f), brush, layoutRectangleRight);

            }
            else
            {



                Font fontCu = new Font("宋体", 12f, FontStyle.Bold);
                int height = 15;
                int heightRight = 15;

                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                int interval = 5;
                int pointX = 35;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(200, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                heightRight = image.Width - 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(wareHouseName, font, brush, layoutRectangleRight);

                heightRight += 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight);


                heightRight += 15;
                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                // g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);

                RectangleF layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(lbStore.Text, fontCu, brush, layoutRectangle);

                height += 10;
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 180f, 30f);
                g.Graphics.DrawString(curPackTaskDetail.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 120f, 40f);
                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                height += interval + 13;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 80 + image.Height - 15;
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(curPackageCode, font, brush, layoutRectangle);

            }
        }



       
        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curPackageCode);
            GetPrintPicture(bt, e);
        }

        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 80,
                Height = 80
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }

        private void WeightForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}

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
        private decimal downWeight;
        decimal upWeight;

        private IWMSClient client = null;

        public string wareHouseName;

        public string topCategoryCode;

        DateTime dt;

        string curProductCurWorkshopAttrDesc;

        public WeightForm()
        {
            InitializeComponent();
        }

        public WeightForm(long _packId, string _taskCode, decimal _orderCount, int _standNum, string _processDes, int _orderNum, string curWorkDesc)
        {
            InitializeComponent();
            this.packId = _packId;
            this.taskCode = _taskCode;
            this.orderCount = _orderCount;
            standNum = _standNum;
            this.processDes = _processDes;
            this.orderNum = _orderNum;
            curProductCurWorkshopAttrDesc = curWorkDesc;
            client = new DefalutWMSClient();
        }

        public WeightForm(long _packId, string _taskCode, decimal _orderCount, int _standNum, string _processDes, int _orderNum, DateTime _dt, string curWorkshopAttrDesc)
        {
            InitializeComponent();
            this.packId = _packId;
            this.taskCode = _taskCode;
            this.orderCount = _orderCount;
            standNum = _standNum;
            this.processDes = _processDes;
            this.orderNum = _orderNum;
            this.dt = _dt;
            curProductCurWorkshopAttrDesc = curWorkshopAttrDesc;
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
                    lbStore.Text = response.result.storedName;
                    lbOrderWeight.Text = orderCount.ToString("f0") + response.result.goodsUnit;//订单总量
                    lbStorePlanNumKe.Text = Util.ConvertJinToG(response.result.planNum).ToString("f0") + "克";//门店需求克数
                    //门店需求量
                    lbStoreWeight.Text = response.result.packageWeight + response.result.goodsUnit + "/" + response.result.planNum + response.result.goodsUnit;


                    downWeight = response.result.modelNum - (response.result.downPlanNum * response.result.modelNum) / 100;
                    upWeight = response.result.modelNum + (response.result.upPlanNum * response.result.modelNum) / 100;


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

                    if (UserInfo.CustomerCode == "7001")
                    {
                        #region 西贝处理

                        PackageXiBeiRequest request = new PackageXiBeiRequest();
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
                        request.goodsName = curGoodsName;

                        PackageResponse response = client.Execute(request);
                        if (!response.IsError)
                        {

                            curPackageCode = response.result;
                            PrintDocument document = new PrintDocument();
                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                            document.OriginAtMargins = true;
                            document.DefaultPageSettings.Landscape = false;
                            //  int leftMargin = document.DefaultPageSettings.Margins.Left;

                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);

#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintXiBeiPage);
                        dialog.Document = document;
#else

                            PrintPreviewDialog dialog = new PrintPreviewDialog();
                            document.PrintPage += new PrintPageEventHandler(this.pd_PrintXiBeiPage);
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
                        else
                        {
                            MessageBox.Show(response.Message);
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        #region 非西贝

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
                        request.goodsName = curGoodsName;


                        PackageResponse response = client.Execute(request);
                        if (!response.IsError)
                        {
                            curPackageCode = response.result;
                            PrintDocument document = new PrintDocument();
                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                            document.OriginAtMargins = true;
                            document.DefaultPageSettings.Landscape = false;
                            //  int leftMargin = document.DefaultPageSettings.Margins.Left;

                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);

                            if (UserInfo.CustomerCode == "15001" || UserInfo.CustomerCode == "11001"
                                || UserInfo.CustomerCode == "18001" || UserInfo.CustomerCode == "19001"
                                     || UserInfo.CustomerCode == "22001" || UserInfo.CustomerCode == "31001" || UserInfo.CustomerCode == "32001"
                                 || UserInfo.CustomerCode == "37001" || UserInfo.CustomerCode == "25001"
                                )
                            {
#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
                        dialog.Document = document;
#else

                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
                                dialog.Document = document;

#endif
                            }
                            else if (UserInfo.CustomerCode == "23001")
                            {
                                #region 嘉和一品

#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintDatePage);
                        dialog.Document = document;
#else

                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintDatePage);
                                dialog.Document = document;

#endif

                                #endregion
                            }
                            else if (UserInfo.CustomerCode == "30001")
                            {
                                #region 汪洋

#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintWangYangPage);
                        dialog.Document = document;
#else

                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintWangYangPage);
                                dialog.Document = document;

#endif
                                #endregion
                            }
                            else if (UserInfo.CustomerCode == "34001" || UserInfo.CustomerCode == "21001")
                            {
                                #region 云海肴 黄记煌
#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintYHYPage);
                        dialog.Document = document;
#else

                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintYHYPage);
                                dialog.Document = document;

#endif

                                #endregion
                            }
                            else
                            {
#if(!DEBUG)
                        PrintDialog dialog = new PrintDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                        dialog.Document = document;
#else

                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                                dialog.Document = document;

#endif
                            }


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

                        #endregion

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



        public void GetPrintXiBeiPicture(Bitmap image, PrintPageEventArgs g)
        {


            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = 5;


            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = 5;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            if (curPackTaskDetail.handleWay != null && curPackTaskDetail.handleWay.Trim() != "")
            {
                Pen pen = new Pen(Color.Black, 1);
                g.Graphics.DrawEllipse(pen, 5, 2, 25, 25);
                g.Graphics.DrawString(curPackTaskDetail.handleWay.Trim(), new Font("宋体", 12f, FontStyle.Bold), brush, 7, 7);
            }



            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
            g.Graphics.DrawString("   " + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

            height += 20;
            //重量

            layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

            g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



            height += 15;


            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

            height += 15;
            //生产日期
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

            double day = 0;
            if (curPackTaskDetail != null)
            {
                day = curPackTaskDetail.expiryDate;

            }
            height += 15;
            if (curPackTaskDetail.handleWay != null && curPackTaskDetail.handleWay.Trim() != "" && curPackTaskDetail.handleWay == "净")
            {

                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("赏味期至:" + DateTime.Now.AddDays(day - 1).ToShortDateString() + "", new Font("宋体", 10f), brush, layoutRectangleRight);

            }

            height += 15;
            //门店
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

            height += 20;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
            if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
            {
                g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
            }


            if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
            {
                if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                {
                    height += 15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                }
            }

            height += 20;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);


        }

        public void GetPrintYHYPicture(Bitmap image, PrintPageEventArgs g)
        {


            if (topCategoryCode == "10")
            {
                #region 分类为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.customerGoodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量
                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 15;
                //门店
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 8f), brush, layoutRectangleRight);

                double day = 0;
                if (curPackTaskDetail != null)
                {
                    day = curPackTaskDetail.expiryDate;

                }


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);

                height += 15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + this.dt.ToShortDateString() + "保质期至:" + this.dt.AddDays(day).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);

              
                //height += 15;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("保质期至:" + DateTime.Now.AddDays(day).ToShortDateString(), new Font("宋体", 8f), brush, layoutRectangleRight);

            

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
         
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);

         


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);

                #endregion
            }
            else
            {
                #region 分类不为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.customerGoodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("电话:010-89958567", new Font("宋体", 8f), brush, layoutRectangleRight);



                height +=15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 8f), brush, layoutRectangleRight);
               
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    height +=15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);

             

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("地址:北京平谷区马坊镇英城南桥东400米", new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }


        }

        public void GetPrintQingPicture(Bitmap image, PrintPageEventArgs g)
        {


            if (topCategoryCode == "10")
            {
                #region 分类为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                double day = 0;
                if (curPackTaskDetail != null)
                {
                    day = curPackTaskDetail.expiryDate;

                }
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + day + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //门店
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }
            else
            {
                #region 分类不为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

                //double day = 0;
                //if (curPackTaskDetail != null)
                //{
                //    day = curPackTaskDetail.expiryDate;

                //}
                //height += 20;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("保质期:" + day + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 40;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //   g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }


        }


        public void GetPrintDatePicture(Bitmap image, PrintPageEventArgs g)
        {


            if (topCategoryCode == "10")
            {
                #region 分类为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + this.dt.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                double day = 0;
                if (curPackTaskDetail != null)
                {
                    day = curPackTaskDetail.expiryDate;

                }
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + day + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //门店
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商：" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }
            else
            {
                #region 分类不为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);



                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

                //double day = 0;
                //if (curPackTaskDetail != null)
                //{
                //    day = curPackTaskDetail.expiryDate;

                //}
                //height += 20;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("保质期:" + day + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 40;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //   g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }


        }


        public void GetPrintWangYangPicture(Bitmap image, PrintPageEventArgs g)
        {


            if (topCategoryCode == "10")
            {
                #region 分类为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString((curWeight/2).ToString("f2") + "KG", fontCu, brush, layoutRectangle);



                height += 15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + this.dt.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                double day = 0;
                if (curPackTaskDetail != null)
                {
                    day = curPackTaskDetail.expiryDate;

                }
                //height += 15;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("保质期:" + day + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //门店
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
               
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
            }
            else
            {
                #region 分类不为10
                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 5;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -10, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 165f, 30f);
                g.Graphics.DrawString("" + curPackTaskDetail.shortName, fontCu11, brush, layoutRectangle);

                height += 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                g.Graphics.DrawString((curWeight/2).ToString("f2") + "KG", fontCu, brush, layoutRectangle);



                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(lbStore.Text, new Font("宋体", 10f), brush, layoutRectangleRight);

             

                height += 40;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
               
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        height += 15;
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("经销商:" + (string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName), new Font("宋体", 8f), brush, layoutRectangleRight);
                #endregion
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
                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
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
                g.Graphics.DrawString(curPackTaskDetail.shortName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height - 10, 120f, 40f);
                g.Graphics.DrawString(curWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                height += interval + 13;

                Rectangle dest2Rect = new Rectangle(pointX, 85, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                //Font fontCode = new Font("宋体", 8f);

                //height = 80 + image.Height - 10;
                //layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                //g.Graphics.DrawString(curPackageCode, fontCode, brush, layoutRectangle);

                //流通号
                layoutRectangleRight = new RectangleF(pointX, 60, 300f, 85f);
                //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号:" + UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                layoutRectangleRight = new RectangleF(pointX, 72, 300f, 85f);
                g.Graphics.DrawString("经销商:" + UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);


                if (curProductCurWorkshopAttrDesc != null && curProductCurWorkshopAttrDesc != "")
                {
                    if (curProductCurWorkshopAttrDesc == "三河车间" || curProductCurWorkshopAttrDesc == "腌菜车间")
                    {
                        layoutRectangleRight = new RectangleF(pointX, 83, 300f, 85f);
                        g.Graphics.DrawString("生产商:三河市鲜洁农产品有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }

                //货主
                layoutRectangleRight = new RectangleF(pointX + image.Width, 96, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);


                //生产日期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 106, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString().ToString(), new Font("宋体", 8f), brush, layoutRectangleRight);

                //保质期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 116, 300f, 85f);
                double day = 0;
                if (curPackTaskDetail != null)
                {
                    day = curPackTaskDetail.expiryDate;
                }
                g.Graphics.DrawString("保 质 期: " + day + "天", new Font("宋体", 8f), brush, layoutRectangleRight);

                Font fontCode = new Font("宋体", 8f);
                layoutRectangle = new RectangleF(pointX + image.Width, 126, 150f, 30f);
                g.Graphics.DrawString(curPackageCode, fontCode, brush, layoutRectangle);

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
                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                heightRight = image.Width - 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(wareHouseName, font, brush, layoutRectangleRight);

                heightRight += 20;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight);

                heightRight += 20;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, font, brush, layoutRectangleRight);

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

                Font fontCode = new Font("宋体", 8f);

                height = 80 + image.Height - 15;
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(curPackageCode, fontCode, brush, layoutRectangle);

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


        private void pd_PrintQingPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPackageCode);
            GetPrintQingPicture(bt, e);
        }

        private void pd_PrintXiBeiPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPackageCode);
            GetPrintXiBeiPicture(bt, e);
        }


        private void pd_PrintYHYPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = Create110QRCode(curPackageCode);
            GetPrintYHYPicture(bt, e);
        }


        private void pd_PrintDatePage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPackageCode);
            GetPrintDatePicture(bt, e);
        }

        private void pd_PrintWangYangPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPackageCode);
            GetPrintWangYangPicture(bt, e);
        }

        public static Bitmap CreateQingQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 140,
                Height = 140
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }

        public static Bitmap Create80QRCode(string asset)
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

        public static Bitmap Create110QRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width =110,
                Height =110
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

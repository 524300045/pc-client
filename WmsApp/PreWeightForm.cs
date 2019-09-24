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
    public partial class PreWeightForm : TabWindow
    {
        public PreWeightForm()
        {
            InitializeComponent();
        }
        private IWMSClient client = null;


        private decimal curWeight = 0;

        private Goods goods;

        private List<PreprocessInfo> preprocessInfoList;

        private PreprocessInfo curPreprocessInfo;

        public string orderNum;
        public DateTime dt;



        public PreWeightForm(Goods _goods, DateTime _dt)
        {
            InitializeComponent();
            this.goods = _goods;
            client = new DefalutWMSClient();
            this.dt = _dt;

        }

        double expireDay = 0;
        string handleWay = "";
        private void getCustomerInfo(string skuCode)
        {
            CustomerGoodsBySkuRequest request = new CustomerGoodsBySkuRequest();
            request.customerCode = UserInfo.CustomerCode;
            request.skuCode = skuCode;
            CustomerGoodsBySkuResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    expireDay = response.result.expiryDate;
                    handleWay = response.result.handleWay;
                }
            }
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void pd_PrintQingPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPreprocessInfo.preprocessCode);
            GetPrintQingPicture(bt, e, curPreprocessInfo);
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

        public void GetPrintQingPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            if (goods.categoryCode == "10")
            {

                #region 分类为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + expireDay + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
              //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号："+UserInfo.foodLicenseNo, new Font("宋体", 10f), brush, layoutRectangleRight);
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }
              

                #endregion
            }
            else
            {

                #region 分类不为10


                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                //height += 20;
                ////生产日期
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                //height += 20;
                //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //g.Graphics.DrawString("保质期:" + expireDay + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 60;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
              //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号："+UserInfo.foodLicenseNo, new Font("宋体", 10f), brush, layoutRectangleRight);
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }
              

                #endregion
            }
        }

        private void WeightForm_Load(object sender, EventArgs e)
        {
            lbBiaoQian.Visible = false;
            tbBiaoQianNum.Visible = false;

            lbOrderNum.Text = orderNum;
            GetCount();
            if (goods.weighed == 1)
            {
                //称重
                lbWeight.Visible = true;
                tbUnit.Visible = true;

                lbBiaoQian.Visible = true;
                tbBiaoQianNum.Visible = true;
            }
            else
            {
                //标准包裹数
                lbStandPackage.Visible = true;
            }
            lbSkuInfo.Text = goods.skuCode + "  " + goods.goodsName + "  " + goods.modelNum + goods.goodsUnit + "/" + goods.physicsUnit;

            decimal downWeight = goods.modelNum - (goods.downLimit * goods.modelNum) / 100;
            decimal upWeight = goods.modelNum + (goods.upLimit * goods.modelNum) / 100;

            lbUpDown.Text = downWeight.ToString("f2") + goods.goodsUnit + "--" + upWeight.ToString("f2") + goods.goodsUnit;
            lbModelNum.Text = goods.modelNum + goods.goodsUnit + "/" + goods.physicsUnit;
            tbWeight.Focus();
            getCustomerInfo(goods.skuCode);
        }

        private void GetCount()
        {
            PreprocessInfoCountRequest request = new PreprocessInfoCountRequest();
            //   request.status = 0;
            request.skuCode = goods.skuCode;
            request.startTime = dt.AddDays(-1).ToString("yyyy-MM-dd 06:00:00");
            request.endTime = dt.ToString("yyyy-MM-dd 06:00:00");
            request.partnerCode = UserInfo.PartnerCode;

            PreprocessInfoCountResponse response = client.Execute(request);
            if (!response.IsError)
            {
                lbCount.Text = response.result.ToString();
            }
        }

        private void tbWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                try
                {
                    tbWeight.Enabled = false;
                    List<PreprocessInfoAdd> list = new List<PreprocessInfoAdd>();

                    if (goods.weighed == 1)
                    {
                        #region 称重


                        if (string.IsNullOrWhiteSpace(tbWeight.Text.Trim()))
                        {
                            MessageBox.Show("请录入重量!");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
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

                        int printNum = 0;
                        if (string.IsNullOrWhiteSpace(tbBiaoQianNum.Text.Trim()))
                        {
                            MessageBox.Show("标签数量不能为空!");
                            tbBiaoQianNum.Focus();
                            tbBiaoQianNum.SelectAll();
                            return;
                        }
                        int.TryParse(tbBiaoQianNum.Text.Trim(), out printNum);

                        if (printNum <= 0)
                        {
                            MessageBox.Show("输入的标签数量必须大于0");
                            tbBiaoQianNum.Focus();
                            tbBiaoQianNum.SelectAll();
                            return;
                        }

                        if (printNum > 50)
                        {
                            MessageBox.Show("输入的标签数量最多不能大于50");
                            tbBiaoQianNum.Focus();
                            tbBiaoQianNum.SelectAll();
                            return;
                        }

                        curWeight = Util.ConvertGToJin(weight);

                        if (UserInfo.CustomerCode == "7001")
                        {
                            curWeight = weight / 1000;
                        }

                        decimal downWeight = goods.modelNum - (goods.downLimit * goods.modelNum) / 100;
                        decimal upWeight = goods.modelNum + (goods.upLimit * goods.modelNum) / 100;
                        if (curWeight < downWeight)
                        {
                            MessageBox.Show("商品重量小于下限");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
                            return;
                        }

                        if (curWeight > upWeight)
                        {
                            MessageBox.Show("重量不能大于上限");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
                            return;
                        }

                        for (int i = 0; i < printNum; i++)
                        {
                            PreprocessInfoAdd add = new PreprocessInfoAdd();
                            add.createUser = UserInfo.RealName;
                            add.goodsName = goods.shortName;
                            add.goodsUnit = goods.goodsUnit;
                            add.modelNum = goods.modelNum;
                            add.operateUser = UserInfo.RealName;

                            add.packWeight = curWeight;
                            add.partnerCode = UserInfo.PartnerCode;
                            add.partnerName = UserInfo.PartnerName;
                            add.physicsUnit = goods.physicsUnit;
                            add.skuCode = goods.skuCode;
                            add.status = 0;
                            add.updateUser = UserInfo.RealName;
                            list.Add(add);
                        }


                        #endregion
                    }
                    else
                    {
                        #region 包裹数

                        if (string.IsNullOrWhiteSpace(tbWeight.Text.Trim()))
                        {
                            MessageBox.Show("请录入包裹数!");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
                            return;
                        }

                        int weight = 0;
                        int.TryParse(tbWeight.Text.Trim(), out weight);
                        if (weight <= 0)
                        {
                            MessageBox.Show("录入包裹数必须大于0!");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
                            return;
                        }
                        if (weight > 20)
                        {
                            MessageBox.Show("录入包裹数不能大于20!");
                            tbWeight.Focus();
                            tbWeight.SelectAll();
                            return;
                        }

                        for (int i = 0; i < weight; i++)
                        {
                            PreprocessInfoAdd add = new PreprocessInfoAdd();
                            add.createUser = UserInfo.RealName;
                            add.goodsName = goods.shortName;
                            add.goodsUnit = goods.goodsUnit;
                            add.modelNum = goods.modelNum;
                            add.operateUser = UserInfo.RealName;
                            add.packWeight = goods.modelNum;
                            add.partnerCode = UserInfo.PartnerCode;
                            add.partnerName = UserInfo.PartnerName;
                            add.physicsUnit = goods.physicsUnit;
                            add.skuCode = goods.skuCode;
                            add.status = 0;
                            add.updateUser = UserInfo.RealName;
                            list.Add(add);
                        }
                        #endregion
                    }

                    if (UserInfo.CustomerCode != "7001" && UserInfo.CustomerCode != "7002")
                    {

                        if (UserInfo.CustomerCode == "15001" || UserInfo.CustomerCode == "11001" 
                            || UserInfo.CustomerCode == "18001"|| UserInfo.CustomerCode == "19001"
                              || UserInfo.CustomerCode == "22001" || UserInfo.CustomerCode == "31001" || UserInfo.CustomerCode == "32001"
                           
                            )
                        {

                            #region 青年餐厅
                            
                         
                            PreprocessInfoRequest request = new PreprocessInfoRequest();
                            request.wareHouseId = UserInfo.WareHouseCode;
                            request.warehouseCode = UserInfo.WareHouseCode;
                            request.warehouseName = UserInfo.WareHouseName;
                            request.customerCode = UserInfo.CustomerCode;
                            request.customerName = UserInfo.CustomerName;

                            request.request = list;
                            PreprocessInfoAddResponse response = client.Execute(request);
                            if (!response.IsError)
                            {

                                if (response.result != null)
                                {

                                    preprocessInfoList = response.result;
                                    foreach (PreprocessInfo item in preprocessInfoList)
                                    {
                                        curPreprocessInfo = item;
                                        PrintDocument document = new PrintDocument();
                                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                                        document.OriginAtMargins = true;
                                        document.DefaultPageSettings.Landscape = false;
                                        document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);



#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
                                dialog.Document = document;
#else
                                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
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
                                    }


                                }
                                GetCount();
                            }

                            #endregion
                        }
                        else if (UserInfo.CustomerCode == "30001" || UserInfo.CustomerCode == "23001")
                        {
                            #region 汪洋和嘉和一品

                            PreprocessInfoRequest request = new PreprocessInfoRequest();
                            request.wareHouseId = UserInfo.WareHouseCode;
                            request.warehouseCode = UserInfo.WareHouseCode;
                            request.warehouseName = UserInfo.WareHouseName;
                            request.customerCode = UserInfo.CustomerCode;
                            request.customerName = UserInfo.CustomerName;

                            request.request = list;
                            PreprocessInfoAddResponse response = client.Execute(request);
                            if (!response.IsError)
                            {

                                if (response.result != null)
                                {

                                    preprocessInfoList = response.result;
                                    foreach (PreprocessInfo item in preprocessInfoList)
                                    {
                                        curPreprocessInfo = item;
                                        PrintDocument document = new PrintDocument();
                                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                                        document.OriginAtMargins = true;
                                        document.DefaultPageSettings.Landscape = false;
                                        document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);



#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintCustomerDatePage);
                                dialog.Document = document;
#else
                                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintCustomerDatePage);
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
                                    }


                                }
                                GetCount();
                            }
                            #endregion

                        }
                        else
                        {

                            #region 正常客户



                            PreprocessInfoRequest request = new PreprocessInfoRequest();
                            request.wareHouseId = UserInfo.WareHouseCode;
                            request.warehouseCode = UserInfo.WareHouseCode;
                            request.warehouseName = UserInfo.WareHouseName;
                            request.customerCode = UserInfo.CustomerCode;
                            request.customerName = UserInfo.CustomerName;

                            request.request = list;
                            PreprocessInfoAddResponse response = client.Execute(request);
                            if (!response.IsError)
                            {

                                if (response.result != null)
                                {

                                    preprocessInfoList = response.result;
                                    foreach (PreprocessInfo item in preprocessInfoList)
                                    {
                                        curPreprocessInfo = item;
                                        PrintDocument document = new PrintDocument();
                                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                                        document.OriginAtMargins = true;
                                        document.DefaultPageSettings.Landscape = false;
                                        document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);



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
                                    }


                                }
                                GetCount();
                            }

                            #endregion


                        }
                    }
                    else
                    {
                        #region 西贝

                        PreprocessXiBeiInfoRequest request = new PreprocessXiBeiInfoRequest();
                        request.wareHouseId = UserInfo.WareHouseCode;
                        request.warehouseCode = UserInfo.WareHouseCode;
                        request.warehouseName = UserInfo.WareHouseName;
                        request.customerCode = UserInfo.CustomerCode;
                        request.customerName = UserInfo.CustomerName;

                        request.request = list;
                        PreprocessInfoAddResponse response = client.Execute(request);
                        if (!response.IsError)
                        {

                            if (response.result != null)
                            {

                                preprocessInfoList = response.result;
                                foreach (PreprocessInfo item in preprocessInfoList)
                                {
                                    curPreprocessInfo = item;
                                    curPreprocessInfo.productWorkshopAttrDesc = goods.productWorkshopAttrDesc;
                                    PrintDocument document = new PrintDocument();
                                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                                    document.OriginAtMargins = true;
                                    document.DefaultPageSettings.Landscape = false;
                                    document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);

#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
                                dialog.Document = document;
#else
                                    PrintPreviewDialog dialog = new PrintPreviewDialog();
                                    document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
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
                                }
                            }


                            GetCount();
                        }
                        else
                        {
                            MessageBox.Show("出现错误:" + goods.goodsName + response.Message);
                        }

                        #endregion

                    }

                    tbWeight.Text = "";



                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现异常" + ex.Message);
                    LogHelper.Log("出现异常tbWeight_KeyDown" + ex.Message);
                }
                finally
                {
                    tbBiaoQianNum.Text = "1";
                    tbWeight.Enabled = true;
                    tbWeight.Focus();
                }

            }
        }

        private void pd_XiBeiPrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateXiBeiQRCode(curPreprocessInfo.preprocessCode);
            GetXiBeiPrintPicture(bt, e, curPreprocessInfo);
        }


        private void pd_PrintCustomerDatePage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPreprocessInfo.preprocessCode);
            GetPrintCustomerDatePicture(bt, e, curPreprocessInfo);
        }


        public void GetPrintCustomerDatePicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            if (goods.categoryCode == "10")
            {

                #region 分类为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);
                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);




                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height += 15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);


                height += 15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + this.dt.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + expireDay + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //编码
                Font fontCode = new Font("宋体", 8f);
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);

                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //   g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号：" + UserInfo.foodLicenseNo, new Font("宋体", 10f), brush, layoutRectangleRight);
                }


                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }


                #endregion
            }
            else
            {

                #region 分类不为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //编码
                Font fontCode = new Font("宋体", 8f);
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);



                height += 60;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                //   g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号：" + UserInfo.foodLicenseNo, new Font("宋体", 10f), brush, layoutRectangleRight);
                }


                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }



                #endregion
            }
        }

        public static Bitmap CreateXiBeiQRCode(string asset)
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

        public void GetXiBeiPrintPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);

            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = 15;
            int heightRight = 15;

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = 5;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(145, -5, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            heightRight += 40;


            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);

            if (handleWay != null && handleWay.Trim() != "")
            {
                Pen pen = new Pen(Color.Black, 1);
                g.Graphics.DrawEllipse(pen, 5, 5, 25, 25);
                g.Graphics.DrawString(handleWay.Trim(), new Font("宋体", 12f, FontStyle.Bold), brush, 7, 10);
            }

            //商品名称
            layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            g.Graphics.DrawString("   " + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

            height += 30;
            //重量

            layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            if (goods.weighed == 1)
            {
                g.Graphics.DrawString("净含量:" + preprocessInfo.packWeight + "Kg", font, brush, layoutRectangle);
            }
            else
            {

                g.Graphics.DrawString(goods.goodsModel, font, brush, layoutRectangle);
            }


            height += 15;


            //生产日期
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);



            if (preprocessInfo.productWorkshopAttrDesc != null && preprocessInfo.productWorkshopAttrDesc != "" && preprocessInfo.productWorkshopAttrDesc != "净毛菜车间" && preprocessInfo.productWorkshopAttrDesc != "库房车间")
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + expireDay + "天 " + "储存方式:0-8℃", new Font("宋体", 8f), brush, layoutRectangleRight);
            }
            else
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("储存方式:" + "0-8℃", new Font("宋体", 10f), brush, layoutRectangleRight);
            }

            //height += 15;
            //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            //g.Graphics.DrawString("储存方式:" + "0-8℃", new Font("宋体", 10f), brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("产地:" + UserInfo.areaName, new Font("宋体", 8f), brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);

            if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
            {
                g.Graphics.DrawString("食用方法:" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
            }
            else
            {
                g.Graphics.DrawString("食用方法:", new Font("宋体", 8f), brush, layoutRectangleRight);
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
          //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);
            if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
            {
                g.Graphics.DrawString("食品经营许可证号："+UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);

            // "北京康安利丰农业有限公司"
        }


        private void WeightForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curPreprocessInfo.preprocessCode);
            GetPrintPicture(bt, e, curPreprocessInfo);
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


        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {
            // if (goods.categoryCode == "10" || goods.categoryCode == "11" || goods.categoryCode == "12" || goods.categoryCode == "13" || goods.categoryCode == "17" || goods.categoryCode == "20")
            //if (goods.twoCategoryCode == "0103")
            if (goods.categoryCode == "10")
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
                //heightRight =image.Width-20;

                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.CompanyName, font, brush, layoutRectangleRight);

                heightRight += 40;

                //流通号
                layoutRectangleRight = new RectangleF(pointX, 55, 300f, 85f);
              //  g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);
                if (UserInfo.foodLicenseNo != null && UserInfo.foodLicenseNo != "")
                {
                    g.Graphics.DrawString("食品经营许可证号："+UserInfo.foodLicenseNo, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                //生产厂家
                layoutRectangleRight = new RectangleF(pointX, 70, 300f, 85f);
                g.Graphics.DrawString("厂家:" + UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);


                heightRight += 15;
                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);


                //门店名称

                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);



                //商品名称
                layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(preprocessInfo.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height - 15, 120f, 40f);
                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    //    g.Graphics.DrawString("1" + goods.physicsUnit+"/"+goods.modelNum+goods.goodsUnit, fontCu, brush, layoutRectangle);
                    // g.Graphics.DrawString(goods.goodsModel, fontCu, brush, layoutRectangle);
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);

                }


                height += interval;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 63 + image.Height;

                Font fontCode = new Font("宋体", 8f);
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangle);


                //货主
                layoutRectangleRight = new RectangleF(pointX + image.Width, 86, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产日期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 105, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString().ToString(), new Font("宋体", 8f), brush, layoutRectangleRight);

                //保质期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 125, 300f, 85f);
                g.Graphics.DrawString("保 质 期: " + expireDay + "天", new Font("宋体", 8f), brush, layoutRectangleRight);

                //if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                //{
                //    layoutRectangleRight = new RectangleF(pointX + image.Width, 140, 300f, 85f);
                //    g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                //}
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
                //heightRight =image.Width-20;

                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.CompanyName, font, brush, layoutRectangleRight);

                heightRight += 40;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight);

                heightRight += 40;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, font, brush, layoutRectangleRight);


                heightRight += 10;
                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);
                if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                {
                    //layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                    //g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                //门店名称

                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);



                //商品名称
                layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(preprocessInfo.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 120f, 40f);
                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    //   g.Graphics.DrawString("1" + goods.physicsUnit+"/"+goods.modelNum+goods.goodsUnit, fontCu, brush, layoutRectangle);
                    //  g.Graphics.DrawString(goods.goodsModel, fontCu, brush, layoutRectangle);
                    // g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);

                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }


                height += interval;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 63 + image.Height;
                Font fontCode = new Font("宋体", 8f);
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangle);
            }
        }

        private void tbBiaoQianNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbWeight_KeyDown(sender, e);
            }
        }
    }
}

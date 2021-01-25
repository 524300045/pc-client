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
    public partial class ChaoMaWeightForm : Form
    {

        Goods goods;

        private IWMSClient client = null;

        private WmsFrozenBoxCode wmsFrozenBoxCode;


        public ChaoMaWeightForm()
        {
            InitializeComponent();
        }

        public ChaoMaWeightForm(Goods _goods)
        {
            InitializeComponent();
            this.goods = _goods;
            client = new DefalutWMSClient();
        }

        private void ChaoMaWeightForm_Load(object sender, EventArgs e)
        {
            tbWeight.Focus();
            lbName.Text=this.goods.goodsName+"("+this.goods.skuCode+")";
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
                    decimal curWeight = 0; ;


                    if (goods.skuCode == "113678"
                        || goods.skuCode == "113673"
                        || goods.skuCode == "113679"
                        || goods.skuCode == "113674"
                        || goods.skuCode == "113905"
                        || goods.skuCode == "113658"
                        || goods.skuCode == "113675"
                        || goods.skuCode == "114690")
                    {
                         curWeight = Util.ConvertGToKg(weight);
                    }
                    else
                    {
                        // curWeight = Util.ConvertGToJin(weight);
                        curWeight = weight;
                    }

                    WmsFrozenBoxCodeChaoMaRequest request = new WmsFrozenBoxCodeChaoMaRequest();
                    request.customerCode = UserInfo.CustomerCode;
                    request.customerName = UserInfo.CustomerName;
                    request.warehouseCode = UserInfo.WareHouseCode;
                    request.warehouseName = UserInfo.WareHouseName;
                    request.createUser = UserInfo.UserName;
                    request.skuCode = goods.skuCode;
                    request.modelNum = curWeight;
                    request.productionDate = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
                    WmsFrozenBoxCodeChaoMaResponse response = client.Execute(request);
                    if (!response.IsError)
                    {
                        if (response.result == null)
                        {
                            MessageBox.Show("打印失败");
                            return;
                        }
                        wmsFrozenBoxCode = response.result;
                       

                        if (goods.skuCode == "113678"
                            || goods.skuCode == "113673"
                            || goods.skuCode == "113679"
                            || goods.skuCode == "113674"
                            || goods.skuCode == "113905"
                            || goods.skuCode == "113658"
                            || goods.skuCode == "113675"
                            || goods.skuCode == "114690")
                        {
                            #region 打印
                            PrintDocument document = new PrintDocument();
                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                            document.OriginAtMargins = true;
                            document.DefaultPageSettings.Landscape = false;
                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if (!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_Print113678);
                                dialog.Document = document;
#else
                            PrintPreviewDialog dialog = new PrintPreviewDialog();
                            document.PrintPage += new PrintPageEventHandler(this.pd_Print113678);
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

                            #endregion

                        }
                        else
                        {
                            #region 打印

                            PrintDocument document = new PrintDocument();
                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                            document.OriginAtMargins = true;
                            document.DefaultPageSettings.Landscape = false;
                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if (!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_Print);
                                dialog.Document = document;
#else
                            PrintPreviewDialog dialog = new PrintPreviewDialog();
                            document.PrintPage += new PrintPageEventHandler(this.pd_Print);
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

                            #endregion
                        }


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
                finally
                {
                    tbWeight.Enabled = true;
                    tbWeight.Text = "";
                    tbWeight.Focus();
                }
            }
        }

        private void pd_Print113678(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateCode(wmsFrozenBoxCode.boxCode);
            GetPicture113678(bt, e);
        }


        private void pd_Print(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateCode(wmsFrozenBoxCode.boxCode);
            GetPicture(bt, e);
        }

        public static Bitmap CreateCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 110,
                Height = 110
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }


        public void GetPicture(Bitmap image, PrintPageEventArgs g)
        {

            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = 0;
           

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = -15;
            RectangleF layoutRectangleRight = new RectangleF(100f, height, 130f, 85f);
            Rectangle destRect = new Rectangle(100, height, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            
            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
            //layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            //g.Graphics.DrawString(wmsFrozenBoxCode.skuCode, fontCu11, brush, layoutRectangle);

            height += 15;
            layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
            g.Graphics.DrawString(wmsFrozenBoxCode.goodsName+ wmsFrozenBoxCode.skuCode, fontCu11, brush, layoutRectangle);

         

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("重量:" + wmsFrozenBoxCode.surplusWeight +"克", new Font("宋体", 10f), brush, layoutRectangleRight);


            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), new Font("宋体", 10f), brush, layoutRectangleRight);

            //String freshAttrDes = "";
            //if (wmsFrozenBoxCode.freshAttr == 1)
            //{
            //    freshAttrDes = "常温";
            //}
            //if (wmsFrozenBoxCode.freshAttr == 2)
            //{
            //    freshAttrDes = "冷冻";
            //}

            //if (wmsFrozenBoxCode.freshAttr == 3)
            //{
            //    freshAttrDes = "冷藏";
            //}

            //height += 15;
            //layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            //g.Graphics.DrawString("温区:" + freshAttrDes, font, brush, layoutRectangleRight);

            String typeDes = "";
            if (wmsFrozenBoxCode.goodsType == 2)
            {
                typeDes = "抄码整箱";
            }
            if (wmsFrozenBoxCode.goodsType == 3)
            {
                typeDes = "抄码拆零";
            }
            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("商品类型:" + typeDes, font, brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(UserInfo.CustomerName, font, brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(wmsFrozenBoxCode.boxCode, font, brush, layoutRectangleRight);


        }



        public void GetPicture113678(Bitmap image, PrintPageEventArgs g)
        {

            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = -5;
            

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = -15;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(110, 10, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);

            height += 15;
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
            g.Graphics.DrawString(wmsFrozenBoxCode.goodsName+ wmsFrozenBoxCode.skuCode, fontCu11, brush, layoutRectangle);


            String peiLiao = "";
            if (wmsFrozenBoxCode.skuCode== "113675")
            {
                peiLiao = "牛大力骨、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113679")
            {
                peiLiao = "牛肉牛排、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113674")
            {
                peiLiao = "牛脖骨、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113678")
            {
                peiLiao = "牛力骨、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113905")
            {
                peiLiao = "肩胛骨、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113673")
            {
                peiLiao = "牛棒骨、水";
            }
            if (wmsFrozenBoxCode.skuCode == "113658")
            {
                peiLiao = "挂钩筋、水";
            }
            if (wmsFrozenBoxCode.skuCode == "114690")
            {
                peiLiao = "燕翅骨、水";
            }

            //商品名称
            if (peiLiao!="")
            {
                height += 15;
                layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
                g.Graphics.DrawString("配料:" + peiLiao, fontCu11, brush, layoutRectangle);
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("重量:"+wmsFrozenBoxCode.weight*2+ "斤 保质期:7个月", new Font("宋体", 8f,FontStyle.Bold), brush, layoutRectangleRight);


            height += 13;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("贮存条件:<=-18", new Font("宋体", 8f, FontStyle.Bold), brush, layoutRectangleRight);


            height += 11;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("包装方式:非真空", new Font("宋体", 8f), brush, layoutRectangleRight);


            height += 11;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:"+this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), new Font("宋体", 8f, FontStyle.Bold), brush, layoutRectangleRight);


            height += 11;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("电话:010-57912177", new Font("宋体", 8f, FontStyle.Bold), brush, layoutRectangleRight);

            height +=10;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("食品经营许可证:JY21117021542763", new Font("宋体", 8f, FontStyle.Bold), brush, layoutRectangleRight);


            height += 13;
            layoutRectangleRight = new RectangleF(pointX, height, 200f, 85f);
            g.Graphics.DrawString("生产厂家:北京西贝天然派食品科技发展有限公司", new Font("宋体", 8f), brush, layoutRectangleRight);

            height +=25;
            layoutRectangleRight = new RectangleF(pointX, height, 200f, 85f);
            g.Graphics.DrawString("生产地址:北京市平谷区中关村科技园区平谷园兴谷A区兴谷西路40号", new Font("宋体", 8f), brush, layoutRectangleRight);

       

         

            height += 25;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(wmsFrozenBoxCode.boxCode, new Font("宋体", 8f, FontStyle.Bold), brush, layoutRectangleRight);


            height += 11;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("产地:北京   西贝专供", new Font("宋体", 8f), brush, layoutRectangleRight);

        }
    }
}

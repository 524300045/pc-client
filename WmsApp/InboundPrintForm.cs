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
    public partial class InboundPrintForm : Form
    {


        private IWMSClient client = null;

        private long detailId;

        private String productDate;

        private String skuCode;

        private WmsFrozenBoxCode wmsFrozenBoxCode;


        public InboundPrintForm()
        {
            InitializeComponent();
        }

        public InboundPrintForm(long detailId,string skuCode, string goodsName,string productionDate,decimal modelNum )
        {
            InitializeComponent();
            this.lbName.Text = goodsName;
            this.lbDate.Text = productionDate;
            this.lbModel.Text = modelNum.ToString();
            this.detailId=detailId;
            this.productDate = productionDate;
            this.skuCode = skuCode;
            client = new DefalutWMSClient();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                  if (string.IsNullOrEmpty(tbNum.Text.Trim()))
                {
                    MessageBox.Show("请录入打印数量");
                    return;
                }
                if (int.Parse(tbNum.Text.Trim()) <= 0)
                {
                    MessageBox.Show("数量必须大于0");
                    return;
                }

                if (int.Parse(tbNum.Text.Trim())>1000)
                {
                      MessageBox.Show("单次最多打印1000张");
                     return;
                }

                WmsFrozenBoxCodeBuPrintRequest request = new WmsFrozenBoxCodeBuPrintRequest();
                request.customerCode = UserInfo.CustomerCode;
                request.customerName = UserInfo.CustomerName;
                request.warehouseCode = UserInfo.WareHouseCode;
                request.warehouseName = UserInfo.WareHouseName;
                request.num = int.Parse(tbNum.Text.Trim());
                request.createUser = UserInfo.UserName;
                request.detailId=this.detailId;
                request.skuCode = skuCode;
                WmsFrozenBoxCodeResponse response = client.Execute(request);
                if (!response.IsError)
                {
                    if (response.result == null)
                    {
                        MessageBox.Show("打印失败");
                        return;
                    }
                    for (int i = 0; i < response.result.Count; i++)
                    {
                        
                        wmsFrozenBoxCode = response.result[i];
                        wmsFrozenBoxCode.productionDate = productDate;
                        #region 打印

                        PrintDocument document = new PrintDocument();
                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                        document.OriginAtMargins = true;
                        document.DefaultPageSettings.Landscape = false;
                        document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
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
                MessageBox.Show(""+ex.Message);
            }
            this.DialogResult = DialogResult.OK;
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
                Width = 140,
                Height = 140
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
            int height = 15;
            int heightRight = 15;

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = -15;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(90, 30, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            heightRight += 40;
            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);


            layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            g.Graphics.DrawString(wmsFrozenBoxCode.skuCode, fontCu11, brush, layoutRectangle);

            height += 15;
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
            g.Graphics.DrawString(wmsFrozenBoxCode.goodsName, fontCu11, brush, layoutRectangle);

            height += 15;
            //重量

            layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            g.Graphics.DrawString("生产日期:" + wmsFrozenBoxCode.productionDate.Replace("00:00:00",""), font, brush, layoutRectangle);

            //height += 15;
            ////重量

            //layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            //g.Graphics.DrawString("保质期:" + wmsFrozenBoxCode.expiryDay+"天", font, brush, layoutRectangle);

            height += 15;


            //生产日期
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("包规:" + wmsFrozenBoxCode.surplusQuantity + wmsFrozenBoxCode.goodsUnit + "/" + wmsFrozenBoxCode.physicsUnit, new Font("宋体", 10f), brush, layoutRectangleRight);

            String freshAttrDes = "";
            if (wmsFrozenBoxCode.freshAttr == 1)
            {
                freshAttrDes = "常温";
            }
            if (wmsFrozenBoxCode.freshAttr == 2)
            {
                freshAttrDes = "冷冻";
            }

            if (wmsFrozenBoxCode.freshAttr == 3)
            {
                freshAttrDes = "冷藏";
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("温区:" + freshAttrDes, font, brush, layoutRectangleRight);

            String typeDes = "";
            if (wmsFrozenBoxCode.goodsType == 0)
            {
                typeDes = "标品整箱";
            }
            if (wmsFrozenBoxCode.goodsType == 1)
            {
                typeDes = "标品拆零";
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
    }
}

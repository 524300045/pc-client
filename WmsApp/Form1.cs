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
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {
            int height = 5;
            int heightRight = 10;

            Font font = new Font("宋体", 12f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            int interval = 20;
            int pointX = 5;
      
            RectangleF layoutRectangleRight = new RectangleF(250f, 15, 400f, 85f);
            g.Graphics.DrawString("1010000100000002", font, brush, layoutRectangleRight);

            heightRight += 20;
            Rectangle destRect = new Rectangle(250, heightRight, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            heightRight += 8 + image.Width;

            layoutRectangleRight = new RectangleF(250, heightRight, 230f, 85f);
            g.Graphics.DrawString("北京平谷1号库", font, brush, layoutRectangleRight);


            //门店名称
            Font fontCu = new Font("宋体", 15f,FontStyle.Bold);
            RectangleF layoutRectangle = new RectangleF(pointX, height, 230f, 85f);
            g.Graphics.DrawString("绿茶朝阳店", fontCu, brush, layoutRectangle);

            height += interval;
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 230f, 85f);
            g.Graphics.DrawString("菠菜（水洗 | 优质）", font, brush, layoutRectangle);

            height += interval;
            //重量
            
            layoutRectangle = new RectangleF(pointX, height, 230f, 85f);
            g.Graphics.DrawString("15斤", font, brush, layoutRectangle);

        

            height += interval;
            //条码
            // layoutRectangle = new RectangleF(pointX, height, 230f, 85f);
            //   g.Graphics.DrawString("规格型号:", font, brush, layoutRectangle);
            Rectangle dest2Rect = new Rectangle(pointX, height, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            layoutRectangle = new RectangleF(pointX, height + image.Height, 230f, 85f);
            g.Graphics.DrawString("1010000100000001", font, brush, layoutRectangle);

        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode("1010000100000001");
            GetPrintPicture(bt, e);
        }

        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 120,
                Height = 120
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            PrintDocument document = new PrintDocument();
            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 250);
            //Margins margins = new Margins(0x87, 20, 5, 20);
            //document.DefaultPageSettings.Margins = margins;
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            document.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            dialog.Document = document;
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
}

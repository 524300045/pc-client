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
using WmsSDK.Request;
using WmsSDK.Response;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public partial class FrozenNumPrintForm : Form
    {

        private string curBoxCode;

        private IWMSClient client = null;


        public FrozenNumPrintForm()
        {
            InitializeComponent();
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
                btnSure.Enabled = false;
                btnCancel.Enabled = false;
                if (string.IsNullOrEmpty(tbNum.Text.Trim()))
                {
                    MessageBox.Show("请录入打印数量");
                    return;
                }
                if (int.Parse(tbNum.Text.Trim())<=0)
                {
                      MessageBox.Show("数量必须大于0");
                    return;
                }

                if (int.Parse(tbNum.Text.Trim()) > 1000)
                {
                    MessageBox.Show("单次最多打印1000张");
                    return;
                }

                FrozenBoxCodeRequest request = new FrozenBoxCodeRequest();
                request.warehouseCode = UserInfo.WareHouseCode;
                request.num = int.Parse(tbNum.Text.Trim());
                FrozenBoxCodeResponse response = client.Execute(request);
                 if (!response.IsError)
                 {
                     if (response.result == null)
                     {
                         MessageBox.Show("打印失败");
                         return;
                     }

                     for (int i = 0; i < response.result.Count; i++)
                     { 
                        curBoxCode=response.result[i];
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
                     }

                 }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnCancel.Enabled = true;
                btnSure.Enabled = true;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void pd_Print(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateCode(curBoxCode);
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


            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);
            int pointX = 5;
            Rectangle destRect = new Rectangle(pointX, -5, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            int height = 110;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(curBoxCode, new Font("宋体", 12f), brush, layoutRectangleRight);




        }

    }
}

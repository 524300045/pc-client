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
    public partial class PrintBoxStoreForm : Form
    {
        private IWMSClient client = null;

        private BoxInfo curBoxInfo = null;

        public PrintBoxStoreForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }



        private void PrintBoxForm_Load(object sender, EventArgs e)
        {
            bindStore();
        }

        private void bindStore()
        {
            StoreInfoRequest request = new StoreInfoRequest();
            request.partnerCode = UserInfo.PartnerCode;
            StoreInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<StoreInfo> storeList = new List<StoreInfo>();
                    storeList = response.result.OrderBy(p=>p.storedCode).ToList();
                    this.dataGridView1.DataSource = storeList;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        this.dataGridView1.Rows[i].Cells["Num"].Value = "1";
                    }
                }
            }
        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curBoxInfo.boxCode);
            GetPrintPicture(bt, e, curBoxInfo);
        }

        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 130,
                Height = 130
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }


        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g, BoxInfo boxInfo)
        {

            Font fontCu = new Font("宋体", 12f, FontStyle.Bold);
          

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
       
            //门店名称

            int height = 5;
            RectangleF layoutRectangle = new RectangleF(100, height, 200f, 30f);
            g.Graphics.DrawString(boxInfo.boxCode, font, brush, layoutRectangle);

            Rectangle dest2Rect = new Rectangle(70, 20, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            height = image.Height+5 ;
            //重量
            layoutRectangle = new RectangleF(100, height, 130f, 30f);
            g.Graphics.DrawString(boxInfo.storedName , fontCu, brush, layoutRectangle);

          

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Text = "正在打印....";
                btnPrint.Enabled = false;

                for (int m= 0; m< dataGridView1.Rows.Count; m++)
                {
                    if (this.dataGridView1.Rows[m].Cells["Num"].Value != null)
                    {
                        int num = 0;
                        try
                        {
                            num = int.Parse(this.dataGridView1.Rows[m].Cells["Num"].Value.ToString());
                        }
                        catch (Exception)
                        {

                        }
                        if (num > 0)
                        {

                            List<BoxInfoAdd> list = new List<BoxInfoAdd>();
                            for (int i = 0; i < num; i++)
                            {
                                BoxInfoAdd boxInfo = new BoxInfoAdd();
                                boxInfo.customerCode = UserInfo.CustomerCode;
                                boxInfo.customerName = UserInfo.CustomerName;
                                boxInfo.warehouseCode = UserInfo.WareHouseCode;
                                boxInfo.warehouseName = UserInfo.WareHouseName;
                                boxInfo.storedCode = this.dataGridView1.Rows[m].Cells["storedCode"].Value.ToString();
                                boxInfo.storedName = this.dataGridView1.Rows[m].Cells["storedName"].Value.ToString();
                                boxInfo.partnerCode = UserInfo.PartnerCode;
                                boxInfo.partnerName = UserInfo.PartnerName;
                                boxInfo.printMan = UserInfo.RealName;
                                boxInfo.updateUser = UserInfo.RealName;
                                boxInfo.createUser = UserInfo.RealName;
                                list.Add(boxInfo);
                            }


                            BoxInfoAddRequest request = new BoxInfoAddRequest();
                            request.request = list;
                            BoxInfoAddResponse response = client.Execute(request);
                            if (!response.IsError)
                            {
                                //开始打印
                                if (response.result != null)
                                {
                                    //循环打印
                                    foreach (BoxInfo item in response.result)
                                    {
                                        curBoxInfo = item;
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
                                    }

                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常" + ex.Message);
            }
            finally
            {
               
                btnPrint.Enabled = true;
                btnPrint.Text = "批量打印";
                MessageBox.Show("打印完成");
                this.DialogResult = DialogResult.OK;
            }
         
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells["Num"].Value = "0";
            }
        }

      

    }
}

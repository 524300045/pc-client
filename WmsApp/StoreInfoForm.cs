using Sdbs.Wms.Controls.Pager;
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
using Wms.Controls.Pager;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public partial class StoreInfoForm : TabWindow
    {

        private IWMSClient client = null;
        public StoreInfoForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;

            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
            cbHeader.Value = string.Empty;

            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dataGridView1.Columns[0].HeaderCell = cbHeader;

           BindDgv();
        }

        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            //这一句很重要结束编辑状态
            dataGridView1.EndEdit();
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = state;
                }
            }
        }
      

        private void BindDgv()
        {
          
             StoreInfoAllRequest request = new StoreInfoAllRequest();
           // request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
               List<StoreInfo> storeList=new List<StoreInfo>();
           StoreInfoResponse response=client.Execute(request);
           if (!response.IsError)
           {
               if (response.result!=null)
               {
                
                   foreach (StoreInfo item in  response.result)
                   {
                       storeList.Add(item);
                   }
               }
           }
           if (!string.IsNullOrWhiteSpace(tbStoreName.Text.Trim()))
           {
               storeList = storeList.Where(p=>p.storedName.Contains(tbStoreName.Text.Trim())).ToList() ;
           }
               this.dataGridView1.DataSource = storeList;
            
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
           
            BindDgv();
        }


        private String curStoreCode;

        private string curStoreName;



        private void btnBox_Click(object sender, EventArgs e)
        {

            int m = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    m++;
                }
            }
            if (m == 0)
            {
                MessageBox.Show("请选择要打印的门店");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    string storeCode = this.dataGridView1.Rows[i].Cells["storedCode"].Value.ToString();
                    string storeName = this.dataGridView1.Rows[i].Cells["storedName"].Value.ToString();

                    curStoreCode = storeCode;
                    curStoreName =UserInfo.CustomerName+"-"+storeName;

                    //打印门店码
                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 300);

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


        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curStoreCode);
            GetPrintPicture(bt, e);
        }

        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 230,
                Height = 230
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }


        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {

            Font fontCu = new Font("宋体", 16f, FontStyle.Bold);


            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            //门店名称
            int height = 15;
          

            Rectangle dest2Rect = new Rectangle(150, 20, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            RectangleF layoutRectangle = new RectangleF(180, height+15, 500f, 30f);
            g.Graphics.DrawString(curStoreName, fontCu, brush, layoutRectangle);

            height = image.Height-10;
            layoutRectangle = new RectangleF(230, height, 130f, 30f);
            g.Graphics.DrawString(curStoreCode, font, brush, layoutRectangle);



        }

    }
}

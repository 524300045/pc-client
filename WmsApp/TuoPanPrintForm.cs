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
    public partial class TuoPanPrintForm : TabWindow
    {

        private IWMSClient client = null;

        private PaginatorDTO paginator;

        private SortableBindingList<WmsSDK.Model.LocationsModel> sortList = null;

        String areaCode = "ZC";
        public TuoPanPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                paginator = new PaginatorDTO { PageNo = 1, PageSize = 50 };
                btnQuery.Enabled = false;
                BindDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询异常");
            }
            finally
            {
                btnQuery.Enabled = true;
            }
        }

        String curBarCode = "";
        private void btnPrint_Click(object sender, EventArgs e)
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
                MessageBox.Show("请选择要打印的容器");
                return;
            }

           
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                     curBarCode = this.dataGridView1.Rows[i].Cells["locationCode"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["locationCode"].Value.ToString();
                   
                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

            
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
                                dialog.Document = document;
#else
                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
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

        private void BindDgv()
        {

            LocationsQueryRequest request = new LocationsQueryRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            request.areaCode = areaCode;
            request.locationName = tbName.Text.Trim();
            request.page = paginator.PageNo;

            LocationsResponse response = client.Execute(request);

            if (!response.IsError)
            {

                if (response.result == null)
                {

                    this.dataGridView1.DataSource = null;
                    MessageBox.Show("没有打印任务!");
                    return;
                }



                int recordCount = response.pageUtil.totalRow;
                int totalPage;
                if (recordCount % paginator.PageSize == 0)
                {
                    totalPage = recordCount / paginator.PageSize;
                }
                else
                {
                    totalPage = recordCount / paginator.PageSize + 1;
                }

                IPagedList<LocationsModel> pageList = new PagedList<LocationsModel>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<LocationsModel>(pageList.ContentList);
                this.dataGridView1.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
            }
        }

        private void GoodsBarCodePrintForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;
                DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();

                cbHeader.Value = string.Empty;

                cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
                dataGridView1.Columns[0].HeaderCell = cbHeader;

                paginator = new PaginatorDTO { PageNo = 1, PageSize = 50 };

                BindDgv();

            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
            }
          
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






        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
        }

   
        private void pd_PrintBoxPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curBarCode);
            GetPrintPicture(bt, e);
        }

        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {


            Font fontCu = new Font("宋体", 12f, FontStyle.Bold);

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //int pointX = 10;

            //左上角二维码
            Rectangle dest2Rect = new Rectangle(60, 5, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            //商品编码
            RectangleF layoutRectangleRight = new RectangleF(110, 145, 300f, 85f);
            g.Graphics.DrawString(curBarCode, font, brush, layoutRectangleRight);




        }


        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 160,
                Height = 160
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
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


    }
  
}

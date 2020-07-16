using Sdbs.Wms.Controls.Pager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
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
    public partial class BinForm : TabWindow
    {

        private IWMSClient client = null;

        private PaginatorDTO paginator;

        private SortableBindingList<WmsSDK.Model.LocationsModel> sortList = null;


        public BinForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();

        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                paginator = new PaginatorDTO { PageNo = 1, PageSize = 30 };
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

     

     

        private void BindDgv()
        {


            string areaCode = cbArea.SelectedValue.ToString();


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

                paginator = new PaginatorDTO { PageNo = 1, PageSize = 30 };



                List<WarehouseArea> list = new List<WarehouseArea>();
                list.Add(new WarehouseArea() { areaCode = "A", areaName = "A" });
                list.Add(new WarehouseArea() { areaCode = "B", areaName = "B" });
                list.Add(new WarehouseArea() { areaCode = "AB", areaName = "AB" });
                list.Insert(0, new WarehouseArea() { areaCode = "", areaName = "全部" });
                cbArea.DataSource = list;
                cbArea.DisplayMember = "areaName";
                cbArea.ValueMember = "areaCode";

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
       
                Bitmap bt = CreateQRCode(curLocationCode);
                GetPrintPicture(bt, e);
          

        }

        public static Bitmap CreateQRCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 320,
                Height = 320
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }

  

        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {
            int x = 10;
            int y = 100;
            int imageY = 10;

            String[] barCodeArr = curLocationCode.Split('-');

            string bar1 = barCodeArr[0];
            string bar2 = barCodeArr[1];
            string bar3 = barCodeArr[2];

            Font fontCu = new Font("宋体", 30f, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            RectangleF layoutRectangle = new RectangleF(x, y, 100f, 120f);
            g.Graphics.DrawString(bar1, fontCu, brush, layoutRectangle);

            y += 60;
            layoutRectangle = new RectangleF(x, y, 100f, 120f);
            g.Graphics.DrawString(bar2, fontCu, brush, layoutRectangle);

            y += 60;
            layoutRectangle = new RectangleF(x, y, 100f, 120f);
            g.Graphics.DrawString(bar3, fontCu, brush, layoutRectangle);


            //左上角二维码
            Rectangle dest2Rect = new Rectangle(x + 80, imageY, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            layoutRectangle = new RectangleF(x + 150, imageY + image.Height - 30, 300f, 120f);
            g.Graphics.DrawString(curLocationCode, new Font("宋体", 22f, FontStyle.Bold), brush, layoutRectangle);

            //绘制线
            //Pen Pen = new Pen(Brushes.Black, 1);
            //Point p1 = new Point(0, 520);
            //Point p2 = new Point(1400, 520);
            //g.Graphics.DrawLine(Pen, p1, p2);


            //Point p3 = new Point(700, 0);
            //Point p4 = new Point(700, 1400);
            //g.Graphics.DrawLine(Pen, p3, p4);


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

        string curLocationCode = "";

        private void button1_Click(object sender, EventArgs e)
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
                MessageBox.Show("请选择要打印的货位");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    curLocationCode = this.dataGridView1.Rows[i].Cells["locationCode"].Value.ToString();

                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 500, 400);

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

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

            List<LocationsModel> locationsList = new List<LocationsModel>();

            //string path = "d:\\高位货架编码.txt";
            //StreamReader sr = new StreamReader(path, Encoding.Default);
            //string line;
            //while ((line = sr.ReadLine()) != null)
            //{
            //    if (!string.IsNullOrWhiteSpace(line))
            //   {
            //       LocationsModel locationsModel = new LocationsModel();
            //       string areaCode="";
            //       string[] arr = line.Split('-');
            //       if (line.IndexOf("AB") > 0)
            //       {
            //           areaCode = "AB";
            //       }
            //       else
            //       {
            //           if (line.IndexOf("A") > 0)
            //           {
            //               areaCode = "A";
            //           }
            //           else
            //           {
            //               areaCode = "B";
            //           }
            //       }
            //        Console.WriteLine(line);
            //        locationsModel.areaCode = areaCode;
            //        locationsModel.locationCode = line.Trim();
            //        locationsModel.locationName = line.Trim();
            //        locationsList.Add(locationsModel);
            //   }
            //}


            try
            {

                btnExport.Enabled = false;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "库位(*.xls)|*.xls";
                sfd.Title = "导出包裹信息";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                sfd.FileName = "库位" + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string areaCode = cbArea.SelectedValue.ToString();


                    LocationsQueryRequest request = new LocationsQueryRequest();
                    request.warehouseCode = UserInfo.WareHouseCode;
                    request.areaCode = areaCode;
                    request.locationName = tbName.Text.Trim();
                    request.page = paginator.PageNo;
                    request.pageSize = 10000;

                    LocationsResponse response = client.Execute(request);

                    if (!response.IsError)
                    {

                        if (response.result == null)
                        {
                            MessageBox.Show("没有数据!");
                            return;
                        }
                        locationsList = response.result;

                        DataTable dtExecl = new DataTable();
                        DataColumn dc1 = new DataColumn("      库区编码       ");
                        dtExecl.Columns.Add(dc1);

                        DataColumn dc2 = new DataColumn("      库位编码     ");
                        dtExecl.Columns.Add(dc2);

                        DataColumn dc4 = new DataColumn("      库位名称       ");
                        dtExecl.Columns.Add(dc4);

                        for (int i = 0; i < locationsList.Count; i++)
                        {
                            DataRow dr = dtExecl.NewRow();
                            dr[0] = locationsList[i].areaCode;
                            dr[1] = locationsList[i].locationCode;
                            dr[2] = locationsList[i].locationName;
                            dtExecl.Rows.Add(dr);
                        }

                        NPOIHelper.ExportDTtoExcel(dtExecl, sfd.Title, "", sfd.FileName);

                        MessageBox.Show("导出成功");

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出异常" + ex.Message);
            }
            finally
            {

                btnExport.Enabled = true;
            }

        }
    }
}

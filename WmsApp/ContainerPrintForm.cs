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
    public partial class ContainerPrintForm : TabWindow
    {

        private IWMSClient client = null;

        private PaginatorDTO paginator;

        private SortableBindingList<WmsSDK.Model.Container> sortList = null;

        List<WmsSDK.Model.Container> list;

        string curBarCode = "";

        public ContainerPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
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

            int num = 0;
            ContainerPrintNumForm form = new ContainerPrintNumForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                num = form.number;
            }
            else
            {
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    String barCode = this.dataGridView1.Rows[i].Cells["barCode"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["barCode"].Value.ToString();
                    curBarCode = barCode;
                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                    for (int n = 0; n < num; n++)
                    {
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

        }

        private void BindDgv()
        {
            string codeOne = cbOne.SelectedValue == null ? "" : cbOne.SelectedValue.ToString();
        
            string name = tbName.Text.Trim();

            ContainerQueryRequest request = new ContainerQueryRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.areaCode = cbOne.SelectedValue.ToString();
            request.containerCode = name;
            request.status = null;
            if (cbStatus.SelectedIndex==1)
            {
                request.status = 1;
            }

            if (cbStatus.SelectedIndex ==2)
            {
                request.status = 2;
            }


            ContainerResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
                }
                else
                {
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
                    list = response.result;
                    foreach (WmsSDK.Model.Container item in list)
                    {
                        if (item.status==1)
                        {
                            item.StatusDes = "空闲";
                        }

                        if (item.status == 2)
                        {
                            item.StatusDes = "占用";
                        }
                    }
                    IPagedList<WmsSDK.Model.Container> pageList = new PagedList<WmsSDK.Model.Container>(list, recordCount, totalPage);
                    sortList = new SortableBindingList<WmsSDK.Model.Container>(pageList.ContentList);
                    this.dataGridView1.DataSource = sortList;
                    pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                    pageSplit1.PageCount = pageList.PageCount;
                    pageSplit1.PageNo = paginator.PageNo;
                    pageSplit1.DataBind();
                }

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


                WarehouseAreaRequest request = new WarehouseAreaRequest();
                request.warehouseCode = UserInfo.WareHouseCode;
                WarehouseAreaResponse response = client.Execute(request);
                if (!response.IsError)
                {
                    List<WarehouseArea> list = response.result;
                    list.Insert(0, new WarehouseArea() { areaCode = "", areaName = "全部" });
                    cbOne.DataSource = list;
                    cbOne.DisplayMember = "areaName";
                    cbOne.ValueMember = "areaCode";
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
            }
            cbOne.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
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

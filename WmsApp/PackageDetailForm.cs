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
    public partial class PackageDetailForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<PackageDetailQuery> sortList = null;

        private PackageDetailQuery curPackageDetailQuery;

        private List<PackageDetailQuery> packageDetailQueryList;

        private IWMSClient client = null;

        string wareName;

        int totalPage;

        private BoxInfo curBoxInfo = null;

        public PackageDetailForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            cbStore.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            this.dtBegin.Value = DateTime.Now.AddDays(2).AddDays(-1);
            this.dataGridView1.AutoGenerateColumns = false;
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            bindStore();
            BindDgv();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {

                    if (column.Name == "Column1")
                    {
                        #region 箱号不打印
                        String boxNo = this.dataGridView1.CurrentRow.Cells["boxCode"].Value.ToString();
                        string storedName = this.dataGridView1.CurrentRow.Cells["storedName"].Value.ToString();
                        curBoxInfo = new BoxInfo();
                        curBoxInfo.boxCode = boxNo;
                        curBoxInfo.storedName = storedName;

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


                        #endregion
                    }
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    if (column.Name == "Column13")
                    {
                        if (MessageBox.Show("确定要作废当前所选包裹吗?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        }
                        long id = long.Parse(this.dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                        //作废
                        PackageDelRequest delRequest = new PackageDelRequest();
                        delRequest.id = id;
                        delRequest.updateUser = UserInfo.RealName;
                        PackageDelResponse response = client.Execute(delRequest);
                        if (!response.IsError)
                        {
                            BindDgv();
                        }
                        else
                        {
                            MessageBox.Show("作废失败" + response.result);
                        }
                    }
                    if (column.Name == "Column12")
                    {
                        if (packageDetailQueryList != null)
                        {
                            string packageCode = this.dataGridView1.CurrentRow.Cells["packageCode"].Value.ToString();

                            if (this.dataGridView1.CurrentRow.Cells["warehouseName"].Value != null)
                            {
                                wareName = this.dataGridView1.CurrentRow.Cells["warehouseName"].Value.ToString();
                            }

                            //重打印
                            curPackageDetailQuery = packageDetailQueryList.Where(p => p.packageCode == packageCode).FirstOrDefault();

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



        private void pd_PrintBoxPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curBoxInfo.boxCode);
            GetPrintPicture(bt, e, curBoxInfo);
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

            height = image.Height + 5;
            //重量
            layoutRectangle = new RectangleF(100, height, 130f, 30f);
            g.Graphics.DrawString(boxInfo.storedName, fontCu, brush, layoutRectangle);

        }



        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {
            if (curPackageDetailQuery.twoCategoryCode == "0103")
            {
                Font fontCu = new Font("宋体", 12f, FontStyle.Bold);
                int height = 15;
                int heightRight = 15;

                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                int interval = 5;
                int pointX = 35;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(200, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                heightRight = image.Width - 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(wareName, font, brush, layoutRectangleRight);

                heightRight += 20;

                // layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight); 


                heightRight += 15;
                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                // g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);

                RectangleF layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.storedName, fontCu, brush, layoutRectangle);

                height += 10;
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 180f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height - 10, 120f, 40f);
                g.Graphics.DrawString(curPackageDetailQuery.weight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                height += interval + 13;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 80 + image.Height - 15;
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.packageCode, font, brush, layoutRectangle);

                //流通号
                layoutRectangleRight = new RectangleF(pointX, 60, 300f, 85f);
                g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产厂家
                layoutRectangleRight = new RectangleF(pointX, 75, 300f, 85f);
                g.Graphics.DrawString("厂家:" + UserInfo.CompanyName, new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产日期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 110, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString().ToString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                //保质期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 130, 300f, 85f);
                g.Graphics.DrawString("保 质 期: 3天", new Font("宋体", 10f), brush, layoutRectangleRight);
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
                int pointX = 35;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(200, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                heightRight = image.Width - 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(wareName, font, brush, layoutRectangleRight);

                heightRight += 20;

                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight);


                heightRight += 15;
                layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);


                //门店名称


                RectangleF layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.storedName, fontCu, brush, layoutRectangle);


                height += 10;
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 180f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 120f, 40f);
                g.Graphics.DrawString(curPackageDetailQuery.weight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);

                height += interval + 13;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 80 + image.Height - 15;

                layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                g.Graphics.DrawString(curPackageDetailQuery.packageCode, font, brush, layoutRectangle);
            }
        }



        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curPackageDetailQuery.packageCode);
            GetPrintPicture(bt, e);
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


        private void bindStore()
        {
            StoreInfoRequest request = new StoreInfoRequest();
            request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;

            StoreInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<StoreInfo> storeList = new List<StoreInfo>();

                    storeList = response.result;
                    storeList.Insert(0, new StoreInfo() { storedCode = null, storedName = "全部" });
                    this.cbStore.DataSource = storeList;
                    this.cbStore.DisplayMember = "storedName";
                    this.cbStore.ValueMember = "storedCode";
                    cbStore.SelectedIndex = 0;
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                BindDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindDgv()
        {
            PackageDetailQueryRequest request = new PackageDetailQueryRequest();
            if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
            {
                request.skuCode = "%" + tbName.Text.Trim() + "%";
            }
            request.boxCode = tbBoxNo.Text.Trim();

            if (cbStore.SelectedIndex > 0)
            {
                request.storedCode = cbStore.SelectedValue.ToString();
            }
            request.packageCode = tbPackageCode.Text.Trim();
            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            if (cbStatus.SelectedIndex == 0)
            {
                request.status = null;
            }
            if (cbStatus.SelectedIndex == 1)
            {
                request.status = 0;
            }

            if (cbStatus.SelectedIndex == 2)
            {
                request.status = 5;
            }

            if (cbStatus.SelectedIndex == 3)
            {
                request.status = 10;
            }
            //if (cbStatus.SelectedIndex==4)
            //{
            //    request.status = 20;
            //}
            if (cbStatus.SelectedIndex == 4)
            {
                request.status = 90;
            }
            if (cbStatus.SelectedIndex == 5)
            {
                request.status = 30;
            }


            PackageDetailQueryResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
                    return;
                }
                int recordCount = response.pageUtil.totalRow;

                if (recordCount % paginator.PageSize == 0)
                {
                    totalPage = recordCount / paginator.PageSize;
                }
                else
                {
                    totalPage = recordCount / paginator.PageSize + 1;
                }
                packageDetailQueryList = response.result;

                IPagedList<PackageDetailQuery> pageList = new PagedList<PackageDetailQuery>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<PackageDetailQuery>(pageList.ContentList);
                this.dataGridView1.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
            }
            else
            {
                packageDetailQueryList = null;
                this.dataGridView1.DataSource = null;
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                btnImport.Enabled = false;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "包裹明细(*.xls)|*.xls";
                sfd.Title = "导出包裹信息";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                sfd.FileName = "包裹明细" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (packageDetailQueryList == null)
                    {
                        return;
                    }
                    DataTable dtExecl = new DataTable();
                    DataColumn dc1 = new DataColumn("      包装编码       ");
                    dtExecl.Columns.Add(dc1);

                    DataColumn dc2 = new DataColumn("      箱号     ");
                    dtExecl.Columns.Add(dc2);

                    DataColumn dc4 = new DataColumn("      商品编码       ");
                    dtExecl.Columns.Add(dc4);

                    DataColumn dc5 = new DataColumn("      商品名称       ");
                    dtExecl.Columns.Add(dc5);

                    DataColumn dc3 = new DataColumn("重量");
                    dtExecl.Columns.Add(dc3);

                    DataColumn dc6 = new DataColumn("规格");
                    dtExecl.Columns.Add(dc6);

                    DataColumn dc7 = new DataColumn("包装人员");
                    dtExecl.Columns.Add(dc7);


                    DataColumn dc8 = new DataColumn("包装时间");
                    dtExecl.Columns.Add(dc8);


                    DataColumn dc9 = new DataColumn("商户名称");
                    dtExecl.Columns.Add(dc9);

                    DataColumn dc10 = new DataColumn("订单号");
                    dtExecl.Columns.Add(dc10);

                    if (totalPage > 0)
                    {
                        for (int m =1; m <= totalPage; m++)
                        {
                            PackageDetailQueryRequest request = new PackageDetailQueryRequest();
                            if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
                            {
                                request.skuCode = "%" + tbName.Text.Trim() + "%";
                            }
                            request.boxCode = tbBoxNo.Text.Trim();

                            if (cbStore.SelectedIndex > 0)
                            {
                                request.storedCode = cbStore.SelectedValue.ToString();
                            }
                            request.packageCode = tbPackageCode.Text.Trim();
                            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
                            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
                            request.PageIndex = m;
                            request.PageSize = paginator.PageSize;
                            request.partnerCode = UserInfo.PartnerCode;
                            request.customerCode = UserInfo.CustomerCode;
                            request.warehouseCode = UserInfo.WareHouseCode;

                            if (cbStatus.SelectedIndex == 0)
                            {
                                request.status = null;
                            }
                            if (cbStatus.SelectedIndex == 1)
                            {
                                request.status = 0;
                            }

                            if (cbStatus.SelectedIndex == 2)
                            {
                                request.status = 5;
                            }

                            if (cbStatus.SelectedIndex == 3)
                            {
                                request.status = 10;
                            }
                            //if (cbStatus.SelectedIndex==4)
                            //{
                            //    request.status = 20;
                            //}
                            if (cbStatus.SelectedIndex == 4)
                            {
                                request.status = 90;
                            }
                            if (cbStatus.SelectedIndex == 5)
                            {
                                request.status = 30;
                            }


                            PackageDetailQueryResponse response = client.Execute(request);
                            if (!response.IsError)
                            {
                                if (response.result!=null)
                                {
                                    foreach (PackageDetailQuery item in response.result)
                                    {
                                        DataRow dr = dtExecl.NewRow();
                                        dr[0] = item.packageCode;
                                        dr[1] = item.boxCode;
                                        dr[2] = item.skuCode;
                                        dr[3] = item.goodsName;
                                        dr[4] = item.weight;
                                        dr[5] = item.modelNum + "" + item.goodsUnit + "/" + item.physicsUnit;
                                        dr[6] = item.createUser;
                                        dr[7] = item.createTime;
                                        dr[8] = item.storedName;
                                        dr[9] = item.outboundTaskCode;
                                        dtExecl.Rows.Add(dr);
                                    }
                                }
                      
                            }
                        }
                    }
                  
                    NPOIHelper.ExportDTtoExcel(dtExecl, sfd.Title, "", sfd.FileName);

                    MessageBox.Show("导出成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出异常" + ex.Message);
            }
            finally
            {
                btnImport.Enabled = true;
            }
        }

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
        }

        private void tbPackageCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        private void tbBoxNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
    }
}

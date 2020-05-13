using Sdbs.Wms.Controls.Pager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wms.Controls.Pager;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp
{
    public partial class PartnerPackageTaskQueryForm : TabWindow
    {

        int totalPage;

        private PaginatorDTO paginator;

        private SortableBindingList<PackTaskDetail> sortList = null;

        private IWMSClient client = null;

        List<PackTaskDetail> curPackagePackTaskList;


        List<String> colsHeaderText_H = new List<String>();

        public PartnerPackageTaskQueryForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
            colsHeaderText_H.Add("订单总量");
            colsHeaderText_H.Add("实出量");
            colsHeaderText_H.Add("包裹数");

        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            dtBegin.Value = DateTime.Today.AddDays(2).AddDays(-1);
            cbStatus.SelectedIndex = 0;
            this.dataGridView1.AutoGenerateColumns = false;
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            bindStore();
            bindWave();
        }

        private void bindStore()
        {
            StoreInfoRequest request = new StoreInfoRequest();
            request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;

            StoreInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<StoreInfo> storeList = new List<StoreInfo>();
                    storeList = response.result;
                    storeList.Insert(0, new StoreInfo() { storedCode = "", storedName = "--请选择--" });
                    this.cbStore.DataSource = storeList;
                    this.cbStore.DisplayMember = "storedName";
                    this.cbStore.ValueMember = "storedCode";
                    cbStore.SelectedIndex = 0;
                }
            }
        }

        private void bindWave()
        {
            WaveCustomerStoreRequest request = new WaveCustomerStoreRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            request.customerCode = UserInfo.CustomerCode;

            WaveCustomerStoreResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {

                    List<WaveCustomerStoreModel> list = new List<WaveCustomerStoreModel>();
                    list = response.result;
                    list.Insert(0, new WaveCustomerStoreModel() { waveCode = "", waveName = "全部" });

                    this.cbWave.DataSource = list;
                    this.cbWave.ValueMember = "waveCode";
                    this.cbWave.DisplayMember = "waveName";
                    cbWave.SelectedIndex = 0;
                }
            }
        }

        private void BindDgv()
        {
            PartnerPackageTaskRequest request = new PartnerPackageTaskRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
            request.partnerCode = UserInfo.PartnerCode;
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            if (cbWave.SelectedValue.ToString() != "")
            {
                request.waveCode = cbWave.SelectedValue.ToString();
            }

            if (tbName.Text.Trim() != "")
            {
                request.skuCode = "%" + tbName.Text.Trim() + "%";
            }
            request.packTaskCode = tbTaskCode.Text.Trim();
            if (cbStatus.SelectedIndex == 1)
            {
                request.status = 0;
            }
            if (cbStatus.SelectedIndex == 2)
            {
                request.status = 10;
            }
            if (cbStatus.SelectedIndex == 3)
            {
                request.status = 15;
            }
            if (cbStatus.SelectedIndex == 4)
            {
                request.status = 20;
            }

            if (cbStore.SelectedIndex == 0)
            {
                request.storedCode = "";
            }
            else
            {
                request.storedCode = cbStore.SelectedValue.ToString();
            }
            PartnerPackageTaskResponse response = client.Execute(request);

            if (!response.IsError)
            {

                curPackagePackTaskList = response.result;

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
                IPagedList<PackTaskDetail> pageList = new PagedList<PackTaskDetail>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<PackTaskDetail>(pageList.ContentList);
                this.dataGridView1.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    WeightForm weightForm = new WeightForm();
                    if (weightForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
            }
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
            paginator.PageNo = 1;
            BindDgv();
        }

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "供应商包装任务(*.xls)|*.xls";
                sfd.Title = "供应商包装任务";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                sfd.FileName = "供应商包装任务" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    DataTable dtExecl = new DataTable();
                    DataColumn dc0 = new DataColumn("      任务单号       ");
                    dtExecl.Columns.Add(dc0);
                    DataColumn dc1 = new DataColumn("      状态       ");
                    dtExecl.Columns.Add(dc1);
                    DataColumn dc2 = new DataColumn("      商品编码       ");
                    dtExecl.Columns.Add(dc2);
                    DataColumn dc3 = new DataColumn("         商品名称    ");
                    dtExecl.Columns.Add(dc3);
                    DataColumn dc4 = new DataColumn("      门店       ");
                    dtExecl.Columns.Add(dc4);
                    DataColumn dc5 = new DataColumn("      规格     ");
                    dtExecl.Columns.Add(dc5);

                    DataColumn dc6 = new DataColumn("计价单位");
                    dtExecl.Columns.Add(dc6);

                    DataColumn dc7 = new DataColumn("物理单位");
                    dtExecl.Columns.Add(dc7);
                    DataColumn dc8 = new DataColumn("计划量");
                    dtExecl.Columns.Add(dc8);

                    //查询数据
                    if (totalPage > 0)
                    {
                        for (int m = 1; m <= totalPage; m++)
                        {

                            PartnerPackageTaskRequest request = new PartnerPackageTaskRequest();
                            request.PageIndex = m;
                            request.PageSize = 100;
                            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
                            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
                            request.partnerCode = UserInfo.PartnerCode;
                            request.customerCode = UserInfo.CustomerCode;
                            request.warehouseCode = UserInfo.WareHouseCode;

                            if (tbName.Text.Trim() != "")
                            {
                                request.skuCode = "%" + tbName.Text.Trim();
                            }
                            request.packTaskCode = tbTaskCode.Text.Trim();
                            if (cbStatus.SelectedIndex == 1)
                            {
                                request.status = 0;
                            }
                            if (cbStatus.SelectedIndex == 2)
                            {
                                request.status = 10;
                            }
                            if (cbStatus.SelectedIndex == 3)
                            {
                                request.status = 15;
                            }
                            if (cbStatus.SelectedIndex == 4)
                            {
                                request.status = 20;
                            }

                            PartnerPackageTaskResponse response = client.Execute(request);

                            if (response.IsError == false)
                            {
                                if (response.result != null)
                                {

                                    for (int i = 0; i < response.result.Count; i++)
                                    {

                                        DataRow dr = dtExecl.NewRow();
                                        dr[0] = response.result[i].PackTaskCode.ToString();
                                        dr[1] = response.result[i].StatusDes;
                                        dr[2] = response.result[i].skuCode.ToString();
                                        dr[3] = response.result[i].goodsName.ToString();
                                        dr[4] = response.result[i].storedName.ToString();
                                        dr[5] = response.result[i].modelNum.ToString();
                                        dr[6] = response.result[i].goodsUnit;
                                        dr[7] = response.result[i].physicsUnit;
                                        dr[8] = response.result[i].planNum;

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

                MessageBox.Show("异常" + ex.Message);
            }
            finally
            {

            }

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            foreach (string fieldHeaderText in colsHeaderText_H)
            {
                //纵向合并
                if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].HeaderText == fieldHeaderText && e.RowIndex >= 0)
                {
                    using (
                        Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
                        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // 擦除原单元格背景
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            /****** 绘制单元格相互间隔的区分线条，datagridview自己会处理左侧和上边缘的线条，因此只需绘制下边框和和右边框
                             DataGridView控件绘制单元格时，不绘制左边框和上边框，共用左单元格的右边框，上一单元格的下边框*****/

                            //不是最后一行且单元格的值不为null
                            if (e.RowIndex < this.dataGridView1.RowCount - 1 && this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value != null)
                            {
                                //若与下一单元格值不同
                                if (e.Value.ToString() != this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString())
                                {
                                    //下边缘的线
                                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                                    //绘制值
                                    if (e.Value != null)
                                    {
                                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                                            Brushes.Crimson, e.CellBounds.X + 2,
                                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
                                    }
                                }
                                //若与下一单元格值相同 
                                else
                                {
                                    //背景颜色
                                    //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
                                    //  this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                                    // this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                                    //只读（以免双击单元格时显示值）
                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                                    this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = true;
                                }
                            }
                            //最后一行或单元格的值为null
                            else
                            {
                                //下边缘的线
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

                                //绘制值
                                if (e.Value != null)
                                {
                                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                                        Brushes.Crimson, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 2, StringFormat.GenericDefault);
                                }
                            }

                            ////左侧的线（）
                            //e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            //    e.CellBounds.Top, e.CellBounds.Left,
                            //    e.CellBounds.Bottom - 1);

                            //右侧的线
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);

                            //设置处理事件完成（关键点），只有设置为ture,才能显示出想要的结果。
                            e.Handled = true;
                        }
                    }
                }
            }

        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        private void tbTaskCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

    }
}

using Common;
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
    public partial class PackageTaskForm : TabWindow
    {

        int totalPage;

        private PaginatorDTO paginator;

        private SortableBindingList<PackTask> sortList = null;

        private IWMSClient client = null;
        public PackageTaskForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            try
            {

                BindProcess();
                BindWorkShop();
                BindWorkGroup();
                dtBegin.Value = DateTime.Today.AddDays(2).AddDays(-1);
                //   dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

                this.dataGridView1.AutoGenerateColumns = false;
                paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
                cbStatus.SelectedIndex = 0;
                BindDgv();
            }
            catch (Exception ex)
            {
                LogHelper.Log("PackageTaskForm_Load" + ex.Message);
                MessageBox.Show(ex.Message);
            }


        }

        private void BindWorkGroup()
        {
            WarehouseWorkGroupRequest request = new WarehouseWorkGroupRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            WarehouseWorkGroupResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<WarehouseWorkGroup> list = new List<WarehouseWorkGroup>();
                    list = response.result;
                    if (list == null)
                    {
                        list = new List<WarehouseWorkGroup>();
                    }
                    list.Insert(0, new WarehouseWorkGroup() { groupCode = "0", groupName = "全部" });

                    this.cbWorkGroup.DataSource = list;
                    this.cbWorkGroup.ValueMember = "groupCode";
                    this.cbWorkGroup.DisplayMember = "groupName";
                }
            }
        }

        //绑定加工工序
        private void BindProcess()
        {
            ProcessProductRequest request = new ProcessProductRequest();
            ProcessProductResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result!= null)
                {
                    List<Dict> list = new List<Dict>();
                    list = response.result;
                    list.Insert(0, new Dict() { code = "0", name = "全部" });

                    this.cbProcessProduct.DataSource = list;
                    this.cbProcessProduct.ValueMember = "code";
                    this.cbProcessProduct.DisplayMember = "name";
                }
            }
        }

        //绑定车间
        private void BindWorkShop()
        {
            ProductWorkShopRequest request = new ProductWorkShopRequest();
            ProductWorkShopResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<Dict> list = new List<Dict>();
                    list = response.result;
                    list.Insert(0, new Dict() { code = "0", name = "全部" });

                    this.cbWorkShop.DataSource = list;
                    this.cbWorkShop.ValueMember = "code";
                    this.cbWorkShop.DisplayMember = "name";
                }
            }
        }

        private void BindDgv()
        {
            PackTaskRequest request = new PackTaskRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd HH:mm:ss");
            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
            request.partnerCode = UserInfo.PartnerCode;
            if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
            {
                request.skuCode ="%"+tbName.Text.Trim()+"%";
            }
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            request.packTaskType = 10;

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

            if (cbProcessProduct.SelectedIndex!=0)
            {
                request.processProductAttr = int.Parse(cbProcessProduct.SelectedValue.ToString());
            }

            if (cbWorkShop.SelectedIndex!=0)
            {
                request.productWorkshopAttr = int.Parse(cbWorkShop.SelectedValue.ToString());
            }

            if (cbWorkGroup.SelectedIndex!=0)
            {
                request.groupCode = cbWorkGroup.SelectedValue.ToString();
            }

            PackTaskResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
                    MessageBox.Show("没有订单包装任务!");
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

                foreach (PackTask item in response.result)
                {
                    if (item.modelNum > 0)
                    {
                        decimal curValue = item.orderCount / item.modelNum;
                        item.StandNum = (int)curValue;
                    }
                    if (item.orderNum > 0)
                    {
                        item.progressDes = (((double)item.finishNum / item.orderNum) * 100).ToString() + "%";
                    }

                }
                IPagedList<PackTask> pageList = new PagedList<PackTask>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<PackTask>(pageList.ContentList);
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

                    int status = int.Parse(this.dataGridView1.CurrentRow.Cells["status"].Value.ToString());
                    if (status == 15 || status == 20)
                    {
                        MessageBox.Show("当前任务已完成");
                        return;
                    }

                    long id = long.Parse(this.dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    string taskCode = this.dataGridView1.CurrentRow.Cells["PackTaskCode"].Value.ToString();
                    decimal orderCount = decimal.Parse(this.dataGridView1.CurrentRow.Cells["orderCount"].Value.ToString());
                    int standNum = int.Parse(this.dataGridView1.CurrentRow.Cells["StandNum"].Value.ToString());
                    int orderNum = int.Parse(this.dataGridView1.CurrentRow.Cells["orderNum"].Value.ToString());
                    string processDes = this.dataGridView1.CurrentRow.Cells["progressDes"].Value.ToString();

                    string productCurWorkshopAttrDesc = "";
                    if (this.dataGridView1.CurrentRow.Cells["productWorkshopAttrDesc"].Value!=null)
                    {
                        productCurWorkshopAttrDesc= this.dataGridView1.CurrentRow.Cells["productWorkshopAttrDesc"].Value.ToString();
                    }
                    string warehouseName = this.dataGridView1.CurrentRow.Cells["warehouseName"].Value == null ? "" : this.dataGridView1.CurrentRow.Cells["warehouseName"].Value.ToString();
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    if (UserInfo.CustomerCode == "30001" || UserInfo.CustomerCode == "23001" || UserInfo.CustomerCode == "34001"
                        || UserInfo.CustomerCode == "20001" || UserInfo.CustomerCode == "24001" || UserInfo.CustomerCode == "24002"
                        )
                    {
                        WeightForm weightForm = new WeightForm(id, taskCode, orderCount, standNum, processDes, orderNum, this.dtBegin.Value, productCurWorkshopAttrDesc);
                        weightForm.wareHouseName = warehouseName;
                        if (weightForm.ShowDialog() == DialogResult.OK)
                        {
                            BindDgv();
                        }
                    }
                    else
                    {
                        WeightForm weightForm = new WeightForm(id, taskCode, orderCount, standNum, processDes, orderNum, productCurWorkshopAttrDesc,this.dtBegin.Value);
                        weightForm.wareHouseName = warehouseName;
                        if (weightForm.ShowDialog() == DialogResult.OK)
                        {
                            BindDgv();
                        }
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

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "订单包装(*.xls)|*.xls";
                sfd.Title = "订单包装";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                sfd.FileName = "订单包装" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    DataTable dtExecl = new DataTable();
                    DataColumn dc0 = new DataColumn("      任务单号       ");
                    dtExecl.Columns.Add(dc0);
                    DataColumn dc1 = new DataColumn("      状态       ");
                    dtExecl.Columns.Add(dc1);
                    DataColumn dc2 = new DataColumn("      总进度       ");
                    dtExecl.Columns.Add(dc2);
                    DataColumn dc3 = new DataColumn("商品编码");
                    dtExecl.Columns.Add(dc3);
                    DataColumn dc4 = new DataColumn("      商品名称       ");
                    dtExecl.Columns.Add(dc4);
                    DataColumn dc5 = new DataColumn("      规格     ");
                    dtExecl.Columns.Add(dc5);

                    DataColumn dc6 = new DataColumn("单位");
                    dtExecl.Columns.Add(dc6);

                    DataColumn dc7 = new DataColumn("物理单位");
                    dtExecl.Columns.Add(dc7);
                    DataColumn dc8 = new DataColumn("订单总量");
                    dtExecl.Columns.Add(dc8);
                    DataColumn dc9 = new DataColumn("订单数");
                    dtExecl.Columns.Add(dc9);
                    DataColumn dc10 = new DataColumn("已完成");
                    dtExecl.Columns.Add(dc10);

                    DataColumn dc12 = new DataColumn("标准包数");
                    dtExecl.Columns.Add(dc12);


                    DataColumn dc13 = new DataColumn("加工工序");
                    dtExecl.Columns.Add(dc13);

                    DataColumn dc14 = new DataColumn("生产车间");
                    dtExecl.Columns.Add(dc14);

                    DataColumn dc15 = new DataColumn("加工小组");
                    dtExecl.Columns.Add(dc15);


                    List<PackTask> ptList = new List<PackTask>();
                    if (totalPage > 0)
                    {
                        for (int m =1; m <=totalPage; m++)
                        {
                            PackTaskRequest request = new PackTaskRequest();
                            request.PageIndex = m;
                            request.PageSize = paginator.PageSize;
                            request.startTime = dtBegin.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
                            request.partnerCode = UserInfo.PartnerCode;
                            request.skuCode = tbName.Text.Trim();
                            request.packTaskType = 10;
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

                            PackTaskResponse response = client.Execute(request);
                            if (!response.IsError)
                            {

                                foreach (PackTask item in response.result)
                                {
                                    if (item.modelNum > 0)
                                    {
                                        decimal curValue = item.orderCount / item.modelNum;
                                        item.StandNum = (int)curValue;
                                    }
                                    if (item.orderNum > 0)
                                    {
                                        item.progressDes = (((double)item.finishNum / item.orderNum) * 100).ToString() + "%";
                                    }
                                    ptList.Add(item);
                                }

                                
                            }


                        }
                    }

                    if (ptList.Count>0)
                    {
                        foreach (var item in ptList)
                        {
                            DataRow dr = dtExecl.NewRow();
                            dr[0] = item.PackTaskCode;
                            dr[1] = item.statusdes;
                            dr[2] = item.progressDes;
                            dr[3] = item.skuCode;
                            dr[4] = item.goodsName;
                            dr[5] = item.modelNum;
                            dr[6] = item.goodsUnit;
                            dr[7] = item.physicsUnit;
                            dr[8] = item.orderCount;
                            dr[9] = item.orderNum;
                            dr[10] = item.finishNum;
                            dr[11] = item.StandNum;

                            dr[12] = item.processProductAttrDesc;
                            dr[13] = item.productWorkshopAttrDesc;
                            dr[14] = item.groupName;
                            dtExecl.Rows.Add(dr);
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


    }
}

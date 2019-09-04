using Common;
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
using System.Threading.Tasks;
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
    public partial class PrePackageForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<Goods> sortList = null;

        List<Goods> goodsList;
        private IWMSClient client = null;

        private PreprocessInfo curPreprocessInfo;
        Goods goods;

        public PrePackageForm()
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


            dtBegin.Value = DateTime.Today.AddDays(2).AddDays(-1);
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };

            BindProcess();
            BindWorkShop();
            BindWorkGroup();
            string name = tbName.Text.Trim();

            int? processProduct = null;
            int? workShop = null;
            if (cbProcessProduct.SelectedIndex != 0)
            {
                processProduct = int.Parse(cbProcessProduct.SelectedValue.ToString());
            }

            if (cbWorkShop.SelectedIndex != 0)
            {
                workShop = int.Parse(cbWorkShop.SelectedValue.ToString());
            }

            int? isStand = null;
            if (chk.Checked)
            {
                isStand = 1;
            }

            string groupCode = "";
            if (cbWorkGroup.SelectedIndex != 0)
            {
                groupCode = this.cbWorkGroup.SelectedValue.ToString();
            }

            string startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");

            Task.Factory.StartNew(() =>
            {
                // btnQuery_Click(null,null);
                QueryData(name, processProduct, workShop, isStand, groupCode,startTime,endTime);
            });
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
        private void BindProcess()
        {
            ProcessProductRequest request = new ProcessProductRequest();
            ProcessProductResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
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
                    if (list==null)
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

        private void BindDgv(string name, int? productprocess, int? workshop, int? isStand,string groupCode,string startTime,string endTime)
        {
            GoodsRequest request = new GoodsRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            //request.skuCode= "%"+tbName.Text.Trim()+"%";
            //if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
            //{
            //    request.goodsName = "%" + tbName.Text.Trim() + "%";
            //}

            if (!string.IsNullOrWhiteSpace(name))
            {
                request.goodsName = "%" + name + "%";
            }

            request.groupCode = groupCode;

            request.partnerCode = UserInfo.PartnerCode;
            request.isPreprocess = 1;
            request.isFresh = 1;
            //request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            //request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");

            request.startTime = startTime;
            request.endTime = endTime;

            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;

            request.processProductAttr = productprocess;
            request.productWorkshopAttr = workshop;

            request.isStandardProcess = isStand;


          
            GoodsResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        this.dataGridView1.DataSource = null;
                        pageSplit1.Description = "共查询到0条记录";
                        pageSplit1.PageCount =0;
                        pageSplit1.PageNo = 0;
                        pageSplit1.DataBind();
                    }));
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

                    goodsList = response.result;

                    if (goodsList != null)
                    {
                        foreach (Goods item in goodsList)
                        {
                            if (item.orderNum == 0)
                            {
                                item.diffNum = 0;
                            }
                            else
                            {
                                item.diffNum = item.orderNum.Value - (int)item.packageNum;
                            }
                        }
                    }
                    IPagedList<Goods> pageList = new PagedList<Goods>(goodsList.OrderByDescending(p => p.orderNum), recordCount, totalPage);
                    sortList = new SortableBindingList<Goods>(pageList.ContentList);


                    this.Invoke(new MethodInvoker(delegate()
                    {
                        this.dataGridView1.DataSource = sortList;
                        pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                        pageSplit1.PageCount = pageList.PageCount;
                        pageSplit1.PageNo = paginator.PageNo;
                        pageSplit1.DataBind();
                    }));


                }

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {


                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    //if (UserInfo.CustomerCode == "7001")
                    //{
                    //    MessageBox.Show("当前货主无权操作!");
                    //    return;
                    //}

                    string skucode = this.dataGridView1.CurrentRow.Cells["skuCode"].Value.ToString();
                    string goodsName = this.dataGridView1.CurrentRow.Cells["goodsName"].Value.ToString();

                    string orderNum = "";
                    if (this.dataGridView1.CurrentRow.Cells["orderNum"].Value != null)
                    {
                        orderNum = this.dataGridView1.CurrentRow.Cells["orderNum"].Value.ToString();
                    }

                    // int weighted =int.Parse(this.dataGridView1.CurrentRow.Cells["weighed"].Value.ToString());
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    Goods goods = goodsList.Where(p => p.skuCode == skucode).FirstOrDefault();
                    PreWeightForm weightForm = new PreWeightForm(goods, dtBegin.Value);
                    weightForm.orderNum = orderNum;

                    if (weightForm.ShowDialog() == DialogResult.OK)
                    {

                    }

                    //Task.Factory.StartNew(() =>
                    //{
                    //    btnQuery_Click(null, null);
                    //});

                    string name = tbName.Text.Trim();

                    int? processProduct = null;
                    int? workShop = null;
                    if (cbProcessProduct.SelectedIndex != 0)
                    {
                        processProduct = int.Parse(cbProcessProduct.SelectedValue.ToString());
                    }

                    if (cbWorkShop.SelectedIndex != 0)
                    {
                        workShop = int.Parse(cbWorkShop.SelectedValue.ToString());
                    }

                    int? isStand = null;
                    if (chk.Checked)
                    {
                        isStand = 1;
                    }

                    string groupCode = "";
                    if (cbWorkGroup.SelectedIndex != 0)
                    {
                        groupCode = this.cbWorkGroup.SelectedValue.ToString();
                    }

                    string startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
                    string endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");

                    Task.Factory.StartNew(() =>
                    {
                        // btnQuery_Click(null,null);
                        QueryData(name, processProduct, workShop, isStand, groupCode,startTime,endTime);
                    });

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

            string name = tbName.Text.Trim();

            int? processProduct = null;
            int? workShop = null;
            if (cbProcessProduct.SelectedIndex != 0)
            {
                processProduct = int.Parse(cbProcessProduct.SelectedValue.ToString());
            }

            if (cbWorkShop.SelectedIndex != 0)
            {
                workShop = int.Parse(cbWorkShop.SelectedValue.ToString());
            }

            int? isStand = null;
            if (chk.Checked)
            {
                isStand = 1;
            }

            string groupCode = "";
            if (cbWorkGroup.SelectedIndex!=0)
            {
                 groupCode = this.cbWorkGroup.SelectedValue.ToString();
            }

            string startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");

            Task.Factory.StartNew(() =>
            {

                try
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        btnQuery.Text = "正在查询";
                        btnQuery.Enabled = false;
                    }));
                    paginator.PageNo = 1;
                    BindDgv(name, processProduct, workShop, isStand, groupCode, startTime, endTime);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询异常" + ex.Message);
                    LogHelper.Log("btnQuery_Click" + ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        btnQuery.Enabled = true;
                        btnQuery.Text = "查询";
                    }));

                }
            });


        }


        private void QueryData(string name, int? processProduct, int? workShop, int? isStand,string groupCode,string startTime,string endTime)
        {

            // string name = tbName.Text.Trim();

            //int? processProduct = null;
            //   int? workShop = null;
            //if (cbProcessProduct.SelectedIndex != 0)
            //{
            //    processProduct = int.Parse(cbProcessProduct.SelectedValue.ToString());
            //}

            //if (cbWorkShop.SelectedIndex != 0)
            //{
            //    workShop = int.Parse(cbWorkShop.SelectedValue.ToString());
            //}

            //int? isStand = null;
            //if (chk.Checked)
            //{
            //    isStand = 1;
            //}

            Task.Factory.StartNew(() =>
            {

                try
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        btnQuery.Text = "正在查询";
                        btnQuery.Enabled = false;
                    }));
                    paginator.PageNo = 1;
                    BindDgv(name, processProduct, workShop, isStand, groupCode,startTime,endTime);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询异常" + ex.Message);
                    LogHelper.Log("btnQuery_Click" + ex.Message);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        btnQuery.Enabled = true;
                        btnQuery.Text = "查询";
                    }));

                }
            });


        }

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            int? processProduct = null;
            int? workShop = null;
            if (cbProcessProduct.SelectedIndex != 0)
            {
                processProduct = int.Parse(cbProcessProduct.SelectedValue.ToString());
            }

            if (cbWorkShop.SelectedIndex != 0)
            {
                workShop = int.Parse(cbWorkShop.SelectedValue.ToString());
            }

            int? isStand = null;
            if (chk.Checked)
            {
                isStand = 1;
            }
            string groupCode = "";
            if (cbWorkGroup.SelectedIndex != 0)
            {
                groupCode = this.cbWorkGroup.SelectedValue.ToString();
            }
            string startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");

            BindDgv(tbName.Text.Trim(), processProduct, workShop, isStand, groupCode,startTime,endTime);
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
                sfd.Filter = "商品包装(*.xls)|*.xls";
                sfd.Title = "商品包装";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                sfd.FileName = "商品包装" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    DataTable dtExecl = new DataTable();

                    DataColumn dc3 = new DataColumn("商品编码");
                    dtExecl.Columns.Add(dc3);
                    DataColumn dc4 = new DataColumn("      商品名称       ");
                    dtExecl.Columns.Add(dc4);
                    DataColumn dc5 = new DataColumn("      规格     ");
                    dtExecl.Columns.Add(dc5);

                    DataColumn dc6 = new DataColumn("计价单位");
                    dtExecl.Columns.Add(dc6);

                    DataColumn dc7 = new DataColumn("物理单位");
                    dtExecl.Columns.Add(dc7);
                    DataColumn dc8 = new DataColumn("包装规格");
                    dtExecl.Columns.Add(dc8);
                    DataColumn dc9 = new DataColumn("订单需求数量");
                    dtExecl.Columns.Add(dc9);

                    DataColumn dc10 = new DataColumn("包装数量");
                    dtExecl.Columns.Add(dc10);


                    DataColumn dc11 = new DataColumn("加工工序");
                    dtExecl.Columns.Add(dc11);

                    DataColumn dc12 = new DataColumn("生产车间");
                    dtExecl.Columns.Add(dc12);

                    GoodsRequest request = new GoodsRequest();
                    request.PageIndex = 1;
                    request.PageSize = 500;
                    if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
                    {
                        request.goodsName = "%" + tbName.Text.Trim() + "%";
                    }
                    request.partnerCode = UserInfo.PartnerCode;
                    request.isPreprocess = 1;
                    request.isFresh = 1;
                    request.startTime = dtBegin.Value.ToString("yyyy-MM-dd 00:00:00");
                    request.endTime = dtBegin.Value.ToString("yyyy-MM-dd 23:59:59");
                    request.customerCode = UserInfo.CustomerCode;
                    request.warehouseCode = UserInfo.WareHouseCode;
                    GoodsResponse response = client.Execute(request);
                    if (!response.IsError)
                    {
                        if (response.result != null)
                        {
                            for (int i = 0; i < response.result.Count; i++)
                            {

                                DataRow dr = dtExecl.NewRow();
                                dr[0] = response.result[i].skuCode.ToString();
                                dr[1] = response.result[i].goodsName.ToString();
                                dr[2] = response.result[i].goodsModel == null ? "" : response.result[i].goodsModel.ToString();
                                dr[3] = response.result[i].goodsUnit == null ? "" : response.result[i].goodsUnit.ToString();
                                dr[4] = response.result[i].physicsUnit == null ? "" : response.result[i].physicsUnit.ToString();
                                dr[5] = response.result[i].modelNum == null ? "" : response.result[i].modelNum.ToString();
                                dr[6] = response.result[i].orderNum == null ? "" : response.result[i].orderNum.ToString();
                                dr[7] = response.result[i].packageNum == null ? "" : response.result[i].packageNum.ToString();
                                dr[8] = response.result[i].processProductAttrDesc == null ? "" : response.result[i].processProductAttrDesc.ToString();
                                dr[9] = response.result[i].productWorkshopAttrDesc == null ? "" : response.result[i].productWorkshopAttrDesc.ToString();
                                dtExecl.Rows.Add(dr);
                            }
                        }
                    }

                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{

                    //    DataRow dr = dtExecl.NewRow();
                    //    dr[0] = dataGridView1.Rows[i].Cells["skuCode"].Value.ToString();
                    //    dr[1] = dataGridView1.Rows[i].Cells["goodsName"].Value.ToString();
                    //    dr[2] = dataGridView1.Rows[i].Cells["goodsModel"].Value==null?"":dataGridView1.Rows[i].Cells["goodsModel"].Value.ToString();
                    //    dr[3] = dataGridView1.Rows[i].Cells["goodsUnit"].Value==null?"":dataGridView1.Rows[i].Cells["goodsUnit"].Value.ToString();
                    //    dr[4] = dataGridView1.Rows[i].Cells["physicsUnit"].Value==null?"":dataGridView1.Rows[i].Cells["physicsUnit"].Value.ToString();
                    //    dr[5] = dataGridView1.Rows[i].Cells["modelNum"].Value == null ? "" : dataGridView1.Rows[i].Cells["modelNum"].Value.ToString();
                    //    dr[6] = dataGridView1.Rows[i].Cells["orderNum"].Value==null?"":dataGridView1.Rows[i].Cells["orderNum"].Value.ToString();
                    //    dr[7] =dataGridView1.Rows[i].Cells["packageNum"].Value==null?"":dataGridView1.Rows[i].Cells["packageNum"].Value.ToString();

                    //    dtExecl.Rows.Add(dr);
                    //}



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

        private List<PreprocessInfo> preprocessInfoList;
        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("请选择要打印的商品");
                    return;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        if (this.dataGridView1.Rows[i].Cells["isStandardProcess"].Value == null)
                        {
                            continue;
                        }
                        if (this.dataGridView1.Rows[i].Cells["isStandardProcess"].Value.ToString() == "1")
                        {
                            string skuCode = this.dataGridView1.Rows[i].Cells["skuCode"].Value.ToString();

                            int orderNum = 0;
                            if (this.dataGridView1.Rows[i].Cells["orderNum"].Value != null)
                            {
                                orderNum = int.Parse(this.dataGridView1.Rows[i].Cells["orderNum"].Value.ToString());
                            }
                            int packageNum = 0;
                            if (this.dataGridView1.Rows[i].Cells["packageNum"].Value != null)
                            {
                                packageNum = int.Parse(this.dataGridView1.Rows[i].Cells["packageNum"].Value.ToString());
                            }

                            if ((orderNum - packageNum) <= 0)
                            {
                                LogHelper.Log(skuCode + ",没有订单量," + orderNum + "--" + packageNum);
                                continue;
                            }

                            getCustomerInfo(skuCode);

                            System.Threading.Thread.Sleep(200);
                            //打印
                            //if (this.dataGridView1.Rows[i].Cells["weighed"].Value != null && int.Parse(this.dataGridView1.Rows[i].Cells["weighed"].Value.ToString()) == 1)
                            //{
                            LogHelper.Log(skuCode + ",订单量:" + orderNum + ",包装量:" + packageNum);

                            string goodsModel = this.dataGridView1.Rows[i].Cells["goodsModel"].Value.ToString();
                            string goodsName = this.dataGridView1.Rows[i].Cells["goodsName"].Value.ToString();
                            string _productWorkshopAttrDesc = "";

                            if (this.dataGridView1.Rows[i].Cells["productWorkshopAttrDesc"].Value != null)
                            {
                                _productWorkshopAttrDesc = this.dataGridView1.Rows[i].Cells["productWorkshopAttrDesc"].Value.ToString();
                            }

                            int diff = orderNum - packageNum;
                            goods = goodsList.Where(p => p.skuCode == skuCode).FirstOrDefault();
                            List<PreprocessInfoAdd> list = new List<PreprocessInfoAdd>();
                            for (int n = 0; n < diff; n++)
                            {
                                PreprocessInfoAdd add = new PreprocessInfoAdd();
                                add.createUser = UserInfo.RealName;
                                add.goodsName = goods.shortName;
                                add.goodsUnit = goods.goodsUnit;
                                add.modelNum = goods.modelNum;
                                add.operateUser = UserInfo.RealName;
                                add.packWeight = goods.modelNum;
                                add.partnerCode = UserInfo.PartnerCode;
                                add.partnerName = UserInfo.PartnerName;
                                add.physicsUnit = goods.physicsUnit;
                                add.skuCode = goods.skuCode;
                                add.status = 0;
                                add.updateUser = UserInfo.RealName;
                                list.Add(add);
                            }
                            if (UserInfo.CustomerCode != "7001" && UserInfo.CustomerCode != "7002")
                            {
                                if (UserInfo.CustomerCode == "15001" || UserInfo.CustomerCode == "11001")
                                {
                                    #region 青柠泰和青年餐厅

                                    PreprocessInfoRequest request = new PreprocessInfoRequest();
                                    request.wareHouseId = UserInfo.WareHouseCode;
                                    request.warehouseCode = UserInfo.WareHouseCode;
                                    request.warehouseName = UserInfo.WareHouseName;
                                    request.customerCode = UserInfo.CustomerCode;
                                    request.customerName = UserInfo.CustomerName;
                                    request.request = list;
                                    PreprocessInfoAddResponse response = client.Execute(request);
                                    if (!response.IsError)
                                    {
                                        if (response.result != null)
                                        {
                                            preprocessInfoList = response.result;
                                            foreach (PreprocessInfo item in preprocessInfoList)
                                            {
                                                #region 打印
                                                curPreprocessInfo = item;
                                                PrintDocument document = new PrintDocument();
                                                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                                                document.OriginAtMargins = true;
                                                document.DefaultPageSettings.Landscape = false;
                                                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
                                dialog.Document = document;
#else
                                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
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
                                        }
                                    }

                                    #endregion
                                }
                                else
                                { 
                            
                                #region 正常客户


                                //称重商品
                                PreprocessInfoRequest request = new PreprocessInfoRequest();
                                request.wareHouseId = UserInfo.WareHouseCode;
                                request.warehouseCode = UserInfo.WareHouseCode;
                                request.warehouseName = UserInfo.WareHouseName;
                                request.customerCode = UserInfo.CustomerCode;
                                request.customerName = UserInfo.CustomerName;
                                request.request = list;
                                PreprocessInfoAddResponse response = client.Execute(request);
                                if (!response.IsError)
                                {
                                    if (response.result != null)
                                    {
                                        preprocessInfoList = response.result;
                                        foreach (PreprocessInfo item in preprocessInfoList)
                                        {
                                            #region 打印
                                            curPreprocessInfo = item;
                                            PrintDocument document = new PrintDocument();
                                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                                            document.OriginAtMargins = true;
                                            document.DefaultPageSettings.Landscape = false;

                                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
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
                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                }
                            }
                            else
                            {
                                #region 西贝客户
                                PreprocessXiBeiInfoRequest request = new PreprocessXiBeiInfoRequest();
                                request.wareHouseId = UserInfo.WareHouseCode;
                                request.warehouseCode = UserInfo.WareHouseCode;
                                request.warehouseName = UserInfo.WareHouseName;
                                request.customerCode = UserInfo.CustomerCode;
                                request.customerName = UserInfo.CustomerName;
                                request.request = list;
                                PreprocessInfoAddResponse response = client.Execute(request);
                                if (!response.IsError)
                                {
                                    if (response.result != null)
                                    {
                                        preprocessInfoList = response.result;
                                        foreach (PreprocessInfo item in preprocessInfoList)
                                        {
                                            item.goodsModel = goodsModel;
                                            item.productWorkshopAttrDesc = _productWorkshopAttrDesc;
                                            #region 打印
                                            curPreprocessInfo = item;
                                            PrintDocument document = new PrintDocument();
                                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                                            document.OriginAtMargins = true;
                                            document.DefaultPageSettings.Landscape = false;

                                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
                                dialog.Document = document;
#else
                                            PrintPreviewDialog dialog = new PrintPreviewDialog();
                                            document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
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
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("出现错误:" + goodsName + response.Message);
                                }

                                #endregion
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.Message);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curPreprocessInfo.preprocessCode);
            GetPrintPicture(bt, e, curPreprocessInfo);
        }


        private void pd_PrintQingPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQingQRCode(curPreprocessInfo.preprocessCode);
            GetPrintQingPicture(bt, e, curPreprocessInfo);
        }

        private void pd_XiBeiPrintPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateXiBeiQRCode(curPreprocessInfo.preprocessCode);
            GetXiBeiPrintPicture(bt, e, curPreprocessInfo);
        }

        public static Bitmap CreateQingQRCode(string asset)
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


        private Bitmap CreateCircle(string text)
        {
            Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics gBmp = Graphics.FromImage(bmp);//内存位图

            for (int i = 1; i < 20; i++)
            {
                System.Drawing.Font f = new Font("宋体", i, FontStyle.Regular);
                gBmp.DrawString("this is a test", f, Brushes.Black, new PointF(0f, i * Font.Height));
            }

            return bmp;
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


        public static Bitmap CreateXiBeiQRCode(string asset)
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


        double expireDay = 0;
        string handleWay = "";
        private void getCustomerInfo(string skuCode)
        {
            expireDay = 0;
            handleWay = "";
            CustomerGoodsBySkuRequest request = new CustomerGoodsBySkuRequest();
            request.customerCode = UserInfo.CustomerCode;
            request.skuCode = skuCode;
            CustomerGoodsBySkuResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    expireDay = response.result.expiryDate;
                    handleWay = response.result.handleWay;
                }

            }
        }


        public void GetPrintQingPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {
          
            if (goods.categoryCode == "10")
            {

                #region 分类为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;
               

                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);
                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

             
               

                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height,200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height +=15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height +=15;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height +=15;
                //生产日期
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);

                    height +=15;
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString("保质期:" + expireDay + "天 ", new Font("宋体", 10f), brush, layoutRectangleRight);

                    height += 15;
                    //编码
                    Font fontCode = new Font("宋体", 8f);
                    layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                    g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);

                height +=20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);



                height +=15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }
             

                #endregion
            }
            else
            {

                #region 分类不为10

                Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
                Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
                int height = 2;


                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

                Rectangle destRect = new Rectangle(145, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);



                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
                //商品名称
                layoutRectangle = new RectangleF(pointX, height, 200f, 30f);
                g.Graphics.DrawString("" + preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

                height += 15;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 125f, 40f);

                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }

                height += 20;


                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                //编码
                Font fontCode = new Font("宋体", 8f);
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangleRight);

          

                height += 60;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 10f), brush, layoutRectangleRight);



                height += 20;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 10f), brush, layoutRectangleRight);

                height += 15;
                if (UserInfo.CustomerCode == "11001")
                {
                    if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                    {
                        layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                        g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                    }
                }
              
            

                #endregion
            }
        }


        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {
            // if (goods.categoryCode == "10" || goods.categoryCode == "11" || goods.categoryCode == "12" || goods.categoryCode == "13" || goods.categoryCode == "17" || goods.categoryCode == "20")
            //if (goods.twoCategoryCode == "0103")
            if (goods.categoryCode == "10")
            {
                Font fontCu = new Font("宋体", 12f, FontStyle.Bold);
                int height = 15;
                int heightRight = 15;

                Font font = new Font("宋体", 10f);
                Brush brush = new SolidBrush(Color.Black);
                g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                int interval = 5;
                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                //heightRight =image.Width-20;

                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.CompanyName, font, brush, layoutRectangleRight);

                heightRight += 40;

                //流通号
                layoutRectangleRight = new RectangleF(pointX, 55, 300f, 85f);
                g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产厂家
                layoutRectangleRight = new RectangleF(pointX, 70, 300f, 85f);
                g.Graphics.DrawString("厂家:" + UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);


                heightRight += 15;
                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);


                //门店名称

                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);



                //商品名称
                layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(preprocessInfo.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height - 15, 120f, 40f);
                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    // g.Graphics.DrawString("1" + goods.physicsUnit+"/"+goods.modelNum+goods.goodsUnit, fontCu, brush, layoutRectangle);
                    // g.Graphics.DrawString(goods.goodsModel, fontCu, brush, layoutRectangle);
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                    // g.Graphics.DrawString(int.Parse(goods.modelNum.ToString()) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }


                height += interval;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 63 + image.Height;

                Font fontCode = new Font("宋体", 8f);

                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangle);


                //货主
                layoutRectangleRight = new RectangleF(pointX + image.Width, 86, 300f, 85f);
                g.Graphics.DrawString("货主:" + UserInfo.CustomerName, new Font("宋体", 8f), brush, layoutRectangleRight);

                //生产日期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 105, 300f, 85f);
                g.Graphics.DrawString("生产日期:" + DateTime.Now.ToShortDateString().ToString(), new Font("宋体", 8f), brush, layoutRectangleRight);

                //保质期
                layoutRectangleRight = new RectangleF(pointX + image.Width, 125, 300f, 85f);
                g.Graphics.DrawString("保 质 期: " + expireDay + "天", new Font("宋体", 8f), brush, layoutRectangleRight);


                //if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                //{
                //    layoutRectangleRight = new RectangleF(pointX + image.Width, 140, 300f, 85f);
                //    g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                //}
             

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
                int pointX = 5;

                RectangleF layoutRectangleRight = new RectangleF(135f, 5, 130f, 85f);
                //g.Graphics.DrawString(preprocessInfo.preprocessCode, font, brush, layoutRectangleRight);

                Rectangle destRect = new Rectangle(160, -15, image.Width, image.Height);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                //heightRight =image.Width-20;

                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(UserInfo.CompanyName, font, brush, layoutRectangleRight);

                heightRight += 40;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString(UserInfo.RealName, font, brush, layoutRectangleRight);

                heightRight += 40;

                layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                g.Graphics.DrawString("货主" + UserInfo.CustomerName, font, brush, layoutRectangleRight);


                heightRight += 10;
                //layoutRectangleRight = new RectangleF(155, heightRight, 150f, 85f);
                //g.Graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), font, brush, layoutRectangleRight);

                if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
                {
                    //layoutRectangleRight = new RectangleF(135, heightRight, 150f, 85f);
                    //g.Graphics.DrawString("" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
                }

                //门店名称

                RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);



                //商品名称
                layoutRectangle = new RectangleF(pointX, 5, 180f, 30f);
                g.Graphics.DrawString(preprocessInfo.goodsName, font, brush, layoutRectangle);

                height += interval + 20;
                //重量

                layoutRectangle = new RectangleF(pointX, height, 120f, 40f);
                if (goods.weighed == 1)
                {
                    g.Graphics.DrawString(preprocessInfo.packWeight.ToString("f2") + "斤", fontCu, brush, layoutRectangle);
                }
                else
                {
                    // g.Graphics.DrawString("1" + goods.physicsUnit+"/"+goods.modelNum+goods.goodsUnit, fontCu, brush, layoutRectangle);
                    // g.Graphics.DrawString(goods.goodsModel, fontCu, brush, layoutRectangle);
                    g.Graphics.DrawString(Decimal.ToInt32(goods.modelNum) + goods.goodsUnit, fontCu, brush, layoutRectangle);
                }


                height += interval;

                Rectangle dest2Rect = new Rectangle(pointX, 80, image.Width, image.Height);
                g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                height = 63 + image.Height;
                Font fontCode = new Font("宋体", 8f);
                layoutRectangle = new RectangleF(pointX, height, 150f, 30f);
                g.Graphics.DrawString(preprocessInfo.preprocessCode, fontCode, brush, layoutRectangle);
            }
        }


        public void GetXiBeiPrintPicture(Bitmap image, PrintPageEventArgs g, PreprocessInfo preprocessInfo)
        {

            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = 15;
            int heightRight = 15;

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = 5;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(145, -5, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            heightRight += 40;


            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);

            //绘制加工方式
            if (handleWay!=null&&handleWay.Trim() != "")
            {
                Pen pen = new Pen(Color.Black, 1);
                g.Graphics.DrawEllipse(pen, 5, 5, 25, 25);
                g.Graphics.DrawString(handleWay.Trim(), new Font("宋体", 12f,FontStyle.Bold), brush, 7, 10);
               
            }
          

            //商品名称
            layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            g.Graphics.DrawString("   "+preprocessInfo.goodsName, fontCu11, brush, layoutRectangle);

            height += 30;
            //重量

            layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            g.Graphics.DrawString(preprocessInfo.goodsModel, font, brush, layoutRectangle);

            height += 15;


            //生产日期
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:" + dtBegin.Value.ToShortDateString(), new Font("宋体", 10f), brush, layoutRectangleRight);



            if (preprocessInfo.productWorkshopAttrDesc != null && preprocessInfo.productWorkshopAttrDesc != "" && preprocessInfo.productWorkshopAttrDesc != "净毛菜车间" && preprocessInfo.productWorkshopAttrDesc != "库房车间")
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("保质期:" + expireDay + "天 " + "储存方式:0-8℃", new Font("宋体", 8f), brush, layoutRectangleRight);
            }
            else
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString("储存方式:" + "0-8℃", new Font("宋体", 10f), brush, layoutRectangleRight);
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("产地:" + UserInfo.areaName, new Font("宋体", 8f), brush, layoutRectangleRight);

            

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
           
            if (goods != null && !string.IsNullOrWhiteSpace(goods.foodWay))
            {
                g.Graphics.DrawString("食用方法:" + goods.foodWay, new Font("宋体", 8f), brush, layoutRectangleRight);
            }
            else
            {
                g.Graphics.DrawString("食用方法:", new Font("宋体", 8f), brush, layoutRectangleRight);
            }


            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("食品经营许可证号：JY11117051464030", new Font("宋体", 8f), brush, layoutRectangleRight);



            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(string.IsNullOrWhiteSpace(UserInfo.labelName) ? UserInfo.PartnerName : UserInfo.labelName, new Font("宋体", 8f), brush, layoutRectangleRight);


        }
        private void btnInput_Click(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("请选择要打印的商品");
                    return;
                }
                InputNumForm form = new InputNumForm();
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                  
                        if (this.dataGridView1.Rows[i].Cells["isStandardProcess"].Value != null && this.dataGridView1.Rows[i].Cells["isStandardProcess"].Value.ToString() == "1")
                        {
                            string skuCode = this.dataGridView1.Rows[i].Cells["skuCode"].Value.ToString();
                            getCustomerInfo(skuCode);
                            goods = goodsList.Where(p => p.skuCode == skuCode).FirstOrDefault();

                            List<PreprocessInfoAdd> list = new List<PreprocessInfoAdd>();
                            for (int n = 0; n < form.num; n++)
                            {
                                PreprocessInfoAdd add = new PreprocessInfoAdd();
                                add.createUser = UserInfo.RealName;
                                add.goodsName = goods.shortName;
                                add.goodsUnit = goods.goodsUnit;
                                add.modelNum = goods.modelNum;
                                add.operateUser = UserInfo.RealName;
                                add.packWeight = goods.modelNum;
                                add.partnerCode = UserInfo.PartnerCode;
                                add.partnerName = UserInfo.PartnerName;
                                add.physicsUnit = goods.physicsUnit;
                                add.skuCode = goods.skuCode;
                                add.status = 0;
                                add.updateUser = UserInfo.RealName;
                                list.Add(add);
                            }

                            string goodsModel = this.dataGridView1.Rows[i].Cells["goodsModel"].Value.ToString();
                            string goodsName = this.dataGridView1.Rows[i].Cells["goodsName"].Value.ToString();
                            string _productWorkshopAttrDesc = "";
                            if (this.dataGridView1.Rows[i].Cells["productWorkshopAttrDesc"].Value != null)
                            {
                                _productWorkshopAttrDesc = this.dataGridView1.Rows[i].Cells["productWorkshopAttrDesc"].Value.ToString();
                            }
                            if (UserInfo.CustomerCode != "7001" && UserInfo.CustomerCode != "7002")
                            {

                                if (UserInfo.CustomerCode == "15001" || UserInfo.CustomerCode == "11001")
                                {
                                    #region 青柠泰和青年餐厅

                                    PreprocessInfoRequest request = new PreprocessInfoRequest();
                                    request.wareHouseId = UserInfo.WareHouseCode;
                                    request.warehouseCode = UserInfo.WareHouseCode;
                                    request.warehouseName = UserInfo.WareHouseName;
                                    request.customerCode = UserInfo.CustomerCode;
                                    request.customerName = UserInfo.CustomerName;
                                    request.request = list;
                                    PreprocessInfoAddResponse response = client.Execute(request);
                                    if (!response.IsError)
                                    {
                                        if (response.result != null)
                                        {
                                            preprocessInfoList = response.result;
                                            foreach (PreprocessInfo item in preprocessInfoList)
                                            {
                                                #region 打印
                                                curPreprocessInfo = item;
                                                PrintDocument document = new PrintDocument();
                                                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                                                document.OriginAtMargins = true;
                                                document.DefaultPageSettings.Landscape = false;
                                                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
                                dialog.Document = document;
#else
                                                PrintPreviewDialog dialog = new PrintPreviewDialog();
                                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintQingPage);
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
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {


                                    //称重商品
                                    PreprocessInfoRequest request = new PreprocessInfoRequest();
                                    request.wareHouseId = UserInfo.WareHouseCode;
                                    request.warehouseCode = UserInfo.WareHouseCode;
                                    request.warehouseName = UserInfo.WareHouseName;
                                    request.customerCode = UserInfo.CustomerCode;
                                    request.customerName = UserInfo.CustomerName;
                                    request.request = list;
                                    PreprocessInfoAddResponse response = client.Execute(request);
                                    if (!response.IsError)
                                    {
                                        if (response.result != null)
                                        {
                                            preprocessInfoList = response.result;
                                            foreach (PreprocessInfo item in preprocessInfoList)
                                            {
                                                #region 打印
                                                curPreprocessInfo = item;
                                                PrintDocument document = new PrintDocument();
                                                document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                                                document.OriginAtMargins = true;
                                                document.DefaultPageSettings.Landscape = false;
                                                document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
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
                                                #endregion
                                            }
                                        }
                                    }

                                }

                            }
                            else
                            {

                                #region 西贝客户
                                PreprocessXiBeiInfoRequest request = new PreprocessXiBeiInfoRequest();
                                request.wareHouseId = UserInfo.WareHouseCode;
                                request.warehouseCode = UserInfo.WareHouseCode;
                                request.warehouseName = UserInfo.WareHouseName;
                                request.customerCode = UserInfo.CustomerCode;
                                request.customerName = UserInfo.CustomerName;
                                request.request = list;
                                PreprocessInfoAddResponse response = client.Execute(request);
                                if (!response.IsError)
                                {
                                    if (response.result != null)
                                    {
                                        preprocessInfoList = response.result;
                                        foreach (PreprocessInfo item in preprocessInfoList)
                                        {
                                            item.goodsModel = goodsModel;
                                            item.productWorkshopAttrDesc = _productWorkshopAttrDesc;

                                            #region 打印
                                            curPreprocessInfo = item;
                                            PrintDocument document = new PrintDocument();
                                            document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                                            document.OriginAtMargins = true;
                                            document.DefaultPageSettings.Landscape = false;
                                            document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
                                dialog.Document = document;
#else
                                            PrintPreviewDialog dialog = new PrintPreviewDialog();
                                            document.PrintPage += new PrintPageEventHandler(this.pd_XiBeiPrintPage);
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
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("出现错误:" + goodsName + response.Message);
                                }
                                #endregion

                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:" + ex.Message);
            }
        }


    }
}

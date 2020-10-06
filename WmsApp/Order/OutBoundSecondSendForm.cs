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

namespace WmsApp.Order
{
    public partial class OutBoundSecondSendForm : TabWindow
    {

        private IWMSClient client = null;

        int totalPage;

        private PaginatorDTO paginator;

        private SortableBindingList<ShipmentModel> sortList = null;

        public OutBoundSecondSendForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
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
                    //list.Insert(0, new WaveCustomerStoreModel() { waveCode = "", waveName = "全部" });

                    //this.cbWave.DataSource = list;
                    //this.cbWave.ValueMember = "waveCode";
                    //this.cbWave.DisplayMember = "waveName";
                    //cbWave.SelectedIndex = 0;
                    this.ccbWave.DataSource = list;
                    this.ccbWave.ValueMember = "waveCode";
                    this.ccbWave.DisplayMember = "waveName";
                }
            }
        }

        private void bindStore()
        {
            StoreInfoAllRequest request = new StoreInfoAllRequest();
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            StoreInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    List<StoreInfo> storeList = new List<StoreInfo>();
                    storeList = response.result;
                    storeList.Insert(0, new StoreInfo { storedCode = "0", storedName = "全部" });
                    this.cbStore.DataSource = storeList;
                    this.cbStore.DisplayMember = "storedName";
                    this.cbStore.ValueMember = "storedCode";
                    cbStore.SelectedIndex = 0;
                }
            }
        }


        private void bindStatus()
        {
            //DictRequest request = new DictRequest();
            //request.type = "outboundStatus";

            //DictResponse response = client.Execute(request);
            //if (!response.IsError)
            //{
            //    if (response.result != null)
            //    {

            //        List<Dict> list = new List<Dict>();
            //        list = response.result;
            //        list.Insert(0, new Dict() { code = "-1", name = "全部" });

            //        this.cbStatus.DataSource = list;
            //        this.cbStatus.ValueMember = "code";
            //        this.cbStatus.DisplayMember = "name";
            //        cbStatus.SelectedIndex = 0;
            //    }
            //}
        }

        private void bindFreshAttr()
        {
            DictRequest request = new DictRequest();
            request.type = "freshAreaAttr";

            DictResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {
                    
                    List<Dict> list = new List<Dict>();
                    list = response.result;
                    list.Insert(0, new Dict() { code = "-1", name = "全部" });

                    this.cbFreshAttr.DataSource = list;
                    this.cbFreshAttr.ValueMember = "code";
                    this.cbFreshAttr.DisplayMember = "name";
                    cbFreshAttr.SelectedIndex = 0;
                }
            }
        }


        private void BindDgv()
        {

            string storeCode = cbStore.SelectedValue.ToString();
      //      int? status = int.Parse(cbStatus.SelectedValue.ToString());
            OutBoundSeondQueryRequest request = new OutBoundSeondQueryRequest();
            request.warehouseCode = UserInfo.WareHouseCode;
            request.customerCode = UserInfo.CustomerCode;
            if (cbStore.SelectedIndex != 0)
            {
                request.storedCode = cbStore.SelectedValue.ToString();
            }
            request.storedName = tbStoreName.Text.Trim();
            request.startTime = dtBegin.Value;
            request.endTime = Convert.ToDateTime(dtEnd.Value.ToString("yyyy-MM-dd") + " 23:59:59");

            //if (status == -1)
            //{
            //    status = null;
            //}
            //else
            //{
            //    request.status = status;
            //}
            request.status = null;
            request.isPrint = null;

            int? freshAttr = int.Parse(cbFreshAttr.SelectedValue.ToString());
            if (freshAttr == -1)
            {
                request.freshAttr = null;
            }
            else
            {
                request.freshAttr = freshAttr;
            }

            //if (cbWave.SelectedValue.ToString() != "")
            //{
            //    request.waveCode = cbWave.SelectedValue.ToString();
            //}

            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    waveCodeList.Add(wcsModel.waveCode);
                }
                request.waveCodeList = waveCodeList;
            }

            request.page = paginator.PageNo;

            OutBoundPageResponse response = client.Execute(request);

            if (!response.IsError)
            {

                if (response.result == null)
                {
                    this.dgv.DataSource = null;
                    MessageBox.Show("没有数据!");
                    return;
                }

                foreach (var item in response.result)
                {


                    if (sortingNumCount != null)
                    {
                        item.sortingNumCount = Convert.ToDecimal(item.sortingNumCount.ToString("f3"));
                    }
                    if (item.actualNumCount != null)
                    {
                        item.actualNumCount = Convert.ToDecimal(item.actualNumCount.ToString("f3"));
                    }

                    if (item.status == 0)
                    {
                        item.StatusDes = "新建";
                    }
                    if (item.status == 10)
                    {
                        item.StatusDes = "已包装";

                    }
                    if (item.status == 20)
                    {
                        item.StatusDes = "已分拣";
                    }
                    if (item.status == 30)
                    {
                        item.StatusDes = "已发运";
                    }
                    if (item.status == 40)
                    {
                        item.StatusDes = "关闭";
                    }

                    if (item.isPrint == 0)
                    {
                        item.IsPrintDes = "否";
                    }
                    if (item.isPrint == 1)
                    {
                        item.IsPrintDes = "是";
                    }

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

                IPagedList<ShipmentModel> pageList = new PagedList<ShipmentModel>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<ShipmentModel>(pageList.ContentList);
                this.dgv.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();

           
                for (int i = 0; i < this.dgv.Rows.Count; i++)
                {
                    if (this.dgv.Rows[i].Cells["StatusDes"].Value.ToString()!= "关闭")
                    {
                        this.dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        this.dgv.Rows[i].Cells["chk"].ReadOnly = true;
            
                    }
                }
            }

        }


        private void OutBoundSendForm_Load(object sender, EventArgs e)
        {
            dtBegin.Value = DateTime.Today.AddDays(0);
            dtEnd.Value = DateTime.Today.AddDays(0);
            this.dgv.AutoGenerateColumns = false;
            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();

            cbHeader.Value = string.Empty;

            bindWave();
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 50 };
            dgv.Columns[0].HeaderCell = cbHeader;
            bindStore();
            bindStatus();
            bindFreshAttr();
           // BindDgv();
            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dtBegin.Focus();
        }

        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            //这一句很重要结束编辑状态
            dgv.EndEdit();
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (this.dgv.Rows[i].Cells["StatusDes"].Value.ToString() != "关闭")
                    {
                        this.dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        this.dgv.Rows[i].Cells["chk"].ReadOnly = true;
                    }
                    else
                    {
                        dgv.Rows[i].Cells[0].Value = state;
                    }

                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Text = "正在查询";
                btnSearch.Enabled = false;
                paginator.PageNo = 1;
                BindDgv();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "查询";
            }
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            try
            {


                int m = 0;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (this.dgv.Rows[i].Cells["status"].Value.ToString()!= "40")
                    {
                        continue;
                    }
                    if ((bool)dgv.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        m++;
                    }
                }
                if (m == 0)
                {
                    MessageBox.Show("请选择要发运的单据");
                    return;
                }

                btnSend.Enabled = false;

                List<string> list = new List<string>();

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (this.dgv.Rows[i].Cells["status"].Value.ToString()!= "40")
                    {
                        continue;
                    }

                    if ((bool)dgv.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        String taskCode = this.dgv.Rows[i].Cells["outboundTaskCode"].Value == null ? "" : this.dgv.Rows[i].Cells["outboundTaskCode"].Value.ToString();
                        list.Add(taskCode);
                    }
                }



                OutBoundCheckRequest request = new OutBoundCheckRequest();
                request.outboundTaskCodes = list;

                OutBoundCheckResponse response = client.Execute(request);
                if (!response.IsError)
                {

                    if (!string.IsNullOrWhiteSpace(response.Message))
                    {
                        if (MessageBox.Show(response.Message, "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                    }


                        LoadingHelper.ShowLoadingScreen();
                        int sucCount = 0;
                        int errCount = 0;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < list.Count; i++)
                        {

                            List<string> curList = new List<string>();
                            curList.Clear();
                            curList.Add(list[i]);

                            OutBoundSendRequest sendRequest = new OutBoundSendRequest();
                            sendRequest.outboundTaskCodes = curList;
                            sendRequest.createUser = UserInfo.UserName;
                            sendRequest.warehouseCode = UserInfo.WareHouseCode;
                            sendRequest.customerCode = UserInfo.CustomerCode;
                            sendRequest.customerName = UserInfo.CustomerName;
                            OutBoundSendResponse sendResponse = client.Execute(sendRequest);
                            if (!sendResponse.IsError)
                            {
                                sucCount += 1;
                            }
                            else
                            {
                                errCount += 1;
                                sb.Append(sendResponse.Message).Append(","); ;
                            }
                        }
                        LoadingHelper.CloseForm();

                        MessageBox.Show("本次共发运" + list.Count + "条单据,成功:" + sucCount + "条,失败:" + errCount + "条\r\n失败单据:"+sb.ToString());

                }
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {

                MessageBox.Show("出现异常" + ex.Message);
            }
            finally
            {
                btnSend.Enabled = true;
            }

        }

        private void ccbWave_ItemClick(object sender, ItemCheckEventArgs e)
        {
            List<string> selectList = new List<string>();

            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    //selectText += wcsModel.waveName + ",";
                    selectList.Add(wcsModel.waveName);
                }
            }
          
                if (e.NewValue == CheckState.Checked)
                {
                  //  selectText+=
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)this.ccbWave.Items[e.Index];
                   // selectText += wcsModel.waveName;
                    selectList.Add(wcsModel.waveName);
                }

                if (e.NewValue != CheckState.Checked)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)this.ccbWave.Items[e.Index];
                    selectList.Remove(wcsModel.waveName);
                }

                string selectText = "";
                foreach (var item in selectList)
                {
                    selectText += item + ",";
                }
                selectText = selectText.Trim(',');
            this.ccbWave.SetTitleText(selectText);
        }

        private void ccbWave_AllClick(object sender, EventArgs e)
        {
            List<string> selectList = new List<string>();

            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    //selectText += wcsModel.waveName + ",";
                    selectList.Add(wcsModel.waveName);
                }
            }

            string selectText = "";
            foreach (var item in selectList)
            {
                selectText += item + ",";
            }
            selectText = selectText.Trim(',');
            this.ccbWave.SetTitleText(selectText);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}

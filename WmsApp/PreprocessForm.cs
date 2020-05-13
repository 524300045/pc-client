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
    public partial class PreprocessForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<PreprocessInfo> sortList = null;

        private PackageDetailQuery curPackageDetailQuery;

        private List<PreprocessInfo> preprocessInfoList;

        private IWMSClient client = null;

        public PreprocessForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            cbStatus.SelectedIndex = 0;
            this.dataGridView1.AutoGenerateColumns = false;
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            bindWave();
            BindDgv();
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
                paginator.PageNo = 1;
                BindDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindDgv()
        {
            PreprocessInfoQueryRequest request = new PreprocessInfoQueryRequest();
            if (!string.IsNullOrWhiteSpace(tbPackageCode.Text.Trim()))
            {
                request.preprocessCode = tbPackageCode.Text.Trim();
            }
            if (!string.IsNullOrWhiteSpace(tbName.Text.Trim()))
            {
                request.goodsName = "%" + tbName.Text.Trim() + "%";
            }
            if (cbStatus.SelectedIndex==1)
            {
                request.status = 0;
            }

            if (cbStatus.SelectedIndex == 2)
            {
                request.status = 1;
            }
            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;
            request.partnerCode = UserInfo.PartnerCode;
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;

            //if (cbWave.SelectedValue.ToString()!="")
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

            PreprocessInfoResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
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
                preprocessInfoList = response.result;

                IPagedList<PreprocessInfo> pageList = new PagedList<PreprocessInfo>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<PreprocessInfo>(pageList.ContentList);
                this.dataGridView1.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
            }
            else
            {
  
                this.dataGridView1.DataSource = null;
            }

        }

        private void tbPackageCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnQuery_Click(null,null);
            }
        }

        private void pageSplit1_PageChanged(object sender, EventArgs e)
        {
            paginator.PageNo = pageSplit1.PageNo;
            BindDgv();
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

        private void ccbWave_ItemClick(object sender, ItemCheckEventArgs e)
        {
            List<string> selectList = new List<string>();
            if (this.ccbWave.CheckedItems.Count > 0)
            {
                List<String> waveCodeList = new List<string>();
                foreach (var item in this.ccbWave.CheckedItems)
                {
                    WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)item;
                    selectList.Add(wcsModel.waveName);
                }
            }
            if (e.NewValue == CheckState.Checked)
            {
                WaveCustomerStoreModel wcsModel = (WaveCustomerStoreModel)this.ccbWave.Items[e.Index];
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

        
    }
}

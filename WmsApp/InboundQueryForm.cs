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
    public partial class InboundQueryForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<InBoundDetailModel> sortList = null;

        private IWMSClient client = null;



        int totalPage;

        List<String> colsHeaderText_H = new List<String>();

        public InboundQueryForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
            

        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            BindDgv();
        }

      

        private void BindDgv()
        {
            InBoundDetailRequest request = new InBoundDetailRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
          
            request.orderNo = tbOrderNo.Text.Trim();
         
            request.goodsName = tbName.Text.Trim();

            request.customerCode = UserInfo.CustomerCode;
            request.warehouseCode = UserInfo.WareHouseCode;

            InBoundDetailResponse response = client.Execute(request);
            if (!response.IsError)
            {

                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
                    return;
                }
                for (int i = 0; i < response.result.Count; i++)
                {
                    if (response.result[i].modelNum==null||response.result[i].modelNum==0)
                    {
                        response.result[i].modelNum = 1;
                    }
                    response.result[i].boxNum = response.result[i].realityNum / response.result[i].modelNum;
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
                IPagedList<InBoundDetailModel> pageList = new PagedList<InBoundDetailModel>(response.result, recordCount, totalPage);
                sortList = new SortableBindingList<InBoundDetailModel>(pageList.ContentList);


                this.dataGridView1.DataSource = sortList;
                pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                pageSplit1.PageCount = pageList.PageCount;
                pageSplit1.PageNo = paginator.PageNo;
                pageSplit1.DataBind();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    
                    string skucode = this.dataGridView1.CurrentRow.Cells["skuCode"].Value.ToString();
                    string goodsName = this.dataGridView1.CurrentRow.Cells["goodsName"].Value.ToString();
                    decimal modelNum = decimal.Parse(this.dataGridView1.CurrentRow.Cells["modelNum"].Value.ToString());
                    string productionDate = this.dataGridView1.CurrentRow.Cells["productionDate"].Value.ToString();
                    long detailId = long.Parse(this.dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    InboundPrintForm printForm = new InboundPrintForm(detailId, skucode,goodsName, productionDate, modelNum);
                    printForm.ShowDialog();
                }
            }
        }




     

    }
}

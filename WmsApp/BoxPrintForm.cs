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
    public partial class BoxPrintForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<BoxInfo> sortList = null;

        private IWMSClient client = null;
        public BoxPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            bindStore();
        }

        private void bindStore()
        {
            StoreInfoRequest request = new StoreInfoRequest();
            request.partnerCode = UserInfo.PartnerCode;

           StoreInfoResponse response=client.Execute(request);
           if (!response.IsError)
           {
               if (response.result!=null)
               {
                   List<StoreInfo> storeList=new List<StoreInfo>();
                   foreach (StoreInfo item in  response.result)
                   {
                       storeList.Add(item);
                   }

                   storeList.Insert(0, new StoreInfo() { storedCode = "", storedName = "请选择" });
               
                   this.cbStore.DataSource = storeList;
                   this.cbStore.ValueMember = "storedCode";
                   this.cbStore.DisplayMember = "storedName";
                   
                
               }
           }
        }

        private void BindDgv()
        {
            BoxInfoRequest request = new BoxInfoRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.partnerCode = UserInfo.PartnerCode;
            if (cbStore.SelectedIndex>0)
            {
                request.storedCode = cbStore.SelectedValue.ToString();
            }
         
            BoxInfoResponse response = client.Execute(request);
           if (!response.IsError)
           {
               if (response.result==null)
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
               IPagedList<BoxInfo> pageList = new PagedList<BoxInfo>(response.result, recordCount, totalPage);
               sortList = new SortableBindingList<BoxInfo>(pageList.ContentList);
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

        private void btnBox_Click(object sender, EventArgs e)
        {
            //PrintBoxForm form = new PrintBoxForm();
            PrintBoxStoreForm form = new PrintBoxStoreForm();
            form.ShowDialog();

            BindDgv();
        }


    }
}

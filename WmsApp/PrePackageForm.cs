using Common;
using Sdbs.Wms.Controls.Pager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wms.Controls.Pager;
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;

namespace WmsApp
{
    public partial class PrePackageForm : TabWindow
    {

        private PaginatorDTO paginator;

        private SortableBindingList<Goods> sortList = null;

        List<Goods> goodsList;
        private IWMSClient client = null;
        public PrePackageForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }

        private void PackageTaskForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            dtBegin.Value = DateTime.Today.AddDays(2).AddDays(-1);
            paginator = new PaginatorDTO { PageNo = 1, PageSize = 100 };
            //BindDgv();a
            Task.Factory.StartNew(() => {
                btnQuery_Click(null,null);
            });
        }

        private void BindDgv()
        {
            GoodsRequest request = new GoodsRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            //request.skuCode= "%"+tbName.Text.Trim()+"%";
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

                   goodsList = response.result;

                   if (goodsList!=null)
                   {
                       foreach (Goods item in goodsList)
                       {
                           if (item.orderNum ==0)
                           {
                               item.diffNum = 0;
                           }
                           else
                           {
                               item.diffNum = item.orderNum.Value- (int)item.packageNum;
                           }
                       }
                   }
                   IPagedList<Goods> pageList = new PagedList<Goods>(goodsList.OrderByDescending(p => p.orderNum), recordCount, totalPage);
                   sortList = new SortableBindingList<Goods>(pageList.ContentList);
                   
                 
                   this.Invoke(new MethodInvoker(delegate() {
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
                    
                    string skucode = this.dataGridView1.CurrentRow.Cells["skuCode"].Value.ToString();
                    string goodsName = this.dataGridView1.CurrentRow.Cells["goodsName"].Value.ToString();

                    string orderNum = "";
                    if (this.dataGridView1.CurrentRow.Cells["orderNum"].Value!=null)
                    {
                         orderNum = this.dataGridView1.CurrentRow.Cells["orderNum"].Value.ToString();
                    }
               
                   // int weighted =int.Parse(this.dataGridView1.CurrentRow.Cells["weighed"].Value.ToString());
                    //这里可以编写你需要的任意关于按钮事件的操作~
                    Goods goods = goodsList.Where(p => p.skuCode==skucode).FirstOrDefault();
                    PreWeightForm weightForm = new PreWeightForm(goods,dtBegin.Value);
                    weightForm.orderNum = orderNum;

                    if (weightForm.ShowDialog()==DialogResult.OK)
                    {
                        
                    }

                    Task.Factory.StartNew(() =>
                    {
                        btnQuery_Click(null, null);
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
                    BindDgv();
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
            BindDgv();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnQuery_Click(null,null);
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



                    GoodsRequest request = new GoodsRequest();
                    request.PageIndex = 1;
                    request.PageSize =500;
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
                       if (response.result!= null)
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


    }
}

using Sdbs.Wms.Controls.Pager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
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
    public partial class FrozenSuitForm : TabWindow
    {

        private IWMSClient client = null;

        public List<Goods> goodsList;

        public FrozenSuitForm()
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

        
        private void bindAttr()
        {
            DictRequest request = new DictRequest();
            request.type = "goodstype";

            DictResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result != null)
                {

                    List<Dict> list = new List<Dict>();
                    list = response.result;
                    list.Insert(0, new Dict() { code = "-1", name = "全部" });


                    this.cbType.DataSource = list;
                    this.cbType.ValueMember = "code";
                    this.cbType.DisplayMember = "name";
                    cbType.SelectedIndex = 0;
                }
            }
        }
     

        private void BindDgv()
        {
            goodsList=new List<Goods>();

            GoodsFrozenRequest request = new GoodsFrozenRequest();
            request.customerCode = UserInfo.CustomerCode;
            request.goodsName = tbName.Text.ToString().Trim();
            if (cbType.SelectedIndex!=0)
            {
                request.goodsType = int.Parse(cbType.SelectedValue.ToString());
            }
            request.goodsType = 4;
            GoodsFrozenResponse response = client.Execute(request);
            if (!response.IsError)
            {
         
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = null;
                    MessageBox.Show("没有获取到数据!");
                    return;
                }
                goodsList = response.result;
                for (int i = 0; i < response.result.Count; i++)
                {
                    response.result[i].weighedDes = "否";
                    if (response.result[i].weighed!=null&&response.result[i].weighed==1)
                    {
                        response.result[i].weighedDes = "是";
                    }
                    if (response.result[i].goodsType != null)
                    {
                        if (response.result[i].goodsType==0)
                        {
                            response.result[i].GoodsTypeDes = "标品整箱";
                        }
                        if (response.result[i].goodsType == 1)
                        {
                            response.result[i].GoodsTypeDes = "标品拆零";
                        }
                        if (response.result[i].goodsType == 2)
                        {
                            response.result[i].GoodsTypeDes = "抄码整箱";
                        }
                        if (response.result[i].goodsType == 3)
                        {
                            response.result[i].GoodsTypeDes = "抄码拆零";
                        }
                        if (response.result[i].goodsType == 4)
                        {
                            response.result[i].GoodsTypeDes = "套装";
                        }
                    }

                }
                this.dataGridView1.DataSource = response.result;
                
                gb.Text = "共查询到"+response.result.Count.ToString()+"条";
            }
        }

        private void GoodsBarCodePrintForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;
            
                bindAttr();
                BindDgv();

            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    string skucode = this.dataGridView1.CurrentRow.Cells["skuCode"].Value.ToString();
                    Goods goods = goodsList.Where(p => p.skuCode == skucode).FirstOrDefault();
                    if (goods.goodsType ==4)
                    {
                        FengBoxForm fengBoxForm = new FengBoxForm(goods);
                        fengBoxForm.ShowDialog();
                    }
                }
            }
        }

    }
}

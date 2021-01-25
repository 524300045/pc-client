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
using WmsSDK;
using WmsSDK.Model;
using WmsSDK.Request;
using WmsSDK.Response;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace WmsApp
{
    public partial class FengBoxForm : Form
    {

        BindingList<WmsFrozenBoxCode> wmsFrozenBoxCodes = null;

        Goods goods;
        GoodsSuitBox goodsSuitBox = null;

        private IWMSClient client = null;
        public FengBoxForm()
        {
            InitializeComponent();
        }

        public FengBoxForm(Goods _goods)
        {
            InitializeComponent();
            this.goods = _goods;
            client = new DefalutWMSClient();
            this.dataGridView1.AutoGenerateColumns = false;
            wmsFrozenBoxCodes = new BindingList<WmsFrozenBoxCode>();
        }

      

        private void FengBoxForm_Load(object sender, EventArgs e)
        {
            tbBarCode.Focus();
            lbName.Text = this.goods.goodsName;
        }

        private void tbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(tbBarCode.Text.Trim()))
                    {
                        MessageBox.Show("请扫描条码!");
                        tbBarCode.Focus();
                        return;
                    }

                    String boxCode = tbBarCode.Text.Trim();
                    tbBarCode.Enabled = false;
                    lbInfo.Text = "";
                    WmsFrozenBoxCodeSuitScanRequest request = new WmsFrozenBoxCodeSuitScanRequest();
                    request.customerCode = UserInfo.CustomerCode;
                    request.warehouseCode = UserInfo.WareHouseCode;
                    request.boxCode = boxCode;
                    request.skuCode = this.goods.skuCode;
                    WmsFrozenBoxCodeSingleResponse response = client.Execute(request);
                    if (!response.IsError)
                    {
                        if (response.result == null)
                        {
                            MessageBox.Show("操作失败!");
                            return;
                        }

                        WmsFrozenBoxCode curWmsFrozenBox = response.result;

                        var result = wmsFrozenBoxCodes.Where(p => p.boxCode == curWmsFrozenBox.boxCode).FirstOrDefault();
                        if (result!=null)
                        {
                            MessageBox.Show("当前条码已扫描，请核查!");
                            return;
                        }

                        result = wmsFrozenBoxCodes.Where(p => p.skuCode == curWmsFrozenBox.skuCode).FirstOrDefault();
                        if (result != null)
                        {
                            MessageBox.Show("一个箱中同一个品只能装箱一个!");
                            return;
                        }

                        wmsFrozenBoxCodes.Add(curWmsFrozenBox);
                        lbInfo.Text = curWmsFrozenBox.goodsName;
                       
                        this.dataGridView1.DataSource = wmsFrozenBoxCodes;
                      

                    }
                    else
                    {
                        MessageBox.Show(response.Message);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tbBarCode.Enabled = true;
                    tbBarCode.Text = "";
                    tbBarCode.Focus();
                }
            }
        }

        private void btnSumit_Click(object sender, EventArgs e)
        {
            try
            {

                if (wmsFrozenBoxCodes.Count==0)
                {
                    MessageBox.Show("还未扫描商品");
                    return;
                }
                if (wmsFrozenBoxCodes.Count<8)
                {
                   DialogResult dialogResult=MessageBox.Show("商品没有扫描8件，确定提交?","确认",MessageBoxButtons.OKCancel);
                    if (dialogResult!=DialogResult.OK)
                    {
                        return;
                    }
                }
                btnSumit.Enabled = false;
                List<GoodsSuitBoxDetailResponse> goodsSuitBoxes = new List<GoodsSuitBoxDetailResponse>();
                foreach (WmsFrozenBoxCode item in wmsFrozenBoxCodes)
                {
                    GoodsSuitBoxDetailResponse goodsSuitBox = new GoodsSuitBoxDetailResponse();
                    goodsSuitBox.skuCode = goods.skuCode;
                    goodsSuitBox.childBarCode = item.boxCode;
                    goodsSuitBox.createUser = UserInfo.RealName;
                    goodsSuitBox.productionDate = dt.Value.ToString("yyyy-MM-dd");
                    goodsSuitBoxes.Add(goodsSuitBox);
                }

                GoodsSuitBoxTransferDetailRequest goodsSuitBoxTransferDetailRequest = new GoodsSuitBoxTransferDetailRequest();
                goodsSuitBoxTransferDetailRequest.goodsSuitBoxList = goodsSuitBoxes;
                GoodsSuitBoxTransferDetailResponse response = client.Execute(goodsSuitBoxTransferDetailRequest);
                if (!response.IsError)
                {
                    if (response.result == null)
                    {
                        MessageBox.Show("操作失败!");
                        return;
                    }

                    goodsSuitBox = response.result;
                    this.dataGridView1.Rows.Clear();
                    wmsFrozenBoxCodes.Clear();


                    #region 打印

                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 220);
                    document.OriginAtMargins = true;
                    document.DefaultPageSettings.Landscape = false;
                    document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if (!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_Print);
                                dialog.Document = document;
#else
                    PrintPreviewDialog dialog = new PrintPreviewDialog();
                    document.PrintPage += new PrintPageEventHandler(this.pd_Print);
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
                else
                {
                    MessageBox.Show(response.Message);
                    return;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                tbBarCode.Focus();
                btnSumit.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                { 
                   string boxCode= this.dataGridView1.CurrentRow.Cells["boxCode"].Value.ToString();
                    int pos = this.dataGridView1.CurrentRow.Index; //记位置
                    this.wmsFrozenBoxCodes.RemoveAt(pos);
                    Console.WriteLine(wmsFrozenBoxCodes.Count);
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


        private void pd_Print(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateCode(goodsSuitBox.boxCode);
            GetPicture(bt, e);
        }

        public static Bitmap CreateCode(string asset)
        {
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 100,
                Height = 100
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            return writer.Write(asset);
        }


        public void GetPicture(Bitmap image, PrintPageEventArgs g)
        {

            Font fontCu11 = new Font("宋体", 10f, FontStyle.Bold);
            Font fontCu = new Font("宋体", 10f, FontStyle.Bold);
            int height = -10;

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = -15;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(160, height+100, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);
            //layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            //g.Graphics.DrawString(goodsSuitBox.skuCode, fontCu11, brush, layoutRectangle);

            height += 15;
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
            g.Graphics.DrawString(this.goods.goodsName+"("+goodsSuitBox.skuCode+")", fontCu11, brush, layoutRectangle);


            //生产日期
            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("生产日期:" + goodsSuitBox.productionDate+"", new Font("宋体", 10f,FontStyle.Bold), brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("总量量:" + goodsSuitBox.weight*2 + "斤", new Font("宋体", 10f, FontStyle.Bold), brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("贮存条件:<= -18°保质期:7个月", new Font("宋体", 10f,FontStyle.Bold), brush, layoutRectangleRight);

        

            foreach (GoodsSuitBoxDetailResponse item in goodsSuitBox.detailList)
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(item.goodsName+" "+item.childWeight*2+"斤", new Font("宋体", 10f, FontStyle.Bold), brush, layoutRectangleRight);
            }

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString(goodsSuitBox.boxCode, new Font("宋体", 10f, FontStyle.Bold), brush, layoutRectangleRight);

            height += 15;
            layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
            g.Graphics.DrawString("易破损  轻拿轻放", new Font("宋体",10f), brush, layoutRectangleRight);

        }
    }
}

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
    public partial class SuitPrintForm : TabWindow
    {


        public DateTime lastDateTime;

        private IWMSClient client = null;
        public SuitPrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
        }


        private GoodsSuitBox goodsSuitBox;



        private void BindDgv()
        {
            GoodsSuitRequest request = new GoodsSuitRequest();

            request.skuCode = "114625";


            GoodsSuitResponse response = client.Execute(request);
            if (!response.IsError)
            {
                if (response.result == null)
                {
                    this.dataGridView1.DataSource = response.result;
                    return;
                }

                foreach (GoodsSuitModel detail in response.result)
                {
                    if (detail.childSkuCode == "113660")
                    {
                        detail.seq = 1;

                    }
                    if (detail.childSkuCode == "113657")
                    {
                        detail.seq = 2;
                    }
                    if (detail.childSkuCode == "113659")
                    {
                        detail.seq = 3;
                    }
                    if (detail.childSkuCode == "113654")
                    {

                        detail.seq = 4;
                    }
                    if (detail.childSkuCode == "113656")
                    {

                        detail.seq = 5;
                       // detail.childGoodsName = "带肉牛排生制品";
                    }
                    if (detail.childSkuCode == "113661")
                    {

                        detail.seq = 6;
                       // detail.childGoodsName = "牛力骨生制品";
                    }
                    if (detail.childSkuCode == "113655")
                    {
                        detail.seq = 7;
                       // detail.childGoodsName = "大力骨生制品";
                    }
                }

                response.result = response.result.OrderBy(p => p.seq).ToList();
                this.dataGridView1.DataSource = response.result;
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

        //private void btnQuery_Click(object sender, EventArgs e)
        //{

        //    BindDgv();
        //}


        private void btnBox_Click(object sender, EventArgs e)
        {

            try
            {
                List<GoodsSuitBoxTransferDetailModel> detailList = new List<GoodsSuitBoxTransferDetailModel>();

                String msg = "";
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    String childSkuCode = this.dataGridView1.Rows[i].Cells["childSkuCode"].Value.ToString();
                    String name = this.dataGridView1.Rows[i].Cells["childGoodsName"].Value.ToString();
                    String weightStr = this.dataGridView1.Rows[i].Cells["num"].EditedFormattedValue.ToString();
                    if (String.IsNullOrEmpty(weightStr))
                    {
                        msg = name + "重量不能炜空";
                        break;
                    }
                    GoodsSuitBoxTransferDetailModel detailModel = new GoodsSuitBoxTransferDetailModel();
                    detailModel.childSkuCode = childSkuCode;
                    detailModel.childWeight = decimal.Parse(weightStr);
                    detailModel.warehouseCode = UserInfo.WareHouseCode;
                    detailModel.warehouseName = UserInfo.WareHouseName;
                    detailModel.customerCode = UserInfo.CustomerCode;
                    detailModel.customerName = UserInfo.CustomerName;
                    detailModel.skuCode = "114625";
                    detailList.Add(detailModel);
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg);
                    return;
                }



                SuitDateForm suitForm = new SuitDateForm(lastDateTime);
                if (suitForm.ShowDialog() == DialogResult.OK)
                {
                    lastDateTime = suitForm.lastDateTime;

                    GoodsSuitBoxRequest request = new GoodsSuitBoxRequest();
                    request.warehouseCode = UserInfo.WareHouseCode;
                    request.warehouseName = UserInfo.WareHouseName;
                    request.customerCode = UserInfo.CustomerCode;
                    request.customerName = UserInfo.CustomerName;
                    request.skuCode = "114625";
                    request.productionDate = suitForm.productionDate;
                    request.weight = detailList.Sum(p => p.childWeight);
                    request.source = 0;
                    request.status = 0;
                    request.createUser = UserInfo.UserName;

                    foreach (GoodsSuitBoxTransferDetailModel item in detailList)
                    {
                        item.weight = request.weight;
                    }
                    request.detail = detailList;
                    GoodsSuitBoxResponse response = client.Execute(request);
                    if (!response.IsError)
                    {
                        request.boxCode = response.result.boxCode;

                        goodsSuitBox = response.result;
                        lbWeight.Text = "";
                        for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            this.dataGridView1.Rows[i].Cells["num"].Value = "";
                        }

                        #region 打印

                        PrintDocument document = new PrintDocument();
                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);
                        document.OriginAtMargins = true;
                        document.DefaultPageSettings.Landscape = false;
                        document.DefaultPageSettings.Margins = new Margins(SystemInfo.PrintMarginLeft, 1, 0, 1);
#if(!DEBUG)
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
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void SuitPrintForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            lastDateTime = DateTime.Now;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            BindDgv();
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
                Width = 140,
                Height = 140
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
            int height = 15;
            int heightRight = 15;

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            int pointX = -30;

            RectangleF layoutRectangleRight = new RectangleF(80f, 5, 130f, 85f);

            Rectangle destRect = new Rectangle(100, -10, image.Width, image.Height);
            g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            heightRight += 40;
            RectangleF layoutRectangle = new RectangleF(pointX, height, 120f, 30f);


            layoutRectangle = new RectangleF(pointX, 15, 165f, 30f);
            g.Graphics.DrawString(goodsSuitBox.boxCode, fontCu11, brush, layoutRectangle);

            height += 15;
            //重量
            layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            g.Graphics.DrawString(goodsSuitBox.skuCode + "  牛八件", font, brush, layoutRectangle);

            height += 15;
            //重量
            layoutRectangle = new RectangleF(pointX, height, 165f, 40f);
            g.Graphics.DrawString("生产日期:" + goodsSuitBox.productionDate.Replace("00:00:00", ""), font, brush, layoutRectangle);

            height += 15;
            //商品名称
            layoutRectangle = new RectangleF(pointX, height, 300f, 30f);
            g.Graphics.DrawString("总重量:" + goodsSuitBox.weight + " Kg", fontCu11, brush, layoutRectangle);




            for (int i = 0; i < goodsSuitBox.detailList.Count; i++)
            {
                height += 15;
                layoutRectangleRight = new RectangleF(pointX, height, 300f, 85f);
                g.Graphics.DrawString(goodsSuitBox.detailList[i].childSkuCode + " " + goodsSuitBox.detailList[i].goodsName + "  " + goodsSuitBox.detailList[i].childWeight + " kg", font, brush, layoutRectangleRight);
            }





        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewComboBoxColumn combo = dataGridView1.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (combo != null)  //如果该列是ComboBox列
                {
                    dataGridView1.BeginEdit(false); //结束该列的编辑状态
                    DataGridViewComboBoxEditingControl comboEdite = dataGridView1.EditingControl as DataGridViewComboBoxEditingControl;
                    if (comboEdite != null)
                    {
                        comboEdite.DroppedDown = true; //展现下拉列表
                    }
                }
                DataGridViewTextBoxColumn textbox = dataGridView1.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
                if (textbox != null) //如果该列是TextBox列
                {
                    dataGridView1.BeginEdit(true); //开始编辑状态
                
                }
            }
        }

        //private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
          
        //}

        //private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
          
        //}

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().Equals(typeof(DataGridViewTextBoxEditingControl)))//cell为类TextBox时
            {
                e.CellStyle.BackColor = Color.FromName("window");
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
            }

        }

        void editingControl_TextChanged(object sender, EventArgs e)
        {
            decimal totalWeight = 0;

            try
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        object weightStr = this.dataGridView1.Rows[i].Cells["num"].EditedFormattedValue;
                        if (weightStr != null && !String.IsNullOrEmpty(weightStr.ToString().Trim()))
                        {
                            totalWeight = totalWeight += decimal.Parse(weightStr.ToString().Trim());
                        }
                    }
                }

                lbWeight.Text = "总重量 " + totalWeight.ToString() + " KG";

            }
            catch (Exception ex)
            {

                lbWeight.Text = ex.Message;
            }
        }

    }
}

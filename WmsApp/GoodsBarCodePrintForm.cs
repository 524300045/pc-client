﻿using Sdbs.Wms.Controls.Pager;
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
    public partial class GoodsBarCodePrintForm : TabWindow
    {

        private IWMSClient client = null;

        private PaginatorDTO paginator;

        private SortableBindingList<GoodsBarCode> sortList = null;

        List<GoodsBarCode> goodsList;

        private string curGoodName = "";
        private string curSkuCode = "";
        private string curGoodsGrade = "";
        private string curGoodsModel = "";

        public GoodsBarCodePrintForm()
        {
            InitializeComponent();
            client = new DefalutWMSClient();
            cbBarCode.SelectedIndex = 0;
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

        private void btnPrint_Click(object sender, EventArgs e)
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
                MessageBox.Show("请选择要打印条码的商品");
                return;
            }

            int num = 0;
            BarCodePrintNumForm form = new BarCodePrintNumForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                num = form.number;
            }
            else
            {
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    String skucode = this.dataGridView1.Rows[i].Cells["skuCode"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["skuCode"].Value.ToString();
                    String name = this.dataGridView1.Rows[i].Cells["goodsName"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["goodsName"].Value.ToString();
                    String goodsModel = this.dataGridView1.Rows[i].Cells["goodsModel"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["goodsModel"].Value.ToString();
                    String goodsGrade = this.dataGridView1.Rows[i].Cells["goodsGrade"].Value == null ? "" : this.dataGridView1.Rows[i].Cells["goodsGrade"].Value.ToString();
                    curSkuCode = skucode;
                    curGoodName = name;
                    curGoodsGrade = goodsGrade;
                    curGoodsModel = goodsModel;
                    PrintDocument document = new PrintDocument();
                    document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

                    for (int n = 0; n < num; n++)
                    {
#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
                                dialog.Document = document;
#else
                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
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
                    }
                }
            }

        }

        private void BindDgv()
        {
            string codeOne = cbOne.SelectedValue == null ? "" : cbOne.SelectedValue.ToString();
            string codeTwo = cbTwo.SelectedValue == null ? "" : cbTwo.SelectedValue.ToString();
            string codeThree = cbThree.SelectedValue == null ? "" : cbThree.SelectedValue.ToString();
            string name = tbName.Text.Trim();

            GoodsQueryRequest request = new GoodsQueryRequest();
            request.PageIndex = paginator.PageNo;
            request.PageSize = paginator.PageSize;
            request.goodsName = name;
            request.categoryCode = codeOne;
            request.twoCategoryCode = codeTwo;
            request.threeCategoryCode = codeThree;
            request.isFresh = null;
            if (cbFresh.SelectedIndex == 1)
            {
                request.isFresh = 0;
            }
            if (cbFresh.SelectedIndex == 2)
            {
                request.isFresh = 1;
            }

            if (cbBarCode.SelectedIndex == 0)
            {
                request.isBarCode = null;
            }
            else
            {
                request.isBarCode = cbBarCode.SelectedIndex;
            }
         


            GoodsBarCodeResponse response = client.Execute(request);
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
                    IPagedList<GoodsBarCode> pageList = new PagedList<GoodsBarCode>(goodsList.OrderBy(p => p.goodsName), recordCount, totalPage);
                    sortList = new SortableBindingList<GoodsBarCode>(pageList.ContentList);
                    this.dataGridView1.DataSource = sortList;
                    pageSplit1.Description = "共查询到" + pageList.RecordCount + "条记录";
                    pageSplit1.PageCount = pageList.PageCount;
                    pageSplit1.PageNo = paginator.PageNo;
                    pageSplit1.DataBind();
                }

            }

        }

        private void GoodsBarCodePrintForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;
                DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
                //colCB.HeaderCell = cbHeader;

                //dataGridView1.Columns.Add(colCB);
                cbHeader.Value = string.Empty;

                cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
                dataGridView1.Columns[0].HeaderCell = cbHeader;

                paginator = new PaginatorDTO { PageNo = 1, PageSize = 30 };
                cbFresh.SelectedIndex = 1;
                cbOne.SelectedValueChanged -= cbOne_SelectedValueChanged;
                GoodsCategoryQueryRequest request = new GoodsCategoryQueryRequest();
                request.level = "1";
                GoodsCategoryResponse response = client.Execute(request);
                if (!response.IsError)
                {
                    List<GoodsCategory> categoryList = null;
                    if (response.result == null)
                    {
                        categoryList = new List<GoodsCategory>();
                    }
                    else
                    {
                        categoryList = response.result;
                    }
                    categoryList.Insert(0, new GoodsCategory() { categoryCode = "", categoryName = "全部" });
                    cbOne.DataSource = categoryList;
                    cbOne.DisplayMember = "categoryName";
                    cbOne.ValueMember = "categoryCode";
                }

                cbOne.SelectedValueChanged += cbOne_SelectedValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
            }
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


        private void cbOne_SelectedValueChanged(object sender, EventArgs e)
        {
            string oneCode = cbOne.SelectedValue == null ? "" : cbOne.SelectedValue.ToString();
            if (oneCode == "")
            {
                List<GoodsCategory> categoryList = new List<GoodsCategory>();
                categoryList.Insert(0, new GoodsCategory() { categoryCode = "", categoryName = "--请选择--" });
                cbTwo.DataSource = categoryList;
                cbTwo.DisplayMember = "categoryName";
                cbTwo.ValueMember = "categoryCode";

                //cbThree.DataSource = categoryList;
                //cbThree.DisplayMember = "categoryName";
                //cbThree.ValueMember = "categoryCode";
                return;
            }

            try
            {
                GoodsCategoryQueryRequest request = new GoodsCategoryQueryRequest();
                request.level = "2";
                request.parentCode = oneCode;
                GoodsCategoryResponse response = client.Execute(request);
                if (!response.IsError)
                {
                    List<GoodsCategory> categoryList = null;
                    if (response.result == null)
                    {
                        categoryList = new List<GoodsCategory>();
                    }
                    else
                    {
                        categoryList = response.result;
                    }
                    categoryList.Insert(0, new GoodsCategory() { categoryCode = "", categoryName = "全部" });
                    cbTwo.DataSource = categoryList;
                    cbTwo.DisplayMember = "categoryName";
                    cbTwo.ValueMember = "categoryCode";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
            }
        }



        private void cbTwo_SelectedValueChanged(object sender, EventArgs e)
        {
            string twoCode = cbTwo.SelectedValue == null ? "" : cbTwo.SelectedValue.ToString();
            if (twoCode == "")
            {
                List<GoodsCategory> categoryList = new List<GoodsCategory>();
                categoryList.Insert(0, new GoodsCategory() { categoryCode = "", categoryName = "--请选择--" });
                cbThree.DataSource = categoryList;
                cbThree.DisplayMember = "categoryName";
                cbThree.ValueMember = "categoryCode";
                return;
            }

            try
            {
                GoodsCategoryQueryRequest request = new GoodsCategoryQueryRequest();
                request.level = "3";
                request.parentCode = twoCode;
                GoodsCategoryResponse response = client.Execute(request);
                if (!response.IsError)
                {
                    List<GoodsCategory> categoryList = null;
                    if (response.result == null)
                    {
                        categoryList = new List<GoodsCategory>();
                    }
                    else
                    {
                        categoryList = response.result;
                    }
                    categoryList.Insert(0, new GoodsCategory() { categoryCode = "", categoryName = "全部" });
                    cbThree.DataSource = categoryList;
                    cbThree.DisplayMember = "categoryName";
                    cbThree.ValueMember = "categoryCode";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载异常" + ex.Message);
            }
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
                    if (column.Name == "oper")
                    {
                        String skucode = this.dataGridView1.CurrentRow.Cells["skuCode"].Value == null ? "" : this.dataGridView1.CurrentRow.Cells["skuCode"].Value.ToString();
                        String name = this.dataGridView1.CurrentRow.Cells["goodsName"].Value == null ? "" : this.dataGridView1.CurrentRow.Cells["goodsName"].Value.ToString();
                        String goodsModel = this.dataGridView1.CurrentRow.Cells["goodsModel"].Value == null ? "" : this.dataGridView1.CurrentRow.Cells["goodsModel"].Value.ToString();
                        String goodsGrade = this.dataGridView1.CurrentRow.Cells["goodsGrade"].Value == null ? "" : this.dataGridView1.CurrentRow.Cells["goodsGrade"].Value.ToString();
                        curSkuCode = skucode;
                        curGoodName = name;
                        curGoodsGrade = goodsGrade;
                        curGoodsModel = goodsModel;

                        PrintDocument document = new PrintDocument();
                        document.DefaultPageSettings.PaperSize = new PaperSize("Custum", 270, 180);

#if(!DEBUG)
                                PrintDialog dialog = new PrintDialog();
                                document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
                                dialog.Document = document;
#else
                        PrintPreviewDialog dialog = new PrintPreviewDialog();
                        document.PrintPage += new PrintPageEventHandler(this.pd_PrintBoxPage);
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
                    }
                }
            }
        }

        private void pd_PrintBoxPage(object sender, PrintPageEventArgs e) //触发打印事件
        {
            Bitmap bt = CreateQRCode(curSkuCode);
            GetPrintPicture(bt, e);
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


        public void GetPrintPicture(Bitmap image, PrintPageEventArgs g)
        {


            Font fontCu = new Font("宋体", 12f, FontStyle.Bold);

            Font font = new Font("宋体", 10f);
            Brush brush = new SolidBrush(Color.Black);
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //int pointX = 10;

            //左上角二维码
            Rectangle dest2Rect = new Rectangle(30, 5, image.Width, image.Height);
            g.Graphics.DrawImage(image, dest2Rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);


            //商品编码
            RectangleF layoutRectangleRight = new RectangleF(135, 20, 300f, 85f);
            g.Graphics.DrawString("商品编码:" + curSkuCode, font, brush, layoutRectangleRight);


            //商品名称
            layoutRectangleRight = new RectangleF(50, 85, 160f, 85f);
            g.Graphics.DrawString(curGoodName, new Font("宋体", 8f), brush, layoutRectangleRight);


            //规格，等级
            RectangleF layoutRectangle = new RectangleF(50, 125, 180f, 30f);
            g.Graphics.DrawString(curGoodsModel + "," + curGoodsGrade, new Font("宋体", 8f), brush, layoutRectangle);

            //右下角二维码
            Rectangle rightCodeLayout = new Rectangle(200, 80, image.Width, image.Height);
            g.Graphics.DrawImage(image, rightCodeLayout, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

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


    }


    public delegate void CheckBoxClickedHandler(bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }


    class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

        public event CheckBoxClickedHandler OnCheckBoxClicked;

        public DatagridViewCheckBoxHeaderCell()
        {
        }

        protected override void Paint(System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X +
                (cellBounds.Width / 2) - (s.Width / 2);
            p.Y = cellBounds.Location.Y +
                (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, _cbState);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <=
                checkBoxLocation.X + checkBoxSize.Width
            && p.Y >= checkBoxLocation.Y && p.Y <=
                checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(_checked);
                    this.DataGridView.InvalidateCell(this);
                }

            }
            base.OnMouseClick(e);
        }
    }
}

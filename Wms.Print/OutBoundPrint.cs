using Wms.Print.DocumentObject;
using Wms.Print.PrinterPageSetting;
using Wms.Print.Service.Base;
using Wms.Print.Service.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wms.Print
{
   public  class OutBoundPrint
    {

               /// <summary>
        /// 构造函数
        /// </summary>
        public OutBoundPrint()
        {
            PrinterSingleton.Reset();
            mCurrentPageIndex = 1;
            mCurrentRowIndex = 0;
            mCurrentRowIndex2 = 0;
            mPrintDocument = PrinterSingleton.PrintDocument;
            mPrintDocument.DefaultPageSettings.Margins = new Margins(50, 50, 25, 140);
            mPrintDocument.DefaultPageSettings.Landscape = false;
            mPrinterMargins = PrinterSingleton.PrinterMargins;
            mPrinter = new Service.Base.Printer();
            mBody1 = new Body();
            //mBody2 = new Body();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pIsLandscape">是横向还是纵向</param>
        /// <param name="margins">四边边距</param>
        public OutBoundPrint(bool pIsLandscape, Margins margins)
        {
            PrinterSingleton.Reset();
            mCurrentPageIndex = 1;
            mCurrentRowIndex = 0;
            mCurrentRowIndex2 = 0;
            mPrintDocument = PrinterSingleton.PrintDocument;
            mPrintDocument.DefaultPageSettings.Margins = margins;
            mPrintDocument.DefaultPageSettings.Landscape = pIsLandscape;
            mPrinterMargins = PrinterSingleton.PrinterMargins;
            mPrinter = new Service.Base.Printer();
            mBody1 = new Body();
            //mBody2 = new Body();
            mHeader = new Header();//
            mMultiHeader1 = new MultiHeader();//
         //   mHeader.IsDrawAllPage = true;
        }

        /// <summary>
        /// 标题跟子标题之间的距离
        /// </summary>
        private const int CON_SPACEmTitlemCaption = 5;
        /// <summary>
        /// 子标题跟顶部的距离
        /// </summary>
        private const int CON_SPACEmCaptionmTop = 20;
        /// <summary>
        /// 头部跟主体间距离
        /// </summary>
        private const int CON_SPACEmHeadermBody = 5;
        /// <summary>
        /// 主体跟页脚的距离
        /// </summary>
        private const int CON_SPACEmBodymFooter = 5;
        /// <summary>
        /// body1和body2之间的最大距离
        /// </summary>
        private const int CON_SPACEMaxBody = 60;
        /// <summary>
        /// body1和body2之间的最小距离
        /// </summary>
        private const int CON_SPACEMinBody = 10;
        /// <summary>
        /// 是否已经打印过Bottom（各种费用）
        /// </summary>
        private bool IsPrintedBottom = false;
        /// <summary>
        /// 下一对象的坐标及宽
        /// </summary>
        private int X, Y, Width;
        /// <summary>
        /// 缩放比例
        /// </summary>
        private float mScale = 1.0F;
        /// <summary>
        /// 当前页
        /// </summary>
        private int mCurrentPageIndex;
        /// <summary>
        /// 主数据网格的当前行
        /// </summary>
        private int mCurrentRowIndex;
        /// <summary>
        /// body2当前行
        /// </summary>
        private int mCurrentRowIndex2;
        /// <summary>
        /// 绘图表面
        /// </summary>
        private Graphics mGraphics;
        /// <summary>
        /// 打印对象
        /// </summary>
        private Service.Base.Printer mPrinter;

        private PrintDocument mPrintDocument;
        /// <summary>
        /// 打印文档
        /// </summary>
        public PrintDocument PrintDocument
        {
            get { return mPrintDocument; }
        }

        /// <summary>
        /// 打印区域
        /// </summary>
        private PrinterMargins mPrinterMargins;

        /// <summary>
        /// 是否单色(黑色)打印(默认是)
        /// </summary>
        private bool mIsOnlySingleColor = true;
        /// <summary>
        /// 背景色,默认白
        /// </summary>
        public Color BackColor = Color.White;
        /// <summary>
        /// 打印文档的名称
        /// </summary>
        public string DocumentName
        {
            get
            {
                return mPrintDocument.DocumentName;
            }
            set
            {
                mPrintDocument.DocumentName = value;
            }
        }
        /// <summary>
        /// 打印边距
        /// </summary>
        public Margins DocumentMargins
        {
            set
            {
                mPrintDocument.DefaultPageSettings.Margins = value;
            }
        }

        private int mRowsPerPage = -1;
        /// <summary>
        /// 每页的行数,小于等于0则表示自适应,默认就是这样的
        /// </summary>
        public int RowsPerPage
        {
            get { return mRowsPerPage; }
            set { mRowsPerPage = value; }
        }

        private bool mIsPrinterMargins = false;
        /// <summary>
        /// 是否打印有效区域矩阵,默认否
        /// </summary>
        public bool IsPrinterMargins
        {
            get { return mIsPrinterMargins; }
            set { mIsPrinterMargins = value; }
        }

        private GridBorderFlag mGridBorder = GridBorderFlag.Single;
        /// <summary>
        /// 网格边框,默认单线边框
        /// </summary>
        public GridBorderFlag GridBorder
        {
            get { return mGridBorder; }
            set { mGridBorder = value; }
        }

        private Bottom mBottom;
        /// <summary>
        /// 网格底
        /// </summary>
        public Bottom Bottom
        {
            get { return mBottom; }
            set
            {
                //if(value != null)
                //{
                //    if(value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.String[]")
                //    {
                //        if(this.mBottom == null)
                //        {
                //            this.mBottom = new Bottom();
                //        }
                //        this.mBottom.DataSource = (string)value;
                //    }
                //    else if(value.GetType().ToString() == "Vancl.Printer.Bottom")
                //    {
                //        this.mBottom = (Bottom)value;
                //    }
                //}
                mBottom = value;
            }
        }

        /*
        private Ender mEnder;
        /// <summary>
        /// 底部
        /// </summary>
        public Ender Ender
        {
            get { return mEnder; }
            set
            {
                //if(value != null)
                //{
                //    if(value.GetType().ToString() == "System.String")
                //    {
                //        if(mEnder == null)
                //            mEnder = new Ender();
                //        mEnder.Text = (string)value;
                //    }
                //    else if(value.GetType().ToString() == "Vancl.Printer.Ender")
                //    {
                //        mEnder = (Ender)value;
                //    }
                //}
                mEnder = value;
            }
        }*/

        private Header mHeader;
        /// <summary>
        /// 网格头部说明部分
        /// </summary>
        public Header Header
        {
            get { return mHeader; }
            set
            {
                //if(value != null)
                //{
                //    string type = value.GetType().ToString();
                //    if(type == "System.String[]" || type == "System.String[,]" || type == "System.Data.DataTable")
                //    {
                //        if(mHeader == null)
                //            mHeader = new Header();
                //        mHeader.DataSource = value;
                //    }
                //    else if(type == "Vancl.Printer.Header")
                //    {
                //        mHeader = (Header)value;
                //    }
                //}
                mHeader = value;
            }
        }

        private Footer mFooter;
        /// <summary>
        /// 网格下部说明
        /// </summary>
        public Footer Footer
        {
            get { return mFooter; }
            set
            {
                //if(value != null)
                //{
                //    string type = value.GetType().ToString();
                //    if(type == "System.String[]" || type == "System.String[,]" || type == "System.Data.DataTable")
                //    {
                //        if(this.mFooter == null)
                //        {
                //            this.mFooter = new Footer();
                //        }
                //        this.mFooter.DataSource = value;
                //    }
                //    else if(type == "Vancl.Printer.Footer")
                //    {
                //        this.mFooter = (Footer)value;
                //    }
                //}
                mFooter = value;
            }
        }

        private MultiHeader mMultiHeader1;
        /// <summary>
        /// 第一个网格体对应的标题
        /// </summary>
        public MultiHeader MultiHeader1
        {
            get { return mMultiHeader1; }
            set { mMultiHeader1 = value; }
        }

        //private MultiHeader mMultiHeader2;
        ///// <summary>
        ///// 第二个网格体对应的标题
        ///// </summary>
        //public MultiHeader MultiHeader2
        //{
        //    get { return mMultiHeader2; }
        //    set { mMultiHeader2 = value; }
        //}
        private Body mBody1;
        /// <summary>
        /// 第一个网格体
        /// </summary>
        public Body Body1
        {
            get { return mBody1; }
            set { mBody1 = value; }
        }
        /*
        private Body mBody2;
        /// <summary>
        /// 第二个网个体
        /// </summary>
        public Body Body2
        {
            get { return mBody2; }
            set { mBody2 = value; }
        }*/
        private BarCodeObject mBarCode;
        /// <summary>
        /// barCode
        /// </summary>
        public BarCodeObject BarCode
        {
            get { return mBarCode; }
            set { mBarCode = value; }
        }


        private Strings mStrings1;
        /// <summary>
        /// 字符串1
        /// </summary>
        public Strings Strings1
        {
            get { return mStrings1; }
            set { mStrings1 = value; }
        }

        private LogoAndBarCode vipLogo;
        /// <summary>
        /// vip、svip的图片、文字
        /// </summary>
        public LogoAndBarCode VipLogo
        {
            get { return vipLogo; }
            set { vipLogo = value; }
        }
        //private Strings mStrings2;
        ///// <summary>
        ///// 字符串2
        ///// </summary>
        //public Strings Strings2
        //{
        //    get { return mStrings2; }
        //    set { mStrings2 = value; }
        //}
        private Strings mulitHead1BeforeString;
        /// <summary>
        ///MultiHead1之前的字符串
        /// </summary>
        public Strings MulitHead1BeforeString
        {
            get { return mulitHead1BeforeString; }
            set { mulitHead1BeforeString = value; }
        }
        private Strings mulitHead2AfterString;
        /// <summary>
        ///MultiHead2之后的字符串
        /// </summary>
        public Strings MulitHead2AfterString
        {
            get { return mulitHead2AfterString; }
            set { mulitHead2AfterString = value; }
        }


        private Strings splitString;
        /// <summary>
        /// 分割线
        /// </summary>
        public Strings SplitString
        {
            get { return splitString; }
            set { splitString = value; }
        }
        private Strings spaceLine;
        /// 分割线
        /// </summary>
        public Strings SpaceLine
        {
            get { return spaceLine; }
            set { spaceLine = value; }
        }
        /*
        private LogoAndBarCode barcode2;
        ///<summary>
        /// 第二个条形码
        ///</summary>
        public LogoAndBarCode Barcode2
        {
            get { return barcode2; }
            set { barcode2 = value; }
        }*/
        /// <summary>
        /// 打印或显示设置对话框确定后打印。
        /// </summary>
        public PrinterSettings PrintSetup()
        {
            this.mCurrentPageIndex = 1;
            this.mCurrentRowIndex = 0;
            mCurrentRowIndex2 = 0;
            PrinterPageSetting.PrinterPageSetting printerPageSetting;
            printerPageSetting = new PrinterPageSetting.PrinterPageSetting(mPrintDocument);
            printerPageSetting.PrintPage += new PrintPageDelegate(this.printerPageSetting_PrintPage);

            return printerPageSetting.ShowPrintSetupDialog();
        }

        /// <summary>
        /// 直接打印不出现打印机设置对话框。
        /// </summary>
        public void Print()
        {
            //直接打印
            this.mCurrentPageIndex = 1;
            this.mCurrentRowIndex = 0;
            mCurrentRowIndex2 = 0;
            this.mPrintDocument.PrintPage += new PrintPageEventHandler(this.printerPageSetting_PrintPage);

            try
            {
                this.mPrintDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("打印机错误！\n{0}", ex.Message));
            }
        }

        /// <summary>
        /// 打印预览对话框。
        /// </summary>
        public void Preview()
        {
            
            this.mCurrentPageIndex = 1;
            this.mCurrentRowIndex = 0;
            mCurrentRowIndex2 = 0;
            PrinterPageSetting.PrinterPageSetting printerPageSetting;
            printerPageSetting = new PrinterPageSetting.PrinterPageSetting(mPrintDocument);
       
            printerPageSetting.PrintPage += new PrintPageDelegate(this.printerPageSetting_PrintPage);
            printerPageSetting.ShowPrintPreviewDialog();
        }

        /// <summary>
        /// printerPageSetting_PrintPage
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ev"></param>
        private void printerPageSetting_PrintPage(object obj, PrintPageEventArgs ev)
        {
            Graphics g = ev.Graphics;
            this.mGraphics = g;
            try
            {
                bool more = Drag(g);
                if (more)
                {
                    ev.HasMorePages = true;
                    mCurrentPageIndex++;
                }
                else
                {
                    ev.HasMorePages = false;
                    //mCurrentPageIndex = 1;
                    mCurrentRowIndex = 0;
                    mCurrentRowIndex2 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 控制打印BarCode
        /// </summary>
        private bool isPrint = true;
        /// <summary>
        ///控制打印
        /// </summary>
        public bool IsPrint
        {
            get { return isPrint; }
            set { isPrint = value; }
        }
        /// <summary>
        /// Drag
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private bool Drag(Graphics g)
        {
            bool blnHasMorePage = false;		//是否还有下一页标记

            if (mBody1 == null || mBody1.Rows < 0)
            {
                throw new Exception("打印主要网格不能为空，请用Body设置！");
            }
           // if (mBarCode == null) throw new Exception("条码不能为空");
            mPrinter.Graphics = g;
            mPrinter.PrintDocument = this.mPrintDocument;
            mPrinter.PrinterMargins = this.mPrinterMargins;
            //初起打印起点坐标及打印区域的宽
            Y = mPrinter.PrinterMargins.Top;
            X = mPrinter.PrinterMargins.Left;
            Width = mPrinter.PrinterMargins.Width;
            //画打印区域及装订线
            this.DrawPrinterMargins(mPrinter);


            //if (mBarCode != null && (mCurrentPageIndex == 1 || mBarCode.IsDrawAllPage))//&&(mCurrentPageIndex == 1 || mLogoAndBarCode.IsDrawAllPage)
            //{
            //    if (isPrint)
            //    {
            //        mBarCode.PrinterMargins = mPrinterMargins;
            //        OutObject(mBarCode);
            //        isPrint = false;
            //    }
            //}
            mBarCode.PrinterMargins = mPrinterMargins;
            OutObject(mBarCode);

            Y += CON_SPACEmHeadermBody; //条码和header的距离

            if (mStrings1 != null && (mCurrentPageIndex == 1 || mStrings1.IsDrawAllPage))
            {
                OutObject(mStrings1);
            }

            if (vipLogo != null && (mCurrentPageIndex == 1 || vipLogo.IsDrawAllPage))
            {
                OutObject(vipLogo);
            }

            //启用实际宽度
            int lngInfactWidth = 0;

            if (!this.mBody1.IsAverageColsWidth)
            {
                for (int i = 0; i < mBody1.ColsWidth.Length; i++)
                {
                    lngInfactWidth += mBody1.ColsWidth[i];
                }

                if (lngInfactWidth > this.mPrinterMargins.Width)
                {
                    //缩放
                    mScale = this.mPrinterMargins.Width / lngInfactWidth;
                }
                else
                {
                    Width = lngInfactWidth;
                    //X += (this.mPrinterMargins.Width - Width) / 2;
                }
            }

            OutObject(mHeader);
            //if (mHeader != null && (mCurrentPageIndex == 1 || mHeader.IsDrawAllPage))
            //{
            //    OutObject(mHeader);
            //}
            if (mulitHead1BeforeString != null && (mCurrentPageIndex == 1 || mulitHead1BeforeString.IsDrawAllPage))
            {
                OutObject(mulitHead1BeforeString);
            }
            //if(mMultiHeader1 != null && (mCurrentPageIndex == 1 || mMultiHeader1.IsDrawAllPage))
            //仅当body1还有行未打印时，才打印multiheader1
            //if (mMultiHeader1 != null && mCurrentRowIndex < mBody1.Rows)
            //{
            //    OutObject(mMultiHeader1);
            //}
            OutObject(mMultiHeader1);

            #region 主体数据网格
            //计算有效高度，便于分页
            float validHeight = mPrinter.PrinterMargins.Height - (Y - mPrinter.PrinterMargins.Top);// -mPrinter.PrinterMargins.Bottom;
            //if(mMultiHeader2==null)
            //{
            //validHeight = validHeight + 160;
            //}
            if (mFooter != null && mFooter.IsDrawAllPage)
            {
                validHeight -= this.mFooter.Height;
            }
            if (mBottom != null && mBottom.IsDrawAllPage)
            {
                validHeight -= this.mBottom.Height;
            }
            if (validHeight < 0)
            {
                throw new Exception("预留给打印主要网格的空间太小，请适当调整！");
            }

            //有效高度中当前页行数
            int mRowsInCurPage = 0;
            mRowsInCurPage = (int)(validHeight / (float)(this.mBody1.RowHeight));
           // mRowsInCurPage = (int)(validHeight / (float)(42));
            //如果指定每页行数，则以其为主
            if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
            {
                mRowsInCurPage = this.RowsPerPage;
            }

            //************以Body为主************

            string[,] mArrGridText;			//保留当前页文本，用于页小计
            Body mbody;

            //如果指定每页行数，则以其为主
            if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
            {
                mbody = new Body(mRowsInCurPage, this.mBody1.Cols);
            }
            else
            {
                //否则自适应
                if (mRowsInCurPage > (this.mBody1.Rows - this.mCurrentRowIndex))
                {
                    mRowsInCurPage = this.mBody1.Rows - this.mCurrentRowIndex;
                }
                mbody = new Body(mRowsInCurPage, this.mBody1.Cols);
            }

            //存当前页的二维文本
            mArrGridText = new string[mRowsInCurPage, this.mBody1.Cols];
            for (int i = 0; i < mRowsInCurPage && mCurrentRowIndex < this.mBody1.Rows; i++)
            {
                for (int j = 0; j < this.mBody1.Cols; j++)
                {
                    mArrGridText[i, j] = this.mBody1.GetText(mCurrentRowIndex, j);
                }
                mCurrentRowIndex++;
            }
            mbody.DrawGrid.Fonts = mBody1.DrawGrid.Fonts;
            mbody.DrawGrid.Merge = mBody1.DrawGrid.Merge;
            mbody.GridText = mArrGridText;

            mbody.ColsAlignString = this.mBody1.ColsAlignString;
            mbody.ColsWidth = this.mBody1.ColsWidth;
            mbody.IsAverageColsWidth = this.mBody1.IsAverageColsWidth;
            mbody.Font = (Font)(this.mBody1.Font.Clone());
            if (mbody.Rows > 0) OutObject(mbody);
            if (mCurrentRowIndex < this.mBody1.Rows)
            {
                blnHasMorePage = true;
            }
            #endregion

            #region 款项 bottom
            //有bottom 并且 没有打印过bottom，并且有空余的地方&& !IsPrintedBottom
            if (mBottom != null && !blnHasMorePage)
            {
                Y += CON_SPACEmBodymFooter;
                //计算有效高度
                validHeight = mPrinter.PrinterMargins.Height - Y - mPrinter.PrinterMargins.Bottom;
                if (validHeight < mBottom.Height)
                {
                    blnHasMorePage = true;
                }
                else
                {
                    OutObject(mBottom);
                    //IsPrintedBottom = true;
                }
            }
            //if(mBottom != null)
            //{
            //    if(blnHasMorePage == false || mBottom.IsDrawAllPage)
            //    {
            //        Y += CON_SPACEmBodymFooter;
            //        if(mBottom.IsDrawAllPage)
            //        {
            //            OutObject(mBottom);
            //        }
            //        else
            //        {
            //            //计算有效高度
            //            validHeight = mPrinter.PrinterMargins.Height - (Y - mPrinter.PrinterMargins.Top);
            //            if(validHeight < mBottom.Height)
            //            {
            //                blnHasMorePage = true;
            //            }
            //            else
            //            {
            //                OutObject(mBottom);
            //            }
            //        }
            //    }
            //}
            #endregion



            if ((mFooter != null || mBottom != null) && (mCurrentPageIndex == 1 || (mHeader != null && mHeader.IsDrawAllPage)))
            {
                Y += CON_SPACEmBodymFooter;			//网格与页底距离
            }

            //打印页脚与最底
            if (mFooter != null)
            {
                //最后一页必打
                if (blnHasMorePage == false || mFooter.IsDrawAllPage)
                {
                    //如果每页都打印，对mFooter分页失去了意义
                    if (mFooter.IsDrawAllPage)
                    {
                        Strings s = new Strings();
                        s.Text = "第" + mCurrentPageIndex+"页";
                        OutObject(s);
                      //  OutObject(mFooter);
                    }
                    else
                    {
                        //不是每都打，但是最后一页必打mFooter，这时要做分页处理
                        //与Body同样的处理
                        //...
                    }
                }
            }

            //if (mEnder != null)
            //{
            //    if (blnHasMorePage == false || mEnder.IsDrawAllPage)
            //    {
            //        if (mEnder.IsDrawAllPage)
            //        {
            //            OutObject(mEnder);
            //        }
            //        else
            //        {
            //            //计算有效高度
            //            validHeight = mPrinter.PrinterMargins.Height - Y - mPrinter.PrinterMargins.Bottom;
            //            if (validHeight < mEnder.Height)
            //            {
            //                blnHasMorePage = true;
            //            }
            //            else
            //            {
            //                OutObject(mEnder);
            //            }

            //        }
            //    }
            //}

            //画边框
            DrawBorder(g, this.mMultiHeader1, mbody);

            mbody.Dispose();
            mbody = null;

            return blnHasMorePage;
        }

        /// <summary>
        /// 对象打印接口
        /// </summary>
        /// <param name="outer"></param> 
        private void OutObject(Service.Base.Printer outer)
        {
            if (outer != null)
            {
                outer.Graphics = this.mGraphics;
                outer.Rectangle = new Rectangle(X, Y, Width, outer.Height);

                if (this.mIsOnlySingleColor)
                {
                    outer.Pen = Pens.Black;
                    outer.Brush = Brushes.Black;
                }

                outer.Draw();
                this.Y += outer.Rectangle.Height;
            }
        }
        /// <summary>
        /// 画打印区域
        /// </summary>
        private void DrawPrinterMargins(Service.Base.Printer printer)
        {
            if (this.IsPrinterMargins)
            {
                printer.DrawPrinterMargins();
            }
        }

        /// <summary>
        /// 画边框
        /// </summary>
        /// <param name="g">图象</param>
        /// <param name="multiHeader">头</param>
        /// <param name="body">主体</param>
        private void DrawBorder(Graphics g, MultiHeader multiHeader, Body body)
        {
            //网格边框矩阵
            Rectangle mrecGridBorder;
            int x, y, width, height;

            width = body.Rectangle.Width;
            height = body.Rectangle.Height;
            if (multiHeader != null)
            {
                x = multiHeader.Rectangle.X;
                y = multiHeader.Rectangle.Y;
                height += multiHeader.Rectangle.Height;
            }
            else
            {
                x = body.Rectangle.X;
                y = body.Rectangle.Y;
            }

            mrecGridBorder = new Rectangle(x, y, width, height);
            Pen pen = new Pen(Color.Black, 1);

            DrawRectangle dr = new DrawRectangle();
            dr.Graphics = g;
            dr.Rectangle = mrecGridBorder;
            dr.Pen = pen;

            switch (GridBorder)
            {
                case GridBorderFlag.Single:
                    dr.Draw();
                    break;
                case GridBorderFlag.SingleBold:
                    dr.Pen.Width = 2;
                    dr.Draw();
                    if (multiHeader != null)
                    {
                        dr.Rectangle = body.Rectangle;
                        dr.DrawTopLine();
                    }
                    break;
                case GridBorderFlag.Double:
                    dr.Draw();
                    mrecGridBorder = new Rectangle(x - 2, y - 2, width + 4, height + 4);
                    dr.Rectangle = mrecGridBorder;
                    dr.Draw();
                    break;
                case GridBorderFlag.DoubleBold:
                    dr.Draw();
                    mrecGridBorder = new Rectangle(x - 2, y - 2, width + 4, height + 4);
                    dr.Rectangle = mrecGridBorder;
                    dr.Pen.Width = 2;
                    dr.Draw();
                    break;
            }
        }

        #region IDisposable 成员
        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            try
            {
                mGraphics.Dispose();
                mPrintDocument.Dispose();
            }
            catch
            {
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 打印基类(抽象类),描叙打印文档及边距信息
    /// </summary>
    public abstract class PrinterBase : DrawBase
    {
        #region 成员及属性
        private PrintDocument mPrintDocument;
        /// <summary>
        /// 获取或设置打印文档
        /// </summary>
        public PrintDocument PrintDocument
        {
            get { return mPrintDocument; }
            set
            {
                if (value != null)
                    mPrintDocument = value;
            }
        }

        private PrinterMargins mPrinterMargins;
        /// <summary>
        /// 获取或设置打印页的边距及有效打印宽及高
        /// </summary>
        public PrinterMargins PrinterMargins
        {
            get { return mPrinterMargins; }
            set
            {
                if (value != null)
                {
                    mPrinterMargins = value;
                    SetPageInfo();
                }
            }
        }

        private int mPageWidth;
        /// <summary>
        /// 页面宽
        /// </summary>
        public int PageWidth
        {
            get { return mPageWidth; }
        }

        private int mPageHeight;
        /// <summary>
        /// 页面高
        /// </summary>
        public int PageHeight
        {
            get { return mPageHeight; }
        }

        private int mLeftMargin;
        /// <summary>
        /// 页面左边距
        /// </summary>
        public int LeftMargin
        {
            get { return mLeftMargin; }
        }

        private int mRightMargin;
        /// <summary>
        /// 页面右边距
        /// </summary>
        public int RightMargin
        {
            get { return mRightMargin; }
        }

        private int mTopMargin;
        /// <summary>
        /// 页面上边距
        /// </summary>
        public int TopMargin
        {
            get { return mTopMargin; }
        }

        private int mBottomMargin;
        /// <summary>
        /// 页面下边距
        /// </summary>
        public int BottomMargin
        {
            get { return mBottomMargin; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public PrinterBase()
        {
            mPrintDocument = PrinterSingleton.PrintDocument;
            mPrinterMargins = PrinterSingleton.PrinterMargins;
            SetPageInfo();
        }

        /// <summary>
        /// 计算页面信息
        /// </summary>
        public void CalculatePageInfo()
        {
            this.mPrinterMargins = new PrinterMargins(this.PrintDocument);
            SetPageInfo();
        }

        /// <summary>
        /// 设置PrintDocument后可能影响到一些变量的变动，如PrintDocument改变了，就要重求PageWidth\Height等，装订变了，就会影响PrinterMargins等
        /// </summary>
        private void SetPageInfo()
        {
            //获取系统非打印区域边距
            this.mLeftMargin = this.PrinterMargins.Left;
            this.mTopMargin = this.PrinterMargins.Top;

            this.mRightMargin = this.PrinterMargins.Right;
            this.mBottomMargin = this.PrinterMargins.Bottom;

            //注意PrinterMargins.Width/Height仅仅是打印区的宽与高
            this.mPageWidth = this.PrinterMargins.Width + this.mLeftMargin + this.mRightMargin;
            this.mPageHeight = this.PrinterMargins.Height + this.mTopMargin + this.mBottomMargin;


        }
    }
}

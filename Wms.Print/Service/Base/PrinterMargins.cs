using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Base
{
    /// <summary>
    /// 用于描述打印页的边距及有效打印宽及高
    /// </summary>
    public class PrinterMargins : Margins
    {
        #region 字段属性
        private int mWidth;
        /// <summary>
        /// 有效打印宽
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }

        private int mHeight;
        /// <summary>
        /// 有效打印高
        /// </summary>
        public int Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        private int mX1;
        /// <summary>
        /// 有效打印区左边边界的X横坐标
        /// </summary>
        public int X1
        {
            get { return mX1; }
        }

        private int mX2;
        /// <summary>
        /// 有效打印区右边边界的X横坐标
        /// </summary>
        public int X2
        {
            get { return mX2; }
        }

        private int mY1;
        /// <summary>
        /// 有效打印区上边界的Y纵坐标
        /// </summary>
        public int Y1
        {
            get { return mY1; }
        }

        private int mY2;
        /// <summary>
        /// 有效打印区下边界的Y纵坐标
        /// </summary>
        public int Y2
        {
            get { return mY2; }
        }
        #endregion

        /// <summary>
        /// 使用1个单位初始化
        /// </summary>
        public PrinterMargins()
            : this(1, 1, 1, 1, 0, 0)
        { }
        /// <summary>
        /// 用指定的边距及有效打印宽、高初始类的新实例
        /// </summary>
        /// <param name="left">左边距</param>
        /// <param name="right">右边距</param>
        /// <param name="top">上边距</param>
        /// <param name="bottom">下边距</param>
        /// <param name="width">有效打印区的宽</param>
        /// <param name="height">有效打印区的高</param>
        public PrinterMargins(int left, int right, int top, int bottom, int width, int height)
            : base(left, right, top, bottom)
        {
            mWidth = width;
            mHeight = height;
            Calculate();
        }

        /// <summary>
        /// 使用指定的文档初始化
        /// </summary>
        /// <param name="printDocument"></param>
        public PrinterMargins(PrintDocument printDocument)
        {
            PrinterMargins printerMargins = new PrinterMargins();
            printerMargins = GetPrinterMargins(printDocument);

            this.Left = printerMargins.Left;
            this.Right = printerMargins.Right;
            this.Top = printerMargins.Top;
            this.Bottom = printerMargins.Bottom;
            this.Width = printerMargins.Width;
            this.Height = printerMargins.Height;
            printerMargins = null;
            Calculate();
        }

        /// <summary>
        /// 通过PrintDocument获取PrinterMargins
        /// </summary>
        /// <param name="printDocument"></param>
        private PrinterMargins GetPrinterMargins(PrintDocument printDocument)
        {
            PrinterMargins printerMargins;
            int left, right, top, bottom, width, height;
            left = printDocument.DefaultPageSettings.Margins.Left;
            right = printDocument.DefaultPageSettings.Margins.Right;
            top = printDocument.DefaultPageSettings.Margins.Top;
            bottom = printDocument.DefaultPageSettings.Margins.Bottom;
            //left = right = top = bottom = 10;
            width = printDocument.DefaultPageSettings.PaperSize.Width;
            height = printDocument.DefaultPageSettings.PaperSize.Height;

            if (printDocument.DefaultPageSettings.Landscape)
            {
                Swap(ref width, ref height);
            }

            //打印区的高宽应减去边距
            width = width - left - right;
            height = height - top - bottom;

            printerMargins = new PrinterMargins(left, right, top, bottom, width, height);
            return printerMargins;
        }

        /// <summary>
        /// 交换两数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(ref int i, ref int j)
        {
            int tmp = i;
            i = j;
            j = tmp;
        }

        /// <summary>
        /// 计算X1,X2,Y1,Y2
        /// </summary>
        private void Calculate()
        {
            mX1 = this.Left;
            mX2 = this.Left + mWidth;
            mY1 = this.Top;
            mY2 = this.Top + mHeight;
        }
    }
}

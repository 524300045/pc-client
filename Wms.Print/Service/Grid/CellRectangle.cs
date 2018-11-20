using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.Service.Grid
{
    /// <summary>
    /// 网格单元格矩阵,描述左顶宽高
    /// </summary>
    public struct CellRectangle
    {
        private int mCellLeft, mCellTop, mCellWidth, mCellHeight;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellLeft"></param>
        /// <param name="cellTop"></param>
        /// <param name="cellWidth"></param>
        /// <param name="cellHeight"></param>
        public CellRectangle(int cellLeft, int cellTop, int cellWidth, int cellHeight)
        {
            mCellLeft = cellLeft;
            mCellTop = cellTop;
            mCellWidth = cellWidth;
            mCellHeight = cellHeight;
        }

        /// <summary>
        /// 单元格起点X坐标
        /// </summary>
        public int Left
        {
            get
            {
                return mCellLeft;
            }
            set
            {
                this.mCellLeft = value;
            }
        }

        /// <summary>
        /// 单元格起点Y坐标
        /// </summary>
        public int Top
        {
            get
            {
                return mCellTop;
            }
            set
            {
                this.mCellTop = value;
            }
        }

        /// <summary>
        /// 单元格宽
        /// </summary>
        public int Width
        {
            get
            {
                return mCellWidth;
            }
            set
            {
                this.mCellWidth = value;
            }
        }

        /// <summary>
        /// 单元格高
        /// </summary>
        public int Height
        {
            get
            {
                return mCellHeight;
            }
            set
            {
                this.mCellHeight = value;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Print.Service.Grid;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// 多层表头。
    /// </summary>
    public class MultiHeader : Header
    {
        private const int CONST_MAX_ROWS = 3;
        private bool mIsDrawDiagonalLine;		//是否打印第一列的对角线起点为0行0列，指定行数为终点
        private float mDiagonalLineRows;		//行数，可以是小数如1.5

        #region 字段属性
        /// <summary>
        /// 是否打印第一列的对角线，要指定行数
        /// </summary>
        public bool IsDrawDiagonalLine
        {
            get
            {
                return mIsDrawDiagonalLine;
            }
            set
            {
                mIsDrawDiagonalLine = value;
            }
        }

        /// <summary>
        /// 对角线起点为0行0列，指定行数为终点。行数可以是小数如1.5
        /// </summary>
        public float DiagonalLineRows
        {
            get
            {
                return mDiagonalLineRows;
            }
            set
            {
                mDiagonalLineRows = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MultiHeader()
        {
            this.IsDrawAllPage = true;
            mdrawGrid.AlignMent = AlignFlag.Center;
            mdrawGrid.Border = GridBorderFlag.Single;
            mdrawGrid.Line = GridLineFlag.Both;
            this.IsAverageColsWidth = false;

            mIsDrawDiagonalLine = false;
            mDiagonalLineRows = 2;

            //粗体显示并合并
            mdrawGrid.Merge = GridMergeFlag.Any;
            this.Font = new Font("宋体", 12, FontStyle.Bold);

            mdrawGrid.PreferredRowHeight = this.Font.Height + 10;
        }

        /// <summary>
        /// 设置最大行数
        /// </summary>
        /// <returns></returns>
        protected override int SetMaxRows()
        {
            return CONST_MAX_ROWS;
        }

        /// <summary>
        /// 合并行
        /// </summary>
        /// <param name="row">行索引</param>
        /// <param name="startCol">开始列</param>
        /// <param name="endCol">结束列</param>
        /// <param name="text">填充文字</param>
        public void SetMergeTextOnRowSel(int row, int startCol, int endCol, string text)
        {
            mdrawGrid.SetTextOnRowSel(row, startCol, endCol, text);
        }

        /// <summary>
        /// 合并列
        /// </summary>
        /// <param name="col">列索引</param>
        /// <param name="startRow">开始行</param>
        /// <param name="endRow">结束行</param>
        /// <param name="text">填充文字</param>
        public void SetMergeTextOnColSel(int col, int startRow, int endRow, string text)
        {
            mdrawGrid.SetTextOnColSel(col, startRow, endRow, text);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public MultiHeader(int rows, int cols)
            : this()
        {
            base.Initialize(rows, cols);

            string mstrAlignment = "";

            //所有列居中对齐
            for (int i = 0; i < cols; i++)
            {
                mstrAlignment += "C";
            }
            this.mdrawGrid.ColsAlignString = mstrAlignment;
        }

        /// <summary>
        /// 画对角线，仅限于第一列
        /// </summary>
        /// <param name="rows">行数</param>
        protected void DrawDiagonalLine(float rows)
        {
            try
            {
                int x1, y1, x2, y2;

                x1 = mdrawGrid.Rectangle.X;
                y1 = mdrawGrid.Rectangle.Y;

                x2 = x1 + mdrawGrid.ColsWidth[0];
                y2 = y1 + (int)(mdrawGrid.PreferredRowHeight * this.mDiagonalLineRows);

                this.Graphics.SetClip(new Rectangle(x1, y1, mdrawGrid.ColsWidth[0], mdrawGrid.PreferredRowHeight * mdrawGrid.Rows));

                this.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            }
            catch //(Exception e)
            { }
            finally
            {
                this.Graphics.ResetClip();
            }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (mIsDrawDiagonalLine)
            {
                DrawDiagonalLine(this.mDiagonalLineRows);
            }
        }

        /// <summary>
        /// 获取或设置各列对方方式字符串
        /// </summary>
        public string ColsAlign
        {
            get
            {
                return this.mdrawGrid.ColsAlignString;
            }
            set
            {
                this.mdrawGrid.ColsAlignString = value;
            }
        }
    }
}

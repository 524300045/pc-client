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
    /// Outer，网格体之外的对象，通常用于表头表底做表体的附加信息。   
    /// </summary>
    public class Outer : Service.Base.Printer, IDisposable
    {
        //平均列宽
        private bool mIsAverageColsWidth;

        #region 字段属性
        /// <summary>
        /// 是否平均分配列宽
        /// </summary>
        public bool IsAverageColsWidth
        {
            get
            {
                return mIsAverageColsWidth;
            }
            set
            {
                mIsAverageColsWidth = value;
            }
        }
        #endregion


        /// <summary>
        /// 核心为网格对象，不对最终用户公开
        /// </summary>
        protected DrawGrid mdrawGrid;
        /// <summary>
        /// 获取网格对象
        /// </summary>
        public DrawGrid DrawGrid
        {
            get { return mdrawGrid; }
        }


        /// <summary>
        /// 标识是否初始了行列数，Initialize(int rows, int cols) 只有初始了，才能执行Draw()操作。
        /// </summary>
        protected bool mblnHadInitialized;

        /// <summary>
        /// 
        /// </summary>
        public Outer()
        {
            mIsAverageColsWidth = true;

            mblnHadInitialized = false;
            this.IsDrawAllPage = false;

            mdrawGrid = new DrawGrid();

            mdrawGrid.AlignMent = AlignFlag.Left;
            mdrawGrid.ColsAlignString = "";
            mdrawGrid.Border = GridBorderFlag.None;
            mdrawGrid.Line = GridLineFlag.None;
            mdrawGrid.Merge = GridMergeFlag.None;

            this.Font = new Font("宋体", 11);

            mdrawGrid.PreferredRowHeight = this.Font.Height;
            mdrawGrid.Initialize(0, 0);

        }

        /// <summary>
        /// 获取或设置数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return this.mdrawGrid.DataSource;
            }
            set
            {
                this.mdrawGrid.DataSource = value;

                if (DataSource.GetType().ToString() == "System.String[]" || DataSource.GetType().ToString() == "System.String[,]" || DataSource.GetType().ToString() == "System.Data.DataTable")
                {
                    mblnHadInitialized = true;
                }
            }
        }

        /// <summary>
        /// 获取或设置文本
        /// </summary>
        public string[,] Text
        {
            get
            {
                return this.mdrawGrid.GridText;
            }
            set
            {
                this.mdrawGrid.GridText = value;
                mblnHadInitialized = true;
            }
        }

        /// <summary>
        /// 获取是否能执行绘制操作，只有初始了对象的行列数才可以执行Draw()操作
        /// </summary>
        /// <returns></returns>
        public bool CanDraw
        {
            get
            {
                return this.mblnHadInitialized;
            }
        }

        /// <summary>
        /// 获取或设置行高
        /// </summary>
        public int RowHeight
        {
            get
            {
                return mdrawGrid.PreferredRowHeight;
            }
            set
            {
                mdrawGrid.PreferredRowHeight = value;
            }

        }

        /// <summary>
        /// 初始行列数
        /// </summary>
        /// <param name="rows">初始对象的行数</param>
        /// <param name="cols">初始对象的列数</param>
        public virtual void Initialize(int rows, int cols)
        {
            mblnHadInitialized = true;
            mdrawGrid.Initialize(rows, cols);
        }

        /// <summary>
        /// 获取对象的行数
        /// </summary>
        public int Rows
        {
            get
            {
                return mdrawGrid.Rows;
            }
        }

        /// <summary>
        /// 获取对象的列数
        /// </summary>
        public int Cols
        {
            get
            {
                return mdrawGrid.Cols;
            }
        }

        /// <summary>
        /// 获取或设置对象的列宽
        /// </summary>
        public int[] ColsWidth
        {
            get
            {
                return mdrawGrid.ColsWidth;
            }
            set
            {
                mdrawGrid.ColsWidth = value;
            }

        }
        ///// <summary>
        ///// 获取或设置单元格合并方式
        ///// </summary>
        //public GridMergeFlag MergeFlag
        //{
        //    get
        //    {
        //        return mdrawGrid.Merge;
        //    }
        //    set
        //    {
        //        mdrawGrid.Merge = value;
        //    }
        //}

        /// <summary>
        /// 获取对象的高
        /// </summary>
        public override int Height
        {
            get
            {
                return mdrawGrid.Rows * mdrawGrid.PreferredRowHeight;
            }
        }

        /// <summary>
        /// 为对象指定单元设置文本值
        /// </summary>
        /// <param name="row">单元行</param>
        /// <param name="col">单元列</param>
        /// <param name="text">文本值</param>
        public virtual void SetText(int row, int col, string text)
        {
            mdrawGrid.SetText(row, col, text);
        }


        /// <summary>
        /// 用指定的行列分隔分隔的一串字符串，些操作默认已执行初始行列数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colSplit"></param>
        /// <param name="rowSplit"></param>
        public virtual void SetText(char rowSplit, char colSplit, string text)
        {
            mdrawGrid.SetText(rowSplit, colSplit, text);

            //mblnHadInitialized = true;
        }

        /// <summary>
        /// 获取对象指定单元文本值
        /// </summary>
        /// <param name="row">单元行</param>
        /// <param name="col">单元列</param>
        /// <returns></returns>
        public virtual string GetText(int row, int col)
        {
            return mdrawGrid.GetText(row, col);
        }

        /// <summary>
        /// 在绘图表面绘制对象绘制文本
        /// </summary>
        public override void Draw()
        {
            if (mblnHadInitialized)
            {
                base.Draw();

                //在指定的区域内绘制文本				
                mdrawGrid.Rectangle = new Rectangle((int)this.Rectangle.X + (int)this.MoveX, (int)this.Rectangle.Y + (int)this.MoveY, (int)this.Rectangle.Width, (int)this.Rectangle.Height);
                mdrawGrid.Graphics = this.Graphics;

                if (this.mIsAverageColsWidth)
                {
                    mdrawGrid.Width = mdrawGrid.Rectangle.Width;
                    mdrawGrid.ColsWidth = mdrawGrid.GetAverageColsWidth();
                }

                mdrawGrid.Draw();
            }
            else
            {
                throw new Exception("对象的行列数还未初始，请用Initialize（）进行操作！");
            }
        }

        #region IDisposable 成员
        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            if (mdrawGrid != null)
                this.mdrawGrid.Dispose();
        }

        #endregion

    }
}

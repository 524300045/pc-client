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
    /// Body，数据表格主题。    
    /// </summary>
    public class Body : Outer
    {

        /// <summary>
        /// 
        /// </summary>
        public Body()
        {
            this.IsDrawAllPage = false;
            mdrawGrid.AlignMent = AlignFlag.Left;
            mdrawGrid.Border = GridBorderFlag.Single;
            mdrawGrid.Line = GridLineFlag.Both;

            this.IsAverageColsWidth = false;

            //不合并
            mdrawGrid.Merge = GridMergeFlag.None;
            //this.Font = new Font("宋体",12);

            mdrawGrid.Font = new Font("宋体", 20);
            mdrawGrid.PreferredRowHeight = mdrawGrid.Font.Height + 2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public Body(int rows, int cols)
            : this()
        {
            base.Initialize(rows, cols);
        }

        /// <summary>
        /// 获取或设置文本
        /// </summary>
        public string[,] GridText
        {
            set
            {
                mblnHadInitialized = true;
                mdrawGrid.GridText = value;
            }
            get
            {
                return mdrawGrid.GridText;
            }
        }

        /// <summary>
        /// 获取或设置各列对齐方式字符串
        /// </summary>
        public string ColsAlignString
        {
            set
            {
                mdrawGrid.ColsAlignString = value;
            }
            get
            {
                return mdrawGrid.ColsAlignString;
            }
        }

        /// <summary>
        /// 获取或设置字体
        /// </summary>
        public override Font Font
        {
            get
            {
                return mdrawGrid.Font;
            }
            set
            {
                mdrawGrid.Font = value;
            }
        }


    }
}

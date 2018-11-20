using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// Footer，紧挨在网格体下的对象，行列数不受限制。
    /// </summary>
    public class Footer : Outer
    {
        /// <summary>
        /// 
        /// </summary>
        public Footer()
        {
            this.IsDrawAllPage = true;
            //mdrawGrid.AlignMent = AlignFlag.Center;
            //mdrawGrid.Border = GridBorderFlag.Single;
            //mdrawGrid.Line = GridLineFlag.Both;
            //this.IsAverageColsWidth = false;

            //粗体显示并合并
            //mdrawGrid.Merge = GridMergeFlag.Any;
            //this.Font = new Font("宋体", 12, FontStyle.Bold);
            //mdrawGrid.Font = new Font("宋体", 12);
            //mdrawGrid.PreferredRowHeight = this.Font.Height ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public Footer(int rows, int cols)
            : this()
        {
            base.Initialize(rows, cols);
            //string mstrAlignment = "";

            ////所有列居中对齐
            //for (int i = 0; i < cols; i++)
            //{
            //    mstrAlignment += "C";
            //}
            //this.mdrawGrid.ColsAlignString = mstrAlignment;
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
                mdrawGrid.PreferredRowHeight = value.Height;
            }
        }
    }
}

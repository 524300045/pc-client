using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// Header，紧挨在网格体上的对象，限制在10行内，列数不限。
    /// </summary>
    public class Header : Outer
    {
        private const int CONST_MAX_ROWS = 10;
        private readonly int MAX_ROWS;

        /// <summary>
        /// 
        /// </summary>
        public Header()
        {
            MAX_ROWS = SetMaxRows();
        }

        /// <summary>
        /// 设置最大行数
        /// </summary>
        /// <returns></returns>
        protected virtual int SetMaxRows()
        {
            return CONST_MAX_ROWS;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public Header(int rows, int cols)
            : this()
        {
            this.Initialize(rows, cols);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rows">行数</param>
        /// <param name="cols">列数</param>
        public override void Initialize(int rows, int cols)
        {
            int mrows = rows;

            if (mrows < 0)
            {
                mrows = 0;
            }

            if (mrows > MAX_ROWS)
            {
                throw new Exception("行数限制在“" + MAX_ROWS.ToString() + "”行以内！");
            }
            else
            {
                base.Initialize(mrows, cols);
            }
        }


    }
}

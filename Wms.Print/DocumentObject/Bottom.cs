using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Print.DocumentObject
{
    /// <summary>
    /// Bottom，提供一个一行三列的对象，第一列居左，第三列居右，中间一旬居中。默认每页重复打印。    
    /// </summary>
    public class Bottom : Top
    {
        /// <summary>
        /// 
        /// </summary>
        public Bottom()
        {
            this.IsDrawAllPage = true;
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

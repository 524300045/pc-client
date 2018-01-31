using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public   class GoodsCategory
    {
        /** 主键id */
       public long id { get; set; }
        /** 编码 */
       public String categoryCode { get; set; }
        /** 分类名称 */
       public String categoryName { get; set; }
       
      
    }
}

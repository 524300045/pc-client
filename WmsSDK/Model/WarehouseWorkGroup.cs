using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class WarehouseWorkGroup
    {
        /** id */
       public long id { get; set; }
        /** 客户编码 */
       public String groupCode { get; set; }
        /** 客户名称 */
       public String groupName { get; set; }
        /** 仓库编码 */
       public String warehouseCode { get; set; }

       public String warehouseName { get; set; }
    
    }
}

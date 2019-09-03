using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class CustomerPrintConfig
    {
        /** 主键id */
        public long id { get; set; }
        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        public String warehouseName { get; set; }
        /** 客户编码 */
        public String customerCode { get; set; }
        /** 客户名称 */
        public String customerName { get; set; }
  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class WarehouseArea
    {

        /** 主键id */
        public long id { get; set; }
        /** 库区编码 */
        public String areaCode { get; set; }
        /** 库区名称 */
        public String areaName { get; set; }
        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        public String warehouseName { get; set; }
        /** 库区类型 */
        public int? type { get; set; }
        /** 默认品类 */
        public String category { get; set; }
        /** 温层 */
        public int? temperatureLayer { get; set; }
        /** 状态 */
        public int? status { get; set; }
        /** 创建时间 */
      
        /** 创建人 */
        public String createUser { get; set; }
        /** 更新时间 */
      
        /** 更新人 */
        public String updateUser { get; set; }
        /** 是否有效 */
       
    }
}

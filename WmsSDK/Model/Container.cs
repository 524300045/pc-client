using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class Container
    {

        public long id { get; set; }
        /** 容器编码 */
        public String containerCode { get; set; }
        /** 容器名称 */
        public String containerName { get; set; }
        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        public String warehouseName { get; set; }
        /** 库区编码 */
        public String areaCode { get; set; }
        /** 库区名称 */
        public String areaName { get; set; }
        /** 状态 */
        public int? status { get; set; }
        /** 容器条码 */
        public String barCode { get; set; }
        /** 创建时间 */

        /** 创建人 */
        public String createUser { get; set; }
        /** 更新时间 */
      
        /** 更新人 */
        public String updateUser { get; set; }


        public string StatusDes { get; set; }
    }
}

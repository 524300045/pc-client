using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class WmsFrozenBoxCode
    {
    
        /**  */
        public String warehouseCode { get; set; }
        /**  */
        public String warehouseName { get; set; }
        /**  */
        public String customerCode { get; set; }
        /**  */
        public String customerName { get; set; }
        /** 箱码 */
        public String boxCode { get; set; }
        /**  */
        public String skuCode { get; set; }

      
        /**  */
        public String goodsName { get; set; }
        /** 计价单位 */
        public String goodsUnit { get; set; }
        /**  */
        public String physicsUnit { get; set; }
        /** 包规 */
        public decimal modelNum { get; set; }
        /** 数量 */
        public decimal quantity { get; set; }
        /** 重量 */
        public decimal weight { get; set; }
        /** 剩余数量 */
        public decimal surplusQuantity { get; set; }
        /** 剩余重量 */
        public decimal surplusWeight { get; set; }
        /**  */
        public String productionDate { get; set; }
        /**  */
        public Double expiryDay { get; set; }
     
     
        /** 商品类型 */
        public int? goodsType { get; set; }

        //保鲜温区属性
        public int? freshAttr { get; set; }
    

    }
}

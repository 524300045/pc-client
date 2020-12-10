using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class InBoundDetailModel
    {

        public Int64 id { get; set; }
        /** 入库任务单号 */
        public String inboundTaskCode { get; set; }
        /** 状态 */
     
        /** 商品编码 */
        public String skuCode { get; set; }
        /** 商品名称 */
        public String goodsName { get; set; }
        /** 客户商品编码 */

        /** 采购单号 */
        public String orderNo { get; set; }
        /** 门店编码 */
  
        /** 原单据编号 */
        public String originOrderNo { get; set; }
        /** 是否生鲜 1：是 0：否 */
   
        /** 包规（数量） */
        public decimal modelNum { get; set; }
        /** 计价单位（斤、两） */
        public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
        public String physicsUnit { get; set; }
        /** 计划数量 */
        public decimal planNum { get; set; }
        /** 实际数量 */
        public decimal realityNum { get; set; }

        /** 创建人 */
        public String createUser { get; set; }
 
     

        /** 规格型号 */
        public String goodsModel { get; set; }



        /** 供应商编码 */
        public String partnerCode { get; set; }
        /** 供应商名称 */
        public String partnerName { get; set; }

        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        public String warehouseName { get; set; }

        public String customerCode { get; set; }

        public String customerName { get; set; }

        /**
         * 生产日期
         */
        public String productionDate { get; set; }

        public decimal boxNum { get; set; }


    }
}

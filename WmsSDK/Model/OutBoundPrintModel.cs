using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class OutBoundPrintModel
    {
        public int id { get; set; }
        /** 出库任务单号 */
        public String outboundTaskCode { get; set; }
        /** 状态 */
        public int status { get; set; }
        /** 是否打印 */
        public int isPrint { get; set; }
      
        /** 打印人 */
        public String printMan;
        /** 是否分拣完成 */
        public int isSorting;
      
        /** 是否发运完成 */
        public int? isDelivery { get; set; }
     
        /** 发运人 */
        public String deliveryMan { get; set; }
        /** 线路编码 */
        public String lineCode { get; set; }
        /** 车辆编码 */
        public String vehicleCode { get; set; }
        /** 采购单号 */
        public String orderNo { get; set; }
        /** 单据类型 */
        public int? orderType { get; set; }
        /** 单据来源 */
        public int? orderSource { get; set; }
        /** 业务类型 */
        public int? businessType { get; set; }
        /** 仓库编码 */
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        public String warehouseName { get; set; }
        /** 城市编码 */
        public String regionCode { get; set; }
        /** 门店编码 */
        public String storedCode { get; set; }
        /** 门店名称*/
        public String storedName { get; set; }
        /** 原单据编号 */
        public String originOrderNo { get; set; }
        /** 客户编码 */
        public String customerCode { get; set; }
        /** 业务员 */
        public String salesman { get; set; }
        /** 付款条件 */
        public String paymentClause { get; set; }
        /** 币种 */
        public int? currency { get; set; }
        /** 汇率 */
        public decimal exchangeRate { get; set; }
        /** 备注 */
        public String remark { get; set; }
    
      
        /** 创建人 */
        public String createUser { get; set; }
     
        /** 更新人 */
        public String updateUser { get; set; }
        /** 是否有效 */
        public int? yn { get; set; }

        /***是否发运打印*/
        public int? isDeliverPrint { get; set; }
        /**发运打印人*/
        public String deliverPrintUser { get; set; }

        public string deliveryDate { get; set; }


        public string receiver { get; set; }

        public string address { get; set; }

        public string receiverPhone { get; set; }

        public string customerPhone { get; set; }


        public decimal priceCount { get; set; }

        public string companyAddress { get; set; }

        public List<ShipMentDetailVo> detailList { get; set; }
    }
}

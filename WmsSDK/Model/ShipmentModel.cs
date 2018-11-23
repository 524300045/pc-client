using System ;
using System.Collections.Generic;
using System.Linq ;
using System.Text;

namespace WmsSDK.Model
{
    public  class ShipmentModel
    {
        public Int32 id { get; set; }
        /** 门店名称*/
        public String storedName { get; set; }

        /** 状态 */
        public int? status { get; set; }


        public string StatusDes { get; set; }

        public String lineName { get; set; }
        /**车辆名称*/
        public String vehicleName { get; set; }

        /** 分拣完成时间 */
        public string finishSortingTime { get; set; }

        /** 发运时间 */
        public String deliveryTime { get; set; }

        /** 发货日期 */
        public string deliveryDate { get; set; }

        /** 出库任务单号 */
        public String outboundTaskCode { get; set; }

        /** 箱号 */
        public String boxCode { get; set; }

        /***是否发运打印*/
        public int? isDeliverPrint { get; set; }

        /**
         * 箱数
         */
        public int packageNum { get; set; }

        /**发运总数*/
        public double deliveryNumCount { get; set; }

        /**实际发运量*/
        public decimal actualNumCount { get; set; }

        /**分拣数量总和*/
        public decimal sortingNumCount { get; set; }
        /**采购单号*/
        public String orderNo { get; set; }


        public string IsPrintDes { get; set; }


        public int? isPrint { get; set; }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class GoodsSuitBoxTransferDetailModel
    {

        public int id;

        public String customerCode { get; set; }
        public String customerName { get; set; }
        public String warehouseCode { get; set; }
        public String warehouseName { get; set; }

        public String childGoodsName { get; set; }

        public String skuCode { get; set; }


        public String boxCode { get; set; }


        public String productionDate { get; set; }


        public decimal weight { get; set; }


        public String childSkuCode { get; set; }

        public String childBarCode { get; set; }

        public decimal childWeight { get; set; }

        /**
         * 0:正常 1:已使用
         */
        public int status { get; set; }

        /**
         * 来源来源0:康安 1:供应商
         */
        public int source { get; set; }



        /** 创建人 */
        public String createUser { get; set; }

        /** 修改人 */
        public String updateUser { get; set; }

        /** 有效标示（1：有效；0：无效） */
        public int yn { get; set; }

        public String goodsName { get; set; }
    }
}

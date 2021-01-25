using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsSuitBoxRequest : IWMSRequest<GoodsSuitBoxResponse>
    {
        public string GetAPIPath()
        {
            return "/goodsSuitBoxTransfer/add";
        }

        public String customerCode { get; set; }
        public String customerName { get; set; }
        public String warehouseCode { get; set; }
        public String warehouseName { get; set; }


        public String skuCode { get; set; }


        public String boxCode { get; set; }


        public String productionDate { get; set; }


        public decimal weight { get; set; }



        /**
         * 0:正常 1:已使用
         */
        public int status { get; set; }

        /**
         * 来源来源0:康安 1:供应商
         */
        public int source { get; set; }

        public String createUser { get; set; }


        public List<GoodsSuitBoxTransferDetailModel> detail { get; set; }
    }
}

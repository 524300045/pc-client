using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class GoodsSuitBox
    {

        public long id { get; set; }
        public String customerCode { get; set; }
        public String customerName { get; set; }
        public String warehouseCode { get; set; }
        public String warehouseName { get; set; }
        public String skuCode { get; set; }
        public String boxCode { get; set; }
        public String productionDate { get; set; }
        public decimal weight { get; set; }

        public List<GoodsSuitBoxDetailResponse> detailList { get; set; }
    }
}

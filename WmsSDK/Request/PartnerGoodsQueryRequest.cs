using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PartnerGoodsQueryRequest : IWMSRequest<GoodsBarCodeResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/getPartnerGoodsBarCodeList";
        }

        [JsonProperty("skuCode")]
        public String skuCode;

        [JsonProperty("goodsName")]
        public String goodsName;


        [JsonProperty("isFresh")]
        public int? isFresh { get; set; }


        public string partnerCode { get; set; }

    

        [JsonProperty("twoCategoryCode")]
        public String twoCategoryCode { get; set; }

        [JsonProperty("categoryCode")]
        public String categoryCode { get; set; }

        [JsonProperty("threeCategoryCode")]
        public String threeCategoryCode { get; set; }

        public int? isBarCode { get; set; }

    }
}

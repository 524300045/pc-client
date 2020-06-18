using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PackageXiBeiRequest : IWMSRequest<PackageResponse>
    {
        public string GetAPIPath()
        {
           // return "/packageDetail/packageXiBei";
            return "/packageDetail/packageDetail";
        }


        [JsonProperty("packTaskDetailId")]
        public long packTaskDetailId { get; set; }

        [JsonProperty("weight")]
        public decimal weight { get; set; }

        [JsonProperty("processUser")]
        public string processUser { get; set; }

        [JsonProperty("packTaskCode")]
        public string packTaskCode { get; set; }

        [JsonProperty("outboundTaskCode")]
        public string outboundTaskCode { get; set; }

        [JsonProperty("skuCode")]
        public string skuCode { get; set; }

        [JsonProperty("boxCode")]
        public string boxCode { get; set; }

        [JsonProperty("createUser")]
        public string createUser { get; set; }

        [JsonProperty("updateUser")]
        public string updateUser { get; set; }

        [JsonProperty("partnerCode")]
        public String partnerCode { get; set; }

        [JsonProperty("partnerName")]
        public String partnerName { get; set; }


        [JsonProperty("goodsName")]
        public string goodsName { get; set; }
    }
}

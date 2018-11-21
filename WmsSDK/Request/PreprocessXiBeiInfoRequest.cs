using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PreprocessXiBeiInfoRequest : IWMSRequest<PreprocessInfoAddResponse>
    {
        public string GetAPIPath()
        {
            return "/preprocessInfo/xiBeibatchAdd";
        }

        public List<PreprocessInfoAdd> request { get; set; }

        [JsonProperty("warehouseId")]
        public string wareHouseId { get; set; }

         [JsonProperty("warehouseCode")]
        public String warehouseCode { get; set; }

         [JsonProperty("warehouseName")]
        public String warehouseName { get; set; }

         [JsonProperty("customerCode")]
        public String customerCode { get; set; }

         [JsonProperty("customerName")]
        public String customerName { get; set; }
    }

   

}

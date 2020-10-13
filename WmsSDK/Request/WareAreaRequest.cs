using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class WareAreaRequest : IWMSRequest<WareAreaResponse>
    {
        public string GetAPIPath()
        {
            return "/locations/getAreaList";
        }


            [JsonProperty("warehouseCode")]
        public String warehouseCode { get; set; }
    }
}

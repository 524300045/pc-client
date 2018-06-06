using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class WarehouseAreaRequest : IWMSRequest<WarehouseAreaResponse>
    {
        public string GetAPIPath()
        {
            return "/warehouseArea/getWarehouseArea";
        }


            [JsonProperty("warehouseCode")]
        public String warehouseCode { get; set; }
    }
}

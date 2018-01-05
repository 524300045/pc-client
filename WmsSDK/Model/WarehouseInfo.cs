using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WmsSDK.Model
{
    public  class WarehouseInfo
    {

        [JsonProperty("warehouseCode")]
        public string WarehouseCode {get;set;}


         [JsonProperty("warehouseName")]
        public string WarehouseName { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class StoreInfo
    {
        /** 门店编码 */
       [JsonProperty("storedCode")]
       public String storedCode { get; set; }
        /** 门店名称 */
       [JsonProperty("storedName")]
       public String storedName { get; set; }


       public int? priority { get; set; }
    }
}

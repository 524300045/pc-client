using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsFrozenRequest : IWMSRequest<GoodsFrozenResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/queryfrozengoodslist";
        }

   
          [JsonProperty("goodsName")]
          public String goodsName;


          [JsonProperty("customerCode")]
          public string customerCode { get; set; }

          public int? goodsType { get; set; }


       

    }
}

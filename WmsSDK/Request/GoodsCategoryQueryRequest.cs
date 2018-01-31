using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsCategoryQueryRequest : IWMSRequest<GoodsCategoryResponse>
    {
        [JsonProperty("level")]
        public String level { get; set; }


        public String parentCode { get; set; }



        public string GetAPIPath()
        {
            return "/goodsCategory/queryCategorylevel";
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class BomRelationRequest : IWMSRequest<BomRelationResponse>
    {
        public string GetAPIPath()
        {
            return "/boxInfo/getBomRelation";
        }

        /** 门店编码 */
        [JsonProperty("skuCode")]
        public String skuCode;

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class TurnBoxAddRequest : IWMSRequest<TurnInfoResponse>
    {
        public string GetAPIPath()
        {
            return "/wmsTurnoverBox/add";
        }

      //  [JsonProperty("request")]
        public List<WmsTurnBoxAdd> boxList { get; set; }
      
    }
}

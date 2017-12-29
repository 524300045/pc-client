using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class BoxInfoAddRequest : IWMSRequest<BoxInfoAddResponse>
    {
        public string GetAPIPath()
        {
            return "/boxInfo/add";
        }

      //  [JsonProperty("request")]
        public List<BoxInfoAdd> request { get; set; }
      
    }
}

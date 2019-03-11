using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
   public  class ImportOrderExeclRequest : IWMSRequest<ImportOrderExeclResponse>
    {
        public string GetAPIPath()
        {
            return "/importFile/importExecl";
        }

        //  [JsonProperty("request")]
        public List<ImportOperateInfo> request { get; set; }
    }
}

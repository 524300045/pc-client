using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class VersionInfoRequest : IWMSRequest<VersionResponse>
    {
        public string GetAPIPath()
        {
            return "/versionInfo/getVersionInfoByCode";
        }

        [JsonProperty("systemCode")]
        public String systemCode;
    }
}

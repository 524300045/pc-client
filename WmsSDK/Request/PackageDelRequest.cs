using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PackageDelRequest : IWMSRequest<PackageDelResponse>
    {
        public string GetAPIPath()
        {
            return "/packageDetail/deletePackage";
        }

             [JsonProperty("id")]
        public long id { get; set; }

             [JsonProperty("updateUser")]
        public string updateUser { get; set; }
    }
}

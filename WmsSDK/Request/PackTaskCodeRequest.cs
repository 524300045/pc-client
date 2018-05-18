using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PackTaskCodeRequest : IWMSRequest<PackTaskDetailResponse>
    {
        public string GetAPIPath()
        {
            return "/packTaskDetail/getPackTaskDetailByStatus";
        }

        [JsonProperty("packTaskCode")]
        public string packTaskCode { get; set; }

            [JsonProperty("customerCode")]
        public String customerCode { get; set; }

    }
}

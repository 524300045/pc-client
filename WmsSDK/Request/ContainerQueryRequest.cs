using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class ContainerQueryRequest : IWMSRequest<ContainerResponse>
    {
        public string GetAPIPath()
        {
            return "/container/getList";
        }

        [JsonProperty("containerCode")]
        public String containerCode;

        [JsonProperty("areaCode")]
        public String areaCode;


        [JsonProperty("status")]
        public int? status { get; set; }


        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

   

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PackageTaskDetailRequest : IWMSRequest<PackageTaskDetailResponse>
    {
        public string GetAPIPath()
        {
            return "/packageDetail/getPackageTaskTraceList";
        }


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

        [JsonProperty("skuCode")]
        public string skuCode { get; set; }


        [JsonProperty("startTime")]
        public string startTime { get; set; }

        [JsonProperty("endTime")]
        public string endTime { get; set; }

        [JsonProperty("packTaskCode")]
        public string packTaskCode { get; set; }


            [JsonProperty("partnerCode")]
        public String partnerCode { get; set; }

           [JsonProperty("customerCode")]
            public String customerCode { get; set; }

             [JsonProperty("warehouseCode")]
            public String warehouseCode { get; set; }
    }
}

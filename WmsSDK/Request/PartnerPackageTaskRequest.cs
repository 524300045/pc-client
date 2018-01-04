using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public  class PartnerPackageTaskRequest : IWMSRequest<PartnerPackageTaskResponse>
    {

        public string GetAPIPath()
        {
            return "/packTaskDetail/getPackTaskDetailPageList";
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


          [JsonProperty("status")]
        public int? status { get; set; }

          [JsonProperty("storedCode")]
          public string storedCode { get; set; }

          [JsonProperty("customerCode")]
          public string customerCode { get; set; }

          [JsonProperty("warehouseCode")]
          public string warehouseCode { get; set; }
    }
}

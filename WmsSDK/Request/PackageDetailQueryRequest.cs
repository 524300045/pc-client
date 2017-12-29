using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{

    /// <summary>
    /// 包装任务明细查询
    /// </summary>
    public class PackageDetailQueryRequest : IWMSRequest<PackageDetailQueryResponse>
    {
   
        public string GetAPIPath()
        {
            return "/packageDetail/getPackageList";
        }

        /** 商品编码 */
           [JsonProperty("skuCode")]
        public String skuCode { get; set; }

        /** 包裹号 */
           [JsonProperty("packageCode")]
        public String packageCode { get; set; }

        /** 门店编码 */
           [JsonProperty("storedCode")]
        public String storedCode { get; set; }

           [JsonProperty("status")]
        public int? status { get; set; }

           [JsonProperty("startTime")]
        public string startTime { get; set; }
        /** 结束时间 */
               [JsonProperty("endTime")]
        public string endTime { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数量
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }


          [JsonProperty("partnerCode")]
        public string partnerCode { get; set; }

           [JsonProperty("boxCode")]
          public String boxCode { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{


    public class InBoundDetailRequest : IWMSRequest<InBoundDetailResponse>
    {
        public string GetAPIPath()
        {
            return "/inBoundDetail/getFrozenInBoundDetailList";
        }


         [JsonProperty("goodsName")]
        public String goodsName { get; set; }

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


        public String orderNo { get; set; }

         [JsonProperty("warehouseCode")]
          public string warehouseCode { get; set; }

         [JsonProperty("customerCode")]
          public string customerCode { get; set; }


    

    }

}

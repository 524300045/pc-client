using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsQueryRequest : IWMSRequest<GoodsBarCodeResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/getGoodsBarCodeList";
        }

        [JsonProperty("skuCode")]
        public String skuCode;

        [JsonProperty("goodsName")]
        public String goodsName;


        [JsonProperty("isFresh")]
        public int? isFresh { get; set; }


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

        [JsonProperty("twoCategoryCode")]
        public String twoCategoryCode { get; set; }

        [JsonProperty("categoryCode")]
        public String categoryCode { get; set; }

        [JsonProperty("threeCategoryCode")]
        public String threeCategoryCode { get; set; }

        public int? isBarCode { get; set; }

    }
}

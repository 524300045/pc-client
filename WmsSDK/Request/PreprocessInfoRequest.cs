using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PreprocessInfoRequest : IWMSRequest<PreprocessInfoAddResponse>
    {
        public string GetAPIPath()
        {
            return "/preprocessInfo/batchAdd";
        }

        public List<PreprocessInfoAdd> request { get; set; }

        [JsonProperty("warehouseId")]
        public string wareHouseId { get; set; }

         [JsonProperty("warehouseCode")]
        public String warehouseCode { get; set; }

         [JsonProperty("warehouseName")]
        public String warehouseName { get; set; }

         [JsonProperty("customerCode")]
        public String customerCode { get; set; }

         [JsonProperty("customerName")]
        public String customerName { get; set; }
    }

    public class PreprocessInfoQueryRequest : IWMSRequest<PreprocessInfoResponse>
    {
        public string GetAPIPath()
        {
            return "/preprocessInfo/getList";
        }


        [JsonProperty("preprocessCode")]
        public string preprocessCode { get; set; }

        [JsonProperty("partnerCode")]
        public String partnerCode { get; set; }


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

          [JsonProperty("status")]
        public int? status { get; set; }


         [JsonProperty("warehouseCode")]
          public string warehouseCode { get; set; }

         [JsonProperty("customerCode")]
          public string customerCode { get; set; }

    }

}

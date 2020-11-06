using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class TurnOverBoxRequest : IWMSRequest<TurnOverBoxResponse>
    {
        public string GetAPIPath()
        {
            return "/wmsTurnoverBox/getWmsTurnoverBoxInfoPageList";
        }

        /** 门店编码 */
        [JsonProperty("storedCode")]
        public String storedCode;


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


   
          [JsonProperty("customerCode")]
          public string customerCode { get; set; }

         [JsonProperty("warehouseCode")]
          public string warehouseCode { get; set; }
    }
}

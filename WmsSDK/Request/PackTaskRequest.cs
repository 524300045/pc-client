using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PackTaskRequest : IWMSRequest<PackTaskResponse>
    {
        public string GetAPIPath()
        {
            return "/packTask/getPackTaskListTrace";
        }

        /** 供应商编码 */
           [JsonProperty("partnerCode")]
        public String partnerCode { get; set; }


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

        //[JsonProperty("deliveryDate")]
        //public DateTime? deliveryDate { get; set; }

        [JsonProperty("skuCode")]
        public string skuCode { get; set; }


        [JsonProperty("status")]
        public int? status { get; set; }

        [JsonProperty("startTime")]
        public string startTime { get; set; }

        [JsonProperty("endTime")]
        public string endTime { get; set; }

               [JsonProperty("packTaskCode")]
        public string packTaskCode { get; set; }

        /// <summary>
        /// 10订单加工 ，20预加工
        /// </summary>
             [JsonProperty("packTaskType")]
               public int? packTaskType { get; set; }

          [JsonProperty("customerCode")]
             public string customerCode { get; set; }

           [JsonProperty("warehouseCode")]
          public string warehouseCode;


           /*加工工序*/
           public int? processProductAttr { get; set; }

           /*生产车间*/
           public int? productWorkshopAttr { get; set; }
    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsRequest : IWMSRequest<GoodsResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/getGoodsListByPartner";
        }

          [JsonProperty("skuCode")]
        public String skuCode;

          [JsonProperty("goodsName")]
          public String goodsName;


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


          [JsonProperty("isPreprocess")]
          public int isPreprocess { get; set; }

         [JsonProperty("isFresh")]
          public int isFresh { get; set; }


        /// <summary>
        /// 供应商编码
        /// </summary>
          [JsonProperty("partnerCode")]
          public string partnerCode { get; set; }


          [JsonProperty("startTime")]
          public string startTime { get; set; }

          [JsonProperty("endTime")]
          public string endTime { get; set; }

          [JsonProperty("warehouseCode")]
          public string warehouseCode { get; set; }

          [JsonProperty("customerCode")]
          public string customerCode { get; set; }


          /*加工工序*/
          public int? processProductAttr { get; set; }

          /*生产车间*/
          public int? productWorkshopAttr { get; set; }

        //是否标准化加工
          public int? isStandardProcess { get; set; }
    }
}

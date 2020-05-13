using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class PackTask
    {

        [JsonProperty("id")]
        public long id { get; set; }


        [JsonProperty("packTaskCode")]
        public string PackTaskCode { get; set; }


        [JsonProperty("packTaskType")]
        public int PackTaskType { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("skuCode")]
        public string skuCode { get; set; }

        [JsonProperty("goodsName")]
        public string goodsName { get; set; }

        [JsonProperty("orderNum")]
        public int orderNum { get; set; }

        [JsonProperty("progress")]
        public int progress { get; set; }

        [JsonProperty("orderCount")]
        public decimal orderCount { get; set; }

        [JsonProperty("partnerCode")]
        public string partnerCode { get; set; }

        [JsonProperty("partnerName")]
        public string partnerName { get; set; }

        [JsonProperty("warehouseCode")]
        public string warehouseCode { get; set; }

        [JsonProperty("regionCode")]
        public string regionCode { get; set; }

        [JsonProperty("originOrderNo")]
        public string originOrderNo { get; set; }

        [JsonProperty("customerCode")]
        public string customerCode { get; set; }

        [JsonProperty("orderDate")]
        public DateTime? orderDate { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime? deliveryDate { get; set; }

        [JsonProperty("operateTime")]
        public DateTime? operateTime { get; set; }

        [JsonProperty("createTime")]
        public DateTime? createTime { get; set; }

        [JsonProperty("createUser")]
        public string createUser { get; set; }

        [JsonProperty("uddateTime")]
        public DateTime? uddateTime { get; set; }

        [JsonProperty("updateUser")]
        public string updateUser { get; set; }

        [JsonProperty("yn")]
        public int yn { get; set; }

         [JsonProperty("modelNum")]
        public decimal modelNum { get; set; }

         [JsonProperty("goodsUnit")]
        public String goodsUnit { get; set; }

         [JsonProperty("physicsUnit")]
        public String physicsUnit { get; set; }



         [JsonProperty("finishNum")]
         public int finishNum { get; set; }


         public string progressDes { get; set; }
        /// <summary>
        /// 标准报数
        /// </summary>
         public int StandNum { get; set; }


          //实际数量
           [JsonProperty("realWeight")]
         public decimal realWeight { get; set; }

        //包裹数量
         [JsonProperty("packageNum")]
         public int packageNum { get; set; }

          [JsonProperty("warehouseName")]
         public String warehouseName { get; set; }

          public String processProductAttrDesc { get; set; }

          public String productWorkshopAttrDesc { get; set; }

          public string groupName { get; set; }

          public String customerGoodsName { get; set; }


          [JsonProperty("waveName")]
          public string waveName { get; set; }


        public string statusdes
        {
            get
            {
                if (status==0)
                {
                    return "新建";
                }

                if (status == 10)
                {
                    return "包装中";
                }

                if (status == 15)
                {
                    return "已完成";
                }

                if (status == 20)
                {
                    return "关闭";
                }

                return "";
            }
        }
    }
}

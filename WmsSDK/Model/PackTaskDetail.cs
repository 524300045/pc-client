using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class PackTaskDetail
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("packTaskCode")]
        public string PackTaskCode { get; set; }

        [JsonProperty("packTaskDetailId")]
        public long packTaskDetailId { get; set; }

        [JsonProperty("skuCode")]
        public string skuCode { get; set; }

        [JsonProperty("goodsName")]
        public string goodsName { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("isFresh")]
        public int isFresh { get; set; }

        [JsonProperty("weighed")]
        public decimal weighed { get; set; }

        [JsonProperty("storedCode")]
        public string storedCode { get; set; }

        [JsonProperty("storedName")]
        public string storedName { get; set; }

        [JsonProperty("modelNum")]
        public decimal modelNum { get; set; }

        [JsonProperty("goodsUnit")]
        public string goodsUnit { get; set; }

        [JsonProperty("physicsUnit")]
        public string physicsUnit { get; set; }

        [JsonProperty("planNum")]
        public decimal planNum { get; set; }

        [JsonProperty("upPlanNum")]
        public decimal upPlanNum { get; set; }

        [JsonProperty("downPlanNum")]
        public decimal downPlanNum { get; set; }

        [JsonProperty("operateTime")]
        public DateTime operateTime { get; set; }

        [JsonProperty("createTime")]
        public DateTime createTime { get; set; }

        [JsonProperty("createUser")]
        public string createUser { get; set; }

        [JsonProperty("updateTime")]
        public DateTime updateTime { get; set; }

        [JsonProperty("updateUser")]
        public string updateUser { get; set; }


          [JsonProperty("progress")]
        public int progress { get; set; }

             [JsonProperty("finishNum")]
          public int finishNum { get; set; }

          [JsonProperty("outboundTaskCode")]
          public string outboundTaskCode { get; set; }

           [JsonProperty("packageWeight")]
          public decimal packageWeight { get; set; }

           [JsonProperty("twoCategoryName")]
           public String twoCategoryName { get; set; }

         [JsonProperty("twoCategoryCode")]
         public String twoCategoryCode { get; set; }

         /** 一级分类编码 */
         [JsonProperty("categoryCode")]
         public String categoryCode { get; set; }
         /** 一级分类名称 */

          [JsonProperty("categoryName")]
         public String categoryName { get; set; }

         [JsonProperty("expiryDate")]
          public Double expiryDate { get; set; }

          public string StatusDes {

              get
              {
                  if (status==0)
                  {
                      return "新建";
                  }
                  if (status==10)
                  {
                      return "包装中";
                  }
                  if (status==15)
                  {
                      return "已完成";
                  }
                  if (status==20)
                  {
                      return "关闭";
                  }
                  return "";
              }
          }
    }
}

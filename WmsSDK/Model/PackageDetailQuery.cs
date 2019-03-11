using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Response
{
    public class PackageDetailQuery
    {

        /**序号*/
        [JsonProperty("id")]
        public long id { get; set; }

        /** 门店名称*/
        [JsonProperty("storedName")]
        public String storedName { get; set; }

        /** 包裹号 */
        [JsonProperty("packageCode")]
        public String packageCode { get; set; }

        /** 包裹状态 */
        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("skuCode")]
        public String skuCode { get; set; }

        [JsonProperty("goodsName")]
        public String goodsName { get; set; }

        /** 包规（数量） */
        [JsonProperty("modelNum")]
        public decimal modelNum { get; set; }

        /** 计价单位（斤、两） */
        [JsonProperty("goodsUnit")]
        public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
        [JsonProperty("physicsUnit")]
        public String physicsUnit { get; set; }

        /** 创建时间 */
        [JsonProperty("createTime")]
        public DateTime createTime { get; set; }
        /** 创建人 */
        [JsonProperty("createUser")]
        public String createUser { get; set; }

        /** 更新时间 */
        [JsonProperty("updateTime")]
        public DateTime updateTime { get; set; }
        /** 更新人 */
        [JsonProperty("updateUser")]
        public String updateUser { get; set; }

        [JsonProperty("outboundTaskCode")]
        public String outboundTaskCode { get; set; }

        /** 门店编码 */
        [JsonProperty("storedCode")]
        public String storedCode { get; set; }

        /** 创建时间 */
        [JsonProperty("deliveryDate")]
        public DateTime deliveryDate { get; set; }

        [JsonProperty("weight")]
        public decimal weight { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        [JsonProperty("boxCode")]
        public string boxCode { get; set; }


          [JsonProperty("warehouseName")]
        public string warehouseName { get; set; }

         [JsonProperty("twoCategoryCode")]
          public String twoCategoryCode { get; set; }

          [JsonProperty("categoryCode")]
         public string categoryCode { get; set; }

        public string statusDes
        {
            get
            {
                if (status == 0)
                {
                    return "新建";
                }
                if (status == 5)
                {
                    return "已包装";
                }

                if (status == 10)
                {
                    return "已分拣";
                }
                if (status == 20)
                {
                    return "已发运";
                }
                if (status == 90)
                {
                    return "作废";
                }
                if (status==30)
                {
                     return "关闭";
                }
                return "";
            }

        }
    }
}

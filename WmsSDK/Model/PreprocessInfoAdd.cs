using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class PreprocessInfoAdd
    {

        /** id */
        [JsonProperty("id")]
        public long id { get; set; }
        /** 预加工单号(小包裹号) */
        [JsonProperty("preprocessCode")]
        public String preprocessCode { get; set; }
        /** 状态 */
        [JsonProperty("status")]
        public int status { get; set; }
        /** 供应商编码 */
        [JsonProperty("partnerCode")]
        public String partnerCode { get; set; }
        /** 供应商名称 */
        [JsonProperty("partnerName")]
        public String partnerName { get; set; }
        /** 商品编码 */
        [JsonProperty("skuCode")]
        public String skuCode { get; set; }
        /** 商品名称 */
        [JsonProperty("goodsName")]
        public String goodsName { get; set; }
        /** 打包量 */
        [JsonProperty("packWeight")]
        public decimal packWeight { get; set; }
        /** 包规（数量） */
        [JsonProperty("modelNum")]
        public decimal modelNum { get; set; }
        /** 计价单位（斤、两） */
        [JsonProperty("goodsUnit")]
        public String goodsUnit { get; set; }
        /** 物理单位（包、箱、瓶） */
        [JsonProperty("physicsUnit")]
        public String physicsUnit { get; set; }
        /** 创建人 */
        [JsonProperty("operateUser")]
        public String operateUser { get; set; }


        /** 创建人 */
        [JsonProperty("createUser")]
        public String createUser { get; set; }

        /** 更新人 */
        [JsonProperty("updateUser")]
        public String updateUser { get; set; }
        /** 是否有效 */
        [JsonProperty("yn")]
        public int yn { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public class TurnOverBox
    {
        /** id */
        [JsonProperty("id")]
        public long id { get; set; }
        /** 客户编码 */
        [JsonProperty("customerCode")]
        public String customerCode { get; set; }
        /** 客户名称 */
        [JsonProperty("customerName")]
        public String customerName { get; set; }
        /** 仓库编码 */
        [JsonProperty("warehouseCode")]
        public String warehouseCode { get; set; }
        /** 仓库名称 */
        [JsonProperty("warehouseName")]
        public String warehouseName { get; set; }
        /** 门店编码 */
        [JsonProperty("storedCode")]
        public String storedCode { get; set; }
        /** 门店名称 */
        [JsonProperty("storedName")]
        public String storedName { get; set; }
        /** 箱号 */
        [JsonProperty("turnBoxCode")]
        public String turnBoxCode { get; set; }
        /** 打印时间 */
        [JsonProperty("printTime")]
        public string printTime { get; set; }
        /** 打印人 */
        [JsonProperty("printMan")]
        public String printMan { get; set; }
     
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class VersionInfo
    {
        /** 主键id */
          [JsonProperty("id")]
        public long id { get; set; }
        /** 系统编码 */
            [JsonProperty("systemCode")]
        public String systemCode { get; set; }
        /**系统名称 */
            [JsonProperty("systemName")]
        public String systemName { get; set; }
        /** 版本号*/
            [JsonProperty("versionCode")]
        public String versionCode { get; set; }

            [JsonProperty("url")]
        public String url { get; set; }

            [JsonProperty("remark")]
        public String remark { get; set; }
    
    }
}

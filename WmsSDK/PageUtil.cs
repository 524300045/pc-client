using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK
{
    public class PageUtil
    {
        [JsonProperty("curPage")]
        public int curPage { get; set; }

        [JsonProperty("totalRow")]
        public int totalRow { get; set; }

        [JsonProperty("pageSize")]
        public int pageSize { get; set; }

        [JsonProperty("totalPage")]
        public int totalPage { get; set; }
    }
}

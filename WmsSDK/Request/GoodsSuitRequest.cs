using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsSuitRequest : IWMSRequest<GoodsSuitResponse>
    {
        public string GetAPIPath()
        {
            return "/goodsSuit/getGoodsSuitList";
        }

        public String skuCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class DictRequest : IWMSRequest<DictResponse>
    {
        public string GetAPIPath()
        {
            return "/dict/getDictByType";
        }

        public string type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Response
{
    public class OutBoundSendResponse : WMSResponse
    {
        public string result { get; set; }
    }

    public class OutBoundCheckResponse : WMSResponse
    {
        public string result { get; set; }
    }
}

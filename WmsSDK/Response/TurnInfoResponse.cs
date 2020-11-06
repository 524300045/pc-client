using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class TurnInfoResponse : WMSResponse
    {
        public List<TurnOverBox> result { get; set; }

        public PageUtil pageUtil { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class TurnOverBoxResponse : WMSResponse
    {
        public List<TurnOverBox> result { get; set; }

        public PageUtil pageUtil { get; set; }
    }
}

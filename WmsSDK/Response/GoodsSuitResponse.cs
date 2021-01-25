using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class GoodsSuitResponse : WMSResponse
    {
        public List<GoodsSuitModel> result { get; set; }

     
    }
}

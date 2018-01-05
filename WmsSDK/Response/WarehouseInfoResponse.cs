using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class WarehouseInfoResponse : WMSResponse
    {
        public List<WarehouseInfo> result { get; set; }
    }
}

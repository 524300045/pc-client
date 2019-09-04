using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class WarehouseWorkGroupResponse : WMSResponse
    {
        public List<WarehouseWorkGroup> result { get; set; }
    }
}

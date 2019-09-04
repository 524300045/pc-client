using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class WarehouseWorkGroupRequest : IWMSRequest<WarehouseWorkGroupResponse>
    {
        public string GetAPIPath()
        {
            return "/warehouseWorkGroup/getAllWorkGroup";
        }

        public String warehouseCode { get; set; }
    }
}

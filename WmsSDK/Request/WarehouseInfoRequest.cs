using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class WarehouseInfoRequest : IWMSRequest<WarehouseInfoResponse>
    {
        public string GetAPIPath()
        {
            return "/warehouseInfo/getAllWarehouse";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
   public   class WarehouseAreaResponse: WMSResponse
    {
       public List<WarehouseArea> result { get; set; }
    }
}

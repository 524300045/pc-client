using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class CustomerPrintConfigRequest : IWMSRequest<CustomerPrintConfigResponse>
    {
        public string GetAPIPath()
        {
            return "/customerPrintConfig/getAllCustomerPrintConfig";
        }
    }
}

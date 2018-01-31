using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class CustomerGoodsBySkuRequest : IWMSRequest<CustomerGoodsBySkuResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/getCustomerGoodsBySkuCode";
        }

        public string customerCode { get; set; }

        public string skuCode { get; set; }
    }
}

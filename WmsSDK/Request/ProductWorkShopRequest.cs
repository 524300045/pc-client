using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class ProductWorkShopRequest : IWMSRequest<ProductWorkShopResponse>
    {
        public string GetAPIPath()
        {
            return "/goods/getProductWorkshopAttr";
        }
    }
}

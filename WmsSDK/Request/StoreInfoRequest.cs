using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class StoreInfoRequest : IWMSRequest<StoreInfoResponse>
    {
        public string GetAPIPath()
        {
            return "/storeInfo/getStoreInfoByPartnerCode";
        }

        public string partnerCode { get; set; }

        public string customerCode { get; set; }

        public string warehouseCode { get; set; }
    }
}

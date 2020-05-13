using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class WaveCustomerStoreRequest : IWMSRequest<WaveCustomerStoreResponse>
    {
        public string GetAPIPath()
        {
            return "/waveCustomerStore/getCustomerWave";
        }

        public string warehouseCode { get; set; }

        public string customerCode { get; set; }
    }
}

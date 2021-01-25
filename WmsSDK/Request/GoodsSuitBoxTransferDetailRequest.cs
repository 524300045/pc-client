using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class GoodsSuitBoxTransferDetailRequest : IWMSRequest<GoodsSuitBoxTransferDetailResponse>
    {
        public string GetAPIPath()
        {
            return "/goodsSuitBoxTransfer/addyangchu";
        }

        public List<GoodsSuitBoxDetailResponse> goodsSuitBoxList { get; set; }


    }
}

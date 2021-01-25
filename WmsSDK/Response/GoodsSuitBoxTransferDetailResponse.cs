using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class GoodsSuitBoxTransferDetailResponse : WMSResponse
    {
        public GoodsSuitBox result { get; set; }

     
    }


    public class GoodsSuitBoxTransferDetailQueryResponse : WMSResponse
    {
        public List<GoodsSuitBoxTransferDetailModel> result { get; set; }


    }
}

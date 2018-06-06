using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class PartnerRequest : IWMSRequest<PartnerResponse>
    {
       public String partnerCode { get; set; }

         public String customerCode { get; set; }


         public String warehouseCode { get; set; }

       public string GetAPIPath()
       {
           return "/partner/getPartnerInfo";
       }
    }
}

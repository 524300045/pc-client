using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
   public  class OutBoundResponse
    {
    }

   public class OutBoundPrintPageResponse : WMSResponse
   {

       public List<ShipmentModel> result { get; set; }

       public PageUtil pageUtil { get; set; }
   }


   public class OutBoundPageResponse : WMSResponse
   {

       public List<ShipmentModel> result { get; set; }

       public PageUtil pageUtil { get; set; }
   }

   public class OutBoundPrintDetailResponse : WMSResponse
   {
     //  public List<ShipMentDetailVo> result { get; set; }

       public OutBoundPrintModel result { get; set; }
   }
}

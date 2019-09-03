using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
   public  class BomRelationResponse : WMSResponse
    {
       public List<BomRelationModel> result { get; set; }
    }
}

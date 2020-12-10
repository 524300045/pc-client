using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class InBoundDetailResponse : WMSResponse
    {
       public List<InBoundDetailModel> result;

       public PageUtil pageUtil { get; set; }
    }

   


}

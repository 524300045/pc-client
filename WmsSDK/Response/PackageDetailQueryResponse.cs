using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Response
{
   public  class PackageDetailQueryResponse:WMSResponse
    {
       public List<PackageDetailQuery> result { get; set; }

       public PageUtil pageUtil { get; set; }
    }
}

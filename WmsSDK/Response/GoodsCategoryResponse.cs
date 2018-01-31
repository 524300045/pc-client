using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
   public   class GoodsCategoryResponse : WMSResponse
    {
       public List<GoodsCategory> result { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
   public  class PreprocessInfoResponse:WMSResponse
    {
       public List<PreprocessInfo> result;

       public PageUtil pageUtil { get; set; }
    }

   public class PreprocessInfoAddResponse : WMSResponse
   {

       public List<PreprocessInfo> result;
   }



   public class PreprocessInfoCountResponse : WMSResponse
   {
       public int result;
   }


}

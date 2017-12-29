using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
   public   class LoginRequest
    {
       public string name { get; set; }

       public string password { get; set; }
    }
}

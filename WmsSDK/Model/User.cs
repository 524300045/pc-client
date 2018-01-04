using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public   class User
    {

       public long id { get; set; }

       public string name { get; set; }

       public string cnName { get; set; }

       public string email { get; set; }

       public string companyName { get; set; }

       public string companyCode { get; set; }

       public List<Menu> menuDtos { get; set; }


      // public string customerMap { get; set; }

       public Dictionary<string,string> customerMap {get;set;}

       public string defaultCustomerCode {get;set;}
    }
}

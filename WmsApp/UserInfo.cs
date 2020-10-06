using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsApp
{
   public   class UserInfo
    {
       public static string UserName;

       public static string RealName;

       public static string PartnerCode;

       public static String PartnerName;

       public static String WareHouseCode;

       public static string WareHouseName;

       public static string CompanyName;


       public static string id;

       public static string cnName;

       public static string CustomerCode;

       public static string CustomerName;


       public static string labelName;

       public static string foodLicenseNo;

       /// <summary>
       /// 区域名称
       /// </summary>
       public static String areaName;


       public static string phone;

       public static string address;

       public static List<Menu> menuDtos { get; set; }
    }
}

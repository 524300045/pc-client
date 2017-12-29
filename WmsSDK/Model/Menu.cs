using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class Menu
    {
        public string menuCode { get; set; }

        public string menuName { get; set; }

        public string menuIcon { get; set; }

        public List<SubMenu> subMenus;
    }
}

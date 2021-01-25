using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
   public  class GoodsSuitModel
    {

       public long id { get; set; }
        /** 主编码 */
        public String skuCode { get; set; }

        public String goodsName { get; set; }


        public String goodsUnit { get; set; }


        /** 内部SKU编码 */
        public String childSkuCode { get; set; }

        public String childGoodsName { get; set; }

        public String childGoodsUnit { get; set; }

        public int seq { get; set; }
    
    }
}

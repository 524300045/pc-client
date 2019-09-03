using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class BomRelationModel
    {
        public String skuCode { get; set; }
        /** 商品名称 */
        public String goodsName { get; set; }
        public String goodsUnit { get; set; }

        public String materialSkuCode { get; set; }
        public String materialGoodsName { get; set; }
        public String materialGoodsUnit { get; set; }
        public Decimal proportion { get; set; }
    
    }
}

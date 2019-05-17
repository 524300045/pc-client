using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK.Model
{
    public  class ShipMentDetailVo
    {

        public int row { get; set; }

        public String categoryName { get; set; }

        public String twoCategoryName { get; set; }

        public String goodsModel { get; set; }

        public String goodsName { get; set; }

        public decimal taxCount { get; set; }

        public decimal taxPrice { get; set; }

        public decimal deliveryNum { get; set; }


        public decimal planNum { get; set; }

        public decimal modelNum { get; set; }

        public decimal? modelWeight { get; set; }
        /**
         * 单行小计
         */
        public decimal subTotalPrice { get; set; }

        public String physicsUnit { get; set; }
        /** 计价单位（斤、两） */
        public String goodsUnit { get; set; }

        public OutBoundModel outBound;

        public decimal taxNoPrice { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class CustomerGoodsBySkuResponse : WMSResponse
    {
        public CustomerGoods result { get; set; }
    }
}

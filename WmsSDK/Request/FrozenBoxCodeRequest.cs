﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Response;

namespace WmsSDK.Request
{
    public class FrozenBoxCodeRequest : IWMSRequest<FrozenBoxCodeResponse>
    {
        public string GetAPIPath()
        {
            return "/wmsFrozenboxcode/createCode";
        }

        public String warehouseCode { get; set; }
        /**  */
        public String warehouseName { get; set; }
        /**  */
        public String customerCode { get; set; }
        /**  */
        public String customerName { get; set; }

        /**  */
        public String skuCode { get; set; }

        /** 数量 */
        public int num { get; set; }

        public String createUser { get; set; }


        public String productionDate { get; set; }


       
    }
}
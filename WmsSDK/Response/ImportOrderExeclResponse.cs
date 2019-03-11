using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class ImportOrderExeclResponse : WMSResponse
    {
        public List<ImportOperateInfo> result { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WmsSDK.Model;

namespace WmsSDK.Response
{
    public class PackTaskResponse : WMSResponse
    {

        public List<PackTask> result { get; set; }

        public PageUtil pageUtil { get; set; }
    }


    public class PackageTaskDetailResponse : WMSResponse
    {

        public List<PackagePackTask> result { get; set; }

        public PageUtil pageUtil { get; set; }
    }
}

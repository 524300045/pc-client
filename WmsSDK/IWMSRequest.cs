using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK
{
    public interface IWMSRequest<in T> where T : WMSResponse
    {
        /// <summary>
        /// 获取TOP的API路径。
        /// </summary>
        /// <returns>API名称</returns>
        string GetAPIPath();
    }

}

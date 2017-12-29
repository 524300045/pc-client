using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmsSDK
{
    public interface IWMSClient
    {
        T Execute<T>(IWMSRequest<T> request) where T : WMSResponse;
        T Execute<T>(string url, IWMSRequest<T> request) where T : WMSResponse;
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WmsSDK
{
    public class NetworkMonitor
    {
        private PerformanceCounter _bytesSent;
        private PerformanceCounter _bytesReceived;
        private readonly int _processId;
        private bool _initialized;
        private bool _notSupport = false;
        public float LastBytesSent
        {
            get; internal set;
        }
        public float LastBytesReceived
        {
            get; internal set;
        }

        private float totalBytes = 0;
        private long totalCount = 0;

        public bool NotSupport
        {
            get
            {
                return _notSupport;
            }
            //set
            //{
            //    _notSupport = value;
            //}
        }

        public NetworkMonitor(int processID)
        {
            _processId = processID;
            Initialize();
        }

        public NetworkMonitor()
            : this(Process.GetCurrentProcess().Id)
        {

        }
        private void Initialize()
        {
            try
            {
                if (_initialized)
                    return;

                var category = new PerformanceCounterCategory(".NET CLR Networking 4.0.0.0");
                var instanceNames = category.GetInstanceNames().Where(i => i.Contains(string.Format("p{0}", _processId)));
                if (!instanceNames.Any()) return;

                _bytesSent = new PerformanceCounter
                {
                    CategoryName = ".NET CLR Networking 4.0.0.0",
                    CounterName = "Bytes Sent",
                    InstanceName = instanceNames.First(),
                    ReadOnly = true
                };

                _bytesReceived = new PerformanceCounter
                {
                    CategoryName = ".NET CLR Networking 4.0.0.0",
                    CounterName = "Bytes Received",
                    InstanceName = instanceNames.First(),
                    ReadOnly = true
                };

                _initialized = true;
            }
            catch
            {
                //NotSupport = true;
            }
        }

        public float GetSentBytes()
        {
            totalCount++;
            if (NotSupport)
            {
                return 0;
            }
            Initialize(); //in Net4.0 performance counter will get activated after first request
            float rawValue = _initialized ? _bytesSent.RawValue : 0;
            totalBytes += rawValue;
            CheckSupported();
            return rawValue;
        }

        public float GetReceivedBytes()
        {
            totalCount++;
            if (NotSupport)
            {
                return 0;
            }
            Initialize(); //in Net4.0 performance counter will get activated after first request
            float rawValue = _initialized ? _bytesReceived.RawValue : 0;
            totalBytes += rawValue;
            CheckSupported();
            return rawValue;
        }

        private void CheckSupported()
        {
            if (totalCount > 5 && totalBytes < 1)
            {
                //NotSupport = true;
            }
        }
    }
}

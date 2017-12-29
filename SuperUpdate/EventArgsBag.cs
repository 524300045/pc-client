using System;
using System.ComponentModel;

namespace WmsSDK
{
    public class APIExecutedEventArgs : EventArgs
    {
        public APIExecuteStatistics Data { get; internal set; }
    }

    /// <summary>
    /// API执行统计信息
    /// </summary>
    [Description("API执行统计信息")]
    public class APIExecuteStatistics
    {
        private static volatile int _id = 0;
        public APIExecuteStatistics()
        {
            if (_id > int.MaxValue - 10)
            {
                _id = 0;
            }
            _id++;
            ID = _id;
        }
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public int ID { get; private set; }
        /// <summary>
        /// API服务地址
        /// </summary>
        [Description("API服务地址")]
        public string ServiceAddress { get; internal set; }
        /// <summary>
        /// API地址
        /// </summary>
        [Description("API地址")]
        public string APIPath { get; internal set; }
        /// <summary>
        /// 完整路径
        /// </summary>
        [Description("完整路径")]
        public string FullURL { get; internal set; }
        /// <summary>
        /// 错误码
        /// </summary>
        [Description("错误码")]
        public string Code { get; internal set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        [Description("请求内容")]
        public string RequestContent { get; internal set; }
        /// <summary>
        /// 上行流量
        /// </summary>
        [Description("上行流量")]
        public float BytesSent { get; internal set; }
        /// <summary>
        /// 上行流量友好文本
        /// </summary>
        [Description("上行流量友好文本")]
        public string BytesSentText
        {
            get
            {
                return BytesToSizeText(BytesSent);
            }
        }
        /// <summary>
        /// 请求类型
        /// </summary>
        [Description("请求类型")]
        public Type RequestType { get; internal set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        [Description("响应内容")]
        public string ResponseContent { get; internal set; }
        /// <summary>
        /// 上行流量
        /// </summary>
        [Description("下行流量")]
        public float BytesReceived { get; internal set; }
        /// <summary>
        /// 下行流量友好文本
        /// </summary>
        [Description("下行流量友好文本")]
        public string BytesReceivedText
        {
            get
            {
                return BytesToSizeText(BytesReceived);
            }
        }
        /// <summary>
        /// 响应类型
        /// </summary>
        [Description("响应类型")]
        public Type ResponseType { get; internal set; }
        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        [Description("耗时(毫秒)")]
        public long ElapseTime { get; internal set; }
        /// <summary>
        /// 耗时文本
        /// </summary>
        [Description("耗时文本")]
        public string ElapseTimeText
        {
            get
            {
                return TimeString(ElapseTime);
            }
        }
        /// <summary>
        /// 网络耗时
        /// </summary>
        [Description("网络耗时")]
        public long NetworkElapseTime { get; internal set; }
        /// <summary>
        /// 网络耗时文本
        /// </summary>
        [Description("网络耗时文本")]
        public string NetworkElapseTimeText
        {
            get
            {
                return TimeString(NetworkElapseTime);
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        public DateTime StartTime { get; internal set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        public DateTime EndTime { get; internal set; }
        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        public Exception Exception { get; internal set; }

        private string[] SizeUnits = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// 字节转为友好文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string BytesToSizeText(float value)
        {
            if (value == 0)
                return "0B";
            var k = 1024;
            var i = Math.Floor(Math.Log(value) / Math.Log(k));
            return ((value / Math.Pow(k, i))).ToString("F2") + SizeUnits[(int)i];
        }

        /// <summary>
        /// 毫秒转成友好文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string TimeString(long value)
        {
            if (value < 1000)
            {
                return string.Format("{0}毫秒", value);
            }
            else
            {
                long second = value / 1000;
                value = value % 1000;
                if (second < 60)
                {
                    return string.Format("{0}秒{1}毫秒", second, value);
                }
                else
                {
                    long minute = second / 60;
                    second = second % 60;
                    return string.Format("{0}分{1}秒{2}毫秒", minute, second, value);
                }
            }
        }
    }
}

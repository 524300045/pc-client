using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WmsSDK
{
    public abstract class WMSResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 调用的URL
        /// </summary>
        public string RequestURL { get; set; }

        /// <summary>
        /// 请求的Json参数
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError
        {
            get
            {
                return this.Code !="200";
               // return !string.IsNullOrWhiteSpace(this.Code);
            }
        }
    }

    public class ErrorWMSResponse : WMSResponse
    {

    }

    public class ErrorCodeMessage
    {
        public static string ServiceAddressNotFound { get { return "未提供服务器地址"; } }
        public static string ServiceAddressNotFoundMessage
        {
            get
            {
                return "系统未找到服务地址，请联系您的系统管理员。";
            }
        }
        public static string EmptyResponse { get { return "服务返回空响应"; } }
        public static string EmptyResponseMessage
        {
            get
            {
                return "服务返回空响应，无法处理。";
            }
        }
    }
}

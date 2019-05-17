using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections.Concurrent;
using Newtonsoft.Json.Serialization;

namespace WmsSDK
{
    /// <summary>
    /// WMS服务代理客户端
    /// </summary>
    public class DefalutWMSClient : IWMSClient
    {
        static DefalutWMSClient()
        {
            //try
            //{
            //    UserAgentString = "WmsSDK4NET." + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //}
            //catch
            //{
            //    UserAgentString = "WmsSDK4NET";
            //}

            #region 识别终端信息
            /*
终端ID：terminalId
仓库ID：warehouseId
仓库编码：warehouseCode
用户ID：currentUserId
用户姓名：currentUserName
IP地址：statistics-IP
终端名称：statistics-TerminalName
终端类型：statistics-TerminalType
终端操作系统名称：statistics-SystemName
终端操作系统版本：statistics-SystemVersion
终端累计发送流量(字节)：statistics-BytesSent
终端累计接收流量(字节)：statistics-BytesReceived
终端上次发送流量(字节)：statistics-LastBytesSent
终端上次接收流量(字节)：statistics-LastBytesReceived
            */
            Properties.Clear();
            //Properties.Add("idc-id", "0");
            //Properties.Add("terminalId", terminalID);
            //try
            //{
            //    Properties.Add("terminalVersion", Assembly.GetEntryAssembly().GetName().Version.ToString());
            //}
            //catch
            //{
            //    Properties.Add("terminalVersion", "<异常>");
            //}
            //Properties.Add("deviceId", ComputerHelper.GetCPUID());
            //Properties.Add("warehouseId", "0");
            //Properties.Add("warehouseCode", "");
            //Properties.Add("warehouseName", "");
            //Properties.Add("currentUserId", "-1");
            //Properties.Add("currentUserName", "");
            //Properties.Add("statistics-IP", ComputerHelper.GetIPAddress());
            string machineName = Environment.MachineName;
            try
            {
                machineName = Dns.GetHostName();
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }
            }
            catch
            {
                machineName = Environment.MachineName;
            }
            //Properties.Add("statistics-TerminalName", machineName);
            //Properties.Add("statistics-TerminalType", "PC");
            //Properties.Add("statistics-SystemName", ComputerHelper.GetOSName());
            //Properties.Add("statistics-SystemVersion", Environment.OSVersion.Version.ToString());
            //Properties.Add("statistics-LastBytesSent", NetworkMonitor.LastBytesSent.ToString("F0"));
            //Properties.Add("statistics-LastBytesReceived", NetworkMonitor.LastBytesReceived.ToString("F0"));
            //Properties.Add("statistics-BytesSent", NetworkMonitor.GetSentBytes().ToString("F0"));
            //Properties.Add("statistics-BytesReceived", NetworkMonitor.GetReceivedBytes().ToString("F0"));
            //Properties.Add("Accept-Encoding", "gzip");

            #endregion
        }

        private static string _defaultServiceAddress = "";

        private static int _timeout = 1800000;
        private static int lastMinute = -1;

        private static volatile float SentBytes;
        private static volatile float ReceivedBytes;
        private static volatile float LastSentBytes;
        private static volatile float LastReceivedBytes;
        private static object _bytesLocker = new object();

        private static bool SupportGZip = false;


        private static string terminalID = Guid.NewGuid().ToString("N");

        public static string UserAgentString { get; private set; }

        private static Dictionary<string, string> _properties = new Dictionary<string, string>();

        /// <summary>
        /// API执行后触发事件
        /// </summary>
        public static event EventHandler<APIExecutedEventArgs> APIExecuted;
        /// <summary>
        /// 触发APIExecuted事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAPIExecuted(APIExecutedEventArgs e)
        {
            if (APIExecuted != null)
            {
                APIExecuted(this, e);
            }
        }

        public static Dictionary<string, string> Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                if (value == null)
                {
                    _properties.Clear();
                }
                else
                {
                    _properties = value;
                }
            }
        }

        public static string DefaultServiceAddress
        {
            get
            {
                return _defaultServiceAddress;
            }

            set
            {
                _defaultServiceAddress = value;
            }
        }

        private static NetworkMonitor netMonitor = new NetworkMonitor();

        public static NetworkMonitor NetworkMonitor
        {
            get
            {
                return netMonitor;
            }
            private set
            {
                netMonitor = value;
            }
        }

        /// <summary>
        /// 调用默认服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(IWMSRequest<T> request) where T : WMSResponse
        {
            return Execute(DefaultServiceAddress, request);
        }

        /// <summary>
        /// 调用指定服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(string url, IWMSRequest<T> request) where T : WMSResponse
        {
            return PrivateExecute(url, request);
        }

        internal T PrivateExecute<T>(IWMSRequest<T> request, Dictionary<string, string> customHeaders = null) where T : WMSResponse
        {
            return PrivateExecute(DefaultServiceAddress, request, customHeaders);
        }

        private T PrivateExecute<T>(string url, IWMSRequest<T> request, Dictionary<string, string> customHeaders = null) where T : WMSResponse
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                T response = ToObject<T>(GetJson(new ErrorWMSResponse { Code = ErrorCodeMessage.ServiceAddressNotFound, Message = ErrorCodeMessage.ServiceAddressNotFoundMessage }));
                return response;
            }
            APIExecuteStatistics data = new APIExecuteStatistics();
            data.StartTime = DateTime.Now/*仅用于统计耗时，可以使用DateTime.Now*/;
            data.ServiceAddress = url;
            data.APIPath = request.GetAPIPath();
            url = url.TrimEnd('/') + "/" + request.GetAPIPath().TrimStart('/');
            data.FullURL = url;
            data.RequestType = request == null ? null : request.GetType();
            data.ResponseType = typeof(T);
            T resp = null;
            try
            {
                if (customHeaders == null)
                {
                    customHeaders = new Dictionary<string, string>();
                }
                foreach (var item in Properties)
                {
                    customHeaders.Add(item.Key, item.Value);
                }
                int currentMinute = DateTime.Now.Minute/*仅记录当前分钟数，可以使用DateTime.Now*/;
                if (lastMinute != currentMinute)
                {
                    //lastMinute = currentMinute;
                    //customHeaders["statistics-IP"] = ComputerHelper.GetIPAddress();
                }
                //lock (_bytesLocker)
                //{
                //    customHeaders["statistics-LastBytesSent"] = LastSentBytes.ToString("F0");
                //    customHeaders["statistics-LastBytesReceived"] = LastReceivedBytes.ToString("F0");
                //    LastSentBytes = LastReceivedBytes = 0;
                //}
                //customHeaders["statistics-BytesSent"] = SentBytes.ToString("F0");
                //customHeaders["statistics-BytesReceived"] = ReceivedBytes.ToString("F0");
                string json = GetJson(request);
                data.RequestContent = json;
                string response = PostData(url, json, ref data, customHeaders);
                data.ResponseContent = response;
                if (string.IsNullOrEmpty(response) || response.Trim().Length < 1)
                {
                    resp = ToObject<T>(GetJson(new ErrorWMSResponse { Code = ErrorCodeMessage.EmptyResponse, Message = ErrorCodeMessage.EmptyResponseMessage }));
                }
                else
                {
                    resp = ToObject<T>(response);
                }
                resp.Body = response;
                resp.RequestJson = json;
                data.EndTime = DateTime.Now/*仅用于统计耗时，可以使用DateTime.Now*/;
                data.ElapseTime = (long)(data.EndTime - data.StartTime).TotalMilliseconds;
                data.Code = resp.Code;
            }
            catch (Exception ex)
            {
                data.Exception = ex;
                throw ex;
            }
            finally
            {
                OnAPIExecuted(new APIExecutedEventArgs { Data = data });
            }
            return resp;
        }

        /// <summary>
        /// Post方式提交数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostData(string url, string json, ref APIExecuteStatistics data, Dictionary<string, string> customHeaders)
        {
            string result = "";
            data.BytesSent = data.BytesReceived = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                string strURL = url;
                HttpWebRequest request = GetWebRequest(url, "POST");
                request.Timeout = 300000;
                request.ContentType = "application/json;charset=UTF-8";
                if (customHeaders != null && customHeaders.Count > 0)
                {
                    foreach (var item in customHeaders)
                    {
                        request.Headers.Add(item.Key, System.Web.HttpUtility.UrlEncode(item.Value, Encoding.UTF8));
                    }
                }
                byte[] payload = Encoding.UTF8.GetBytes(json);
                //if (SupportGZip)
                //{
                //    payload = HttpExtension.Compress(payload);
                //}
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(payload, 0, payload.Length);
                    writer.Close();
                    data.BytesSent += request.GetRequestLength();
                    SentBytes += data.BytesSent;
                    LastSentBytes += data.BytesSent;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Encoding encoding = GetEncoding(response);
                        result = GetResponseAsString(response, encoding, ref data);
                        data.BytesReceived += response.GetResponseLength();
                        ReceivedBytes += data.BytesReceived;
                        LastReceivedBytes += data.BytesReceived;
                    }
                }
            }
            catch (Exception e)
            {
                data.Exception = e;
                string message = e.Message;
                WebException webException = e as WebException;
                if (webException != null)
                {
                    try
                    {
                        HttpWebResponse response = webException.Response as HttpWebResponse;
                        Encoding encoding = GetEncoding(response);
                        string body = GetResponseAsString(response, encoding, ref data);
                        message += Environment.NewLine + "服务返回：" + body;
                        data.BytesReceived += response.GetResponseLength();
                        ReceivedBytes += data.BytesReceived;
                        LastReceivedBytes += data.BytesReceived;
                    }
                    catch { }
                }
                if (Debugger.IsAttached)
                {
                    message += Environment.NewLine + e.StackTrace.ToString();
                }
                result = GetJson(new ErrorWMSResponse { Code = e.Message, Message = message });
            }
            watch.Stop();
            data.NetworkElapseTime = watch.ElapsedMilliseconds;
            return result;
        }

        private static Encoding GetEncoding(HttpWebResponse response)
        {
            Encoding encoding = Encoding.UTF8;
            try
            {
                encoding = Encoding.GetEncoding(response.CharacterSet);
            }
            catch { }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding;
        }

        public static HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = UserAgentString;
            req.Timeout = _timeout;

            return req;
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding, ref APIExecuteStatistics data)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                byte[] buffer = Read2Buffer(stream);
                data.BytesReceived += buffer.Length;
                ReceivedBytes += buffer.Length;
                LastReceivedBytes += buffer.Length;
                bool gzip = (rsp.ContentEncoding ?? "").ToLower().Contains("gzip");
                SupportGZip = gzip;
                if (gzip)
                {
                    byte[] newBuffer = HttpExtension.Decompress(buffer);
                    return encoding.GetString(newBuffer, 0, newBuffer.Length);
                }
                else
                {
                    return encoding.GetString(buffer, 0, buffer.Length);
                }
            }
            finally
            {
                // 释放资源
                if (reader != null)
                {
                    reader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
        }

        public static byte[] Read2Buffer(Stream stream, int bufferLength = 0x8000)
        {
            // 如果指定的无效长度的缓冲区，则指定一个默认的长度作为缓存大小
            if (bufferLength < 1)
            {
                bufferLength = 0x8000;
            }
            // 初始化一个缓存区
            byte[] buffer = new byte[bufferLength];
            int read = 0;
            int block;
            // 每次从流中读取缓存大小的数据，直到读取完所有的流为止
            while ((block = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                // 重新设定读取位置
                read += block;
                // 检查是否到达了缓存的边界，检查是否还有可以读取的信息
                if (read == buffer.Length)
                {
                    // 尝试读取一个字节
                    int nextByte = stream.ReadByte();
                    // 读取失败则说明读取完成可以返回结果
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    // 调整数组大小准备继续读取
                    byte[] newBuf = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuf, buffer.Length);
                    newBuf[read] = (byte)nextByte;
                    buffer = newBuf;// buffer是一个引用（指针），这里意在重新设定buffer指针指向一个更大的内存
                    read++;
                }
            }
            // 如果缓存太大则使用ret来收缩前面while读取的buffer，然后直接返回
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetJson(object model)
        {
            JsonSerializerSettings settings = GetDefaultSettings();
            settings.ContractResolver = new LimitPropertiesContractResolver(JsonIgnoreDirectionType.ToJson);
            if (Debugger.IsAttached)
            {
                return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
            }
            else
            {
                return JsonConvert.SerializeObject(model, Formatting.None, settings);
            }
        }

        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json) where T : class
        {
            JsonSerializerSettings settings = GetDefaultSettings();
            settings.ContractResolver = new LimitPropertiesContractResolver(JsonIgnoreDirectionType.ToObject);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        private static JsonSerializerSettings GetDefaultSettings()
        {
            WMSDateTimeConverter timeFormatConverter = new WMSDateTimeConverter();
            timeFormatConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(timeFormatConverter);
            return settings;
        }
    }

    public class WMSDateTimeConverter : IsoDateTimeConverter
    {
        private static DateTime _time = DateTime.Parse("1990-01-01");
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is DateTime)
            {
                try
                {
                    DateTime time = (DateTime)value;
                    if (time < _time)
                    {
                        value = _time;
                    }
                }
                catch { }
            }
            base.WriteJson(writer, value, serializer);
        }
    }


    public class LimitPropertiesContractResolver : DefaultContractResolver
    {
        public JsonIgnoreDirectionType Direction { get; private set; }
        public LimitPropertiesContractResolver(JsonIgnoreDirectionType directionType)
        {
            Direction = directionType;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            object[] obj = type.GetCustomAttributes(typeof(JsonIgnoreDirectionAttribute), false);
            if (obj.Length > 0)
            {
                JsonIgnoreDirectionAttribute attr = obj.FirstOrDefault(o => { return o is JsonIgnoreDirectionAttribute; }) as JsonIgnoreDirectionAttribute;
                // 经处理类上标记了JsonIgnoreDirectionAttribute的，以提升性能
                if (attr != null)
                {
                    Type ignoreType = Direction == JsonIgnoreDirectionType.ToJson ? typeof(IgnoreToJsonAttribute) : typeof(IgnoreToObjectAttribute);
                    List<string> ignoreList = new List<string>();
                    PropertyInfo[] propertyList = type.GetProperties();
                    if (propertyList != null && propertyList.Length > 0)
                    {
                        var tempList = (from g in
                                            (from f in propertyList
                                             select new { Name = f.Name, Values = f.GetCustomAttributes(ignoreType, false) })
                                        where g.Values != null && g.Values.Length > 0
                                        select g.Name).ToList();
                        if (tempList.Count > 0)
                        {
                            var tmp = (from g in
                                           (from f in propertyList
                                            where tempList.Contains(f.Name)
                                            select new { Name = f.Name, Values = f.GetCustomAttributes(typeof(JsonPropertyAttribute), false) })
                                       where g.Values != null && g.Values.Length > 0
                                       select new { Name = g.Name, JsonPropertyName = (g.Values[0] as JsonPropertyAttribute).PropertyName }).ToList();
                            foreach (var item in tmp)
                            {
                                if (!string.IsNullOrEmpty(item.JsonPropertyName))
                                {
                                    tempList.Remove(item.Name);
                                    tempList.Add(item.JsonPropertyName);
                                }
                            }
                        }
                        ignoreList = ignoreList.Union(tempList).ToList();
                    }
                    FieldInfo[] fieldList = type.GetFields();
                    if (fieldList != null && fieldList.Length > 0)
                    {
                        var tempList = (from g in
                                            (from f in propertyList
                                             select new { Name = f.Name, Values = f.GetCustomAttributes(ignoreType, false) })
                                        where g.Values != null && g.Values.Length > 0
                                        select g.Name).ToList();
                        if (tempList.Count > 0)
                        {
                            var tmp = (from g in
                                           (from f in propertyList
                                            where tempList.Contains(f.Name)
                                            select new { Name = f.Name, Values = f.GetCustomAttributes(typeof(JsonPropertyAttribute), false) })
                                       where g.Values != null && g.Values.Length > 0
                                       select new { Name = g.Name, JsonPropertyName = (g.Values[0] as JsonPropertyAttribute).PropertyName }).ToList();
                            foreach (var item in tmp)
                            {
                                if (!string.IsNullOrEmpty(item.JsonPropertyName))
                                {
                                    tempList.Remove(item.Name);
                                    tempList.Add(item.JsonPropertyName);
                                }
                            }
                        }
                        ignoreList = ignoreList.Union(tempList).ToList();
                    }
                    if (ignoreList.Count > 0)
                    {
                        properties = (from f in properties
                                      where !ignoreList.Contains(f.PropertyName)
                                      select f).ToList();
                    }
                }
            }
            return properties;
        }
    }

    /// <summary>
    /// 指定序列化和反序列化时按方向忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class JsonIgnoreDirectionAttribute : Attribute
    {

    }
    /// <summary>
    /// 指定序列化时忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IgnoreToJsonAttribute : Attribute
    {

    }
    /// <summary>
    /// 指定反序列化时忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IgnoreToObjectAttribute : Attribute
    {

    }
    /// <summary>
    /// 忽略方向
    /// </summary>
    public enum JsonIgnoreDirectionType
    {
        /// <summary>
        /// 序列化为Json字符串时忽略
        /// </summary>
        ToJson,
        /// <summary>
        /// 反序列化为对象时忽略
        /// </summary>
        ToObject
    }
}

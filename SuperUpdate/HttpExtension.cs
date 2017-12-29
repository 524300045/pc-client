using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace WmsSDK
{
    public static class HttpExtension
    {
        /// <summary>
        /// 获取请求的字节数(包含已写入流的字节数)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static long GetRequestLength(this HttpWebRequest request)
        {
            long result = 0;
            if (request != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} {1} HTTP/{2}.{3}", request.Method, request.RequestUri, request.ProtocolVersion.Major, request.ProtocolVersion.Minor);
                builder.Append(Environment.NewLine);
                builder.Append("host: ");
                builder.Append(request.RequestUri.Authority);
                builder.Append(Environment.NewLine);
                builder.Append("connection: Keep-Alive");
                builder.Append(Environment.NewLine);
                builder.Append("content-type: ");
                builder.Append(request.ContentType);
                builder.Append(Environment.NewLine);
                builder.Append("content-length: ");
                builder.Append(request.ContentLength);
                builder.Append(Environment.NewLine);
                builder.AppendFormat("User-Agent: {0}", request.UserAgent);
                builder.Append(Environment.NewLine);
                foreach (var item in request.Headers.AllKeys)
                {
                    builder.AppendFormat("{0}: {1}", item, request.Headers[item]);
                    builder.Append(Environment.NewLine);
                }
                result = Encoding.UTF8.GetByteCount(builder.ToString());
                if (request.ContentLength > 0)
                {
                    result += request.ContentLength;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取响应的字节数(可传入Body一并计算字节数)
        /// </summary>
        /// <param name="response"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        internal static long GetResponseLength(this HttpWebResponse response, string body = "")
        {
            long result = 0;
            if (response != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("HTTP/{0}.{1} {2} {3}", response.ProtocolVersion.Major, response.ProtocolVersion.Minor, (int)response.StatusCode, response.StatusDescription);
                builder.AppendLine();
                foreach (var header in response.Headers.AllKeys)
                {
                    builder.AppendFormat("{0}: {1}", header, response.Headers[header]);
                    builder.AppendLine();
                }
                builder.AppendLine();
                if (!string.IsNullOrWhiteSpace(body))
                {
                    builder.Append(body);
                    builder.AppendLine();
                }
                result = Encoding.UTF8.GetByteCount(builder.ToString());
            }
            return result;
        }

        #region 字符串解压缩

        /// <summary>  
        /// 对字符串进行压缩  
        /// </summary>  
        /// <param name="value">待压缩的字符串</param>  
        /// <returns>压缩后的字符串</returns>  
        public static string CompressString(string value)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.UTF8.GetBytes(value);
            byte[] compressAfterByte = Compress(compressBeforeByte);
            compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }
        /// <summary>  
        /// 对字符串进行解压缩  
        /// </summary>  
        /// <param name="value">待解压缩的字符串</param>  
        /// <returns>解压缩后的字符串</returns>  
        public static string DecompressString(string value)
        {
            string compressString = "";
            byte[] compressBeforeByte = Convert.FromBase64String(value);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.UTF8.GetString(compressAfterByte);
            return compressString;
        }

        /// <summary>  
        /// 对byte数组进行压缩  
        /// </summary>  
        /// <param name="data">待压缩的byte数组</param>  
        /// <returns>压缩后的byte数组</returns>  
        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 对byte数组进行解压
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                byte[] buffer = Decompress(ms);
                ms.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static byte[] Decompress(Stream stream)
        {
            try
            {
                GZipStream zip = new GZipStream(stream, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}

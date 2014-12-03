using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Jobs.Utils
{
    public class RequestHelper
    {
        private static readonly string contentType="application/x-www-form-urlencoded";
        private static readonly int timeout = 30;    //second

   

        #region Post
        public static string Post(string url)
        {
           return MakeRequest(contentType, HttpVerb.Post, timeout, url, null, null);
        }
        public static void Post(string url, string contentType = null, int timeout = 30, string parameters="", IEnumerable<KeyValuePair<string, string>> heads = null)
        {
            MakeRequest(contentType, HttpVerb.Post, timeout, url, parameters, heads);
        }

        #endregion
        private static string MakeRequest(string contentType, HttpVerb method, int Timeout, string url, string parameters, IEnumerable<KeyValuePair<string, string>> heads)
        {

            byte[] bData = Encoding.UTF8.GetBytes(parameters); ;

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("url is empty");

            string strResult = string.Empty;
            try
            {
                HttpWebRequest hwRequest = (HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = Timeout * 1000;
                hwRequest.Method = method.ToString();
                hwRequest.ContentType = contentType;
                hwRequest.ContentLength = bData.Length;
                if (heads != null)
                {
                    foreach (var item in heads)
                    {
                        hwRequest.Headers.Add(item.Key, item.Value);
                    }
                }
               

                if (method == HttpVerb.Post)
                {
                    var smWrite = hwRequest.GetRequestStream();
                    smWrite.Write(bData, 0, bData.Length);
                    smWrite.Close();
                }

                var hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                var stream = hwResponse.GetResponseStream();
                if (stream != null && stream.CanRead)
                {
                    var srReader = new StreamReader(stream, Encoding.UTF8);
                    strResult = srReader.ReadToEnd();
                    srReader.Close();
                }
                hwResponse.Close();
            }
            catch (WebException webEx)
            {
                return webEx.Message + webEx;
            }
            return strResult;
        }


        private static byte[] BuildParameters(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            List<KeyValuePair<string, string>> list;
            if (parameters != null && parameters.Count() > 0)
                list = parameters.ToList();
            else
                list = new List<KeyValuePair<string, string>>();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var current in list)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }
                stringBuilder.Append(Encode(current.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Encode(current.Value));
            }

            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }
        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
    public enum HttpVerb
    {
        Get,
        Post
    }
}

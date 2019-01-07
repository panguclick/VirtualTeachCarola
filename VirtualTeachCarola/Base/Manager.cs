using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IniParser.Model;
using IniParser;

namespace VirtualTeachCarola.Base
{
    class Manager
    {
        private static Manager manager = null;

        public static Manager GetInstance()
        {
            if (manager == null)
            {
                manager = new Manager();
            }

            return manager;
        }

        private MainForm mMainForm = null;
        private DataRow[] subjectRows = null;
        private Config config = new Config();
        private User user = new User();
        private Car car = new Car();

        public MainForm MMainForm { get => mMainForm; set => mMainForm = value; }
        public DataRow[] SubjectRows { get => subjectRows; set => subjectRows = value; }
        internal Config Config { get => config; set => config = value; }
        internal User User { get => user; set => user = value; }
        internal Car Car { get => car; set => car = value; }

        public void RegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand += eventFun;
        }

        public void UnRegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand -= eventFun;
        }

        public void CleanSubject()
        {
            AccessHelper.GetInstance().ExcuteSql("UPDATE GzInfo SET Choice='未选择'");
        }

        public void InitSubject(string subjects)
        {
            AccessHelper.GetInstance().ExcuteSql("UPDATE GzInfo SET Choice='选择' WHERE GZID in(" + subjects + ")");
        }

        public void UpdateSubject()
        {
            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo ORDER BY GZID");
            SubjectRows = dataTable.Select("Choice = '选择'");
        }

        public Boolean HasSubject(string argv, ref Boolean isGvalue)
        {
            string[] sArray = Regex.Split(argv, ",", RegexOptions.IgnoreCase);

            for (int i = 0; i < sArray.Length; i++)
            {
                for(int j = 0; j < subjectRows.Length; j++)
                {
                    if(subjectRows[j]["GZID"].ToString() == sArray[i])
                    {
                        if(subjectRows[j]["GZName"].ToString().Contains("短路"))
                        {
                            isGvalue = true;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        /// 创建POST方式的HTTP请求  
        public static HttpWebResponse CreatePostHttpResponse(string url, 
            IDictionary<string, string> parameters,
            string method,
            int timeout, 
            string userAgent, 
            CookieCollection cookies)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = method;
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.Accept = "application/json";


            //设置代理UserAgent和超时
            //request.UserAgent = userAgent;
            //request.Timeout = timeout; 

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            try
            {
                //发送POST数据  
                if (!(parameters == null || parameters.Count == 0))
                {
                    string json = JsonConvert.SerializeObject(parameters);
                    byte[] data = Encoding.ASCII.GetBytes(json);
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                string[] values = request.Headers.GetValues("Content-Type");
                return request.GetResponse() as HttpWebResponse;
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
  
        }


        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }

        public void InitConfig()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Data\\config.ini");
            Config.Http = data["Setup"]["Http"];
            Config.Temp = data["Setup"]["Temp"];
            Config.Pressure = data["Setup"]["Pressure"];
        }
    }
}

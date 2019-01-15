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
        private List<int> selectSubjects = new List<int>();

        private DataTable ckvalueDataTbale = null;
        private DataTable bytDataTable = null;

        public MainForm MMainForm { get => mMainForm; set => mMainForm = value; }
        public DataRow[] SubjectRows { get => subjectRows; set => subjectRows = value; }
        internal Config Config { get => config; set => config = value; }
        internal User User { get => user; set => user = value; }
        internal Car Car { get => car; set => car = value; }
        public List<int> SelectSubjects { get => selectSubjects; set => selectSubjects = value; }
        public DataTable CkvalueDataTbale { get => ckvalueDataTbale; set => ckvalueDataTbale = value; }
        public DataTable BytDataTable { get => bytDataTable; set => bytDataTable = value; }

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
            string json,
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
                if (json.Length > 0)
                {
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
            if(webresponse == null)
            {
                return "";
            }

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

            ckvalueDataTbale = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM CkValue");
            bytDataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM BYT");
        }

        public List<string> GetSubmitReport()
        {
            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM SubmitReport WHERE TestID = '" + Manager.GetInstance().User.PracticID + "' ORDER BY ID");
            DataRow[] rows = dataTable.Select();

            List<string> res = new List<string>();

            for (int i = 0; i < rows.Length; i++)
            {
                string content = ((DateTime)rows[i]["ID"]).ToString("d");

                if (rows[i]["wgcz"] != null && rows[i]["wgcz"].ToString() == "1")
                {
                    content += " 违规操作:";
                }

                if (rows[i]["Ename"].ToString() != "")
                {
                    content += " " + rows[i]["Ename"];
                }

                if (rows[i]["Oper"].ToString() != "")
                {
                    content += " " + rows[i]["Oper"];
                }

                if (rows[i]["SubMit"].ToString() != "")
                {
                    content += " " + rows[i]["SubMit"];
                }

                res.Add(content);
            }

            return res;
        }

        public static bool ExcuteSQLNeedTwoPoint(RBData bData, 
            RBData rData,
            int valueType,
            DataTable dataTable,
            ref Boolean canYoumen
            )
        {
            bool res = false;
            bool isMax = false;
            try
            {
                if (bData.BaseValue == "" || rData.BaseValue == "")
                {
                    return false;
                }

                string sql = "CheckPoint1 = '" + rData.BaseValue
                    + "' AND CheckPoint2 = '" + bData.BaseValue
                    + "' AND Gearshift = '" + Manager.GetInstance().Car.Gearshift
                    + "' AND accorrun = " + Manager.GetInstance().Car.Power()
                    + " AND breaks = " + Manager.GetInstance().Car.BreakType
                    + " AND ValueType = " + valueType
                    + " AND IsLine = " + Manager.GetInstance().Car.IsLine;
                DataRow[] rows = dataTable.Select(sql);

                if (rows.Length == 0)
                {
                    sql = "CheckPoint1 = '" + bData.BaseValue
                        + "' AND CheckPoint2 = '" + rData.BaseValue
                        + "' AND Gearshift = '" + Manager.GetInstance().Car.Gearshift
                        + "' AND accorrun = " + Manager.GetInstance().Car.Power()
                        + " AND breaks = " + Manager.GetInstance().Car.BreakType
                        + " AND ValueType = " + valueType
                        + " AND IsLine = " + Manager.GetInstance().Car.IsLine;
                    rows = dataTable.Select(sql);
                    if (rows.Length == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    isMax = true;
                }

                string v = (string)rows[0]["Nvalue"];
                string[] sArray = Regex.Split(v, "-", RegexOptions.IgnoreCase);

                if (isMax)
                {
                    rData.MinValue = float.Parse(sArray[0]);
                    canYoumen = true;

                    if (sArray.Length > 1)
                    {
                        rData.MaxValue = float.Parse(sArray[1]);
                    }
                    else
                    {
                        rData.MaxValue = float.Parse(sArray[0]);
                    }
                }
                else
                {
                    bData.MinValue = float.Parse(sArray[0]);

                    if (sArray.Length > 1)
                    {
                        bData.MaxValue = float.Parse(sArray[1]);
                    }
                    else
                    {
                        bData.MaxValue = float.Parse(sArray[0]);
                    }
                }

                bool isGvalue = false;

                if (rows[0]["Gzm"].GetType().Name != "DBNull" && Manager.GetInstance().HasSubject((string)rows[0]["Gzm"], ref isGvalue))
                {
                    if (isGvalue)
                    {
                        rData.MinValue = float.Parse((string)rows[0]["GValue"]);
                    }
                    else
                    {
                        rData.MinValue = float.Parse((string)rows[0]["DValue"]);
                    }

                    bData.MinValue = 0;
                }

                res = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }
    }
}

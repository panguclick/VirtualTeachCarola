using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public MainForm MMainForm { get => mMainForm; set => mMainForm = value; }
        public DataRow[] SubjectRows { get => subjectRows; set => subjectRows = value; }

        public void RegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand += eventFun;
        }

        public void UnRegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand -= eventFun;
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
    }
}

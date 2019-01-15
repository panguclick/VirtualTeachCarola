using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    class WYBDevice : Device
    {
        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if (e.command == "power" || e.command == "sc" ||e.command == "youmeng" ||e.command == "speed")
            {
                if (BbValue.BaseValue != "" && RbValue.BaseValue != "")
                {
                    UpdateRBValue();
                }
            }
            else if (e.command == "vt")
            {
                ValueType = int.Parse(e.args);
                SetTipValue(0);

                if (ValueType == 1)
                {
                    if (BbValue.BaseValue != "" && RbValue.BaseValue != "")
                    {
                        UpdateRBValue();
                    }
                }

                if (ValueType == 0)
                {
                    StopTime();
                }
                else
                {
                    StartTime();
                }
            }
            else if( e.command == "BB")
            {
                if(e.args != "")
                {
                    BbValue.BaseValue = e.args;
                    UpdateRBValue();
                }
                else
                {
                    BbValue.Reset();
                    SetTipValue(0);
                }
            }
            else if (e.command == "RB")
            {
                if (e.args != "")
                {
                    RbValue.BaseValue = e.args;
                    UpdateRBValue();
                }
                else
                {
                    RbValue.Reset();
                    SetTipValue(0);
                }
            }
            else if (e.command == "ZL")
            {
                Record();
            }
            else if (e.args == "ZXJC"
                    || e.args == "JC")
            {
                DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM Element WHERE ID ='" + e.command + "'");
                DataRow[] rows = dataTable.Select();

                Target = rows[0];
            }

            ShowValue();
        }

        private void ShowValue()
        {
            System.Timers.Timer t = new System.Timers.Timer(300);//实例化Timer类，设置间隔时间为10000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(ShowTimerMethod);//到达时间的时候执行事件；
            t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private bool ExcuteSQLNeedOnePoint(RBData data)
        {
            bool res = false;
            try
            {
                if(data.BaseValue == "")
                {
                    return false;
                }

                string sql = "CheckPoint1 = '" + data.BaseValue
                    + "' AND Gearshift = '" + Manager.GetInstance().Car.Gearshift
                    + "' AND accorrun = " + Manager.GetInstance().Car.Power()
                    + " AND breaks = " + Manager.GetInstance().Car.BreakType
                    + " AND ValueType = " + ValueType
                    + " AND IsLine = " + Manager.GetInstance().Car.IsLine;
                DataRow[] bbData = Manager.GetInstance().CkvalueDataTbale.Select(sql);

                if (bbData.Length == 0)
                {
                    return res;
                }

                string v = (string)bbData[0]["Nvalue"];
                string[] sArray = Regex.Split(v, "-", RegexOptions.IgnoreCase);
                data.MinValue = float.Parse(sArray[0]);

                if (sArray.Length > 1)
                {
                    data.MaxValue = float.Parse(sArray[1]);
                }
                else
                {
                    data.MaxValue = float.Parse(sArray[0]);
                }

                res = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        private void UpdateRBValue()
        {
            CanYouMen = false;
            bool isYoumen = false;

            if (ExcuteSQLNeedOnePoint(BbValue) && ExcuteSQLNeedOnePoint(RbValue))
            {
                SetTipValue(RbValue.MinValue - BbValue.MinValue);
            }
            else if (Manager.ExcuteSQLNeedTwoPoint(BbValue, RbValue, ValueType, Manager.GetInstance().CkvalueDataTbale, ref isYoumen))
            {
                CanYouMen = isYoumen;
                float value = RbValue.MinValue - BbValue.MinValue;
                if (CanYouMen == true)
                {
                    value = RbValue.MinValue + Manager.GetInstance().Car.Speed * (RbValue.MaxValue - RbValue.MinValue) / 100;
                }

                SetTipValue(value);
            }
            else
            {
                SetTipValue(0);
            }
        }

        void ThreadTimerMethod(object o)
        {
            if(TipValue != "" && TipValue != "0.00" && TipValue != "0L")
            {
                Random ran = new Random();
                int n = ran.Next(-1, 2);
                float v = float.Parse(TipValue) + n * 0.01f;
                FlashContrl.SetVariable("WYB", string.Format("{0:0.##}", v));
            }
        }

        private void ShowTimerMethod(object source, System.Timers.ElapsedEventArgs e)
        {
            FlashContrl.SetVariable("WYB", TipValue);
        }

        public void StartTime()
        {
            if(ThreadTime == null)
            {
                ThreadTime = new Timer(ThreadTimerMethod, null, 0, 1000);
            }
        }

        public void StopTime()
        {
            if(ThreadTime != null)
            {
                ThreadTime.Dispose();
                ThreadTime = null;
            }
        }

        private void SetTipValue(float value)
        {
            if (ValueType == 0 && value == 0)
            {
                TipValue = "";
            }
            else if (ValueType == 1 && value == 0)
            {
                TipValue = "0.00";
            }
            else if (ValueType == 2 && value == 0)
            {
                TipValue = "0L";
            }
            else
            {
                TipValue = string.Format("{0:0.##}", value);
            }
        }

        private void Record()
        {
            string sql = "insert into RecordTest (Red,Black,TestValue,TestTime,ValueType,TestID,IsLine) values ('"
            + RbValue.BaseValue + "','"
            + BbValue.BaseValue + "','"
            + TipValue + "','"
            + DateTime.Now.ToLocalTime().ToString() + "','"
            + ValueType + "','"
            + Manager.GetInstance().User.PracticID + "','"
            + Manager.GetInstance().Car.IsLine
            + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);

            string eName = "";
            if(Target != null && RbValue.BaseValue != "0" && BbValue.BaseValue != "0")
            {
                eName = (string)Target["LineName"] + " ";
            }

            sql = "insert into SubmitReport (ID,Oper,SubMit,TestID) values ('"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + "测量元件" + "','"
                        + eName + "电压值:" + TipValue + "','"
                        + Manager.GetInstance().User.PracticID
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);
        }
    }
}

using AxShockwaveFlashObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    public partial class MainForm : Form
    {
        WYBDevice mWYBDvice = new WYBDevice();
        SBQDevice mSBQDvice = new SBQDevice();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadFlash.AlignMode = 4;
            LoadFlash.DisableLocalSecurity();
            LoadFlash.Dock = DockStyle.Fill;
            LoadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\login.swf");
            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(Manager.GetInstance().Car.FlashFlashCommand);
            mWYBDvice.FlashContrl = LoadFlash;

            mSBQDvice.FlashContrl = LoadFlash;
        }

        public void FlashFlashCommand(object sender, _IShockwaveFlashEvents_FSCommandEvent e)
        {
            // We only want to react if we got our command
            if (e.command == "shezhijiemian")
            {
                ShowSetting();
            }
            else if (e.command == "Quit")
            {
                Exit();
            }
            else if (e.command == "AppQuit")
            {
                QuitApp();
            }
            else if (e.command == "Login")
            {
                Login(e.args);
            }
            else if (e.command == "ZLK")
            {
                ShowReference();
            }
            else if (e.command == "SB" && e.args == "K600")
            {
                ShowK600();
            }
            else if (e.command == "equipment" && e.args == "ZDY")
            {
                ShowZDY();
            }
            else if (e.command.Equals("SB") && e.args.Equals("WYB"))
            {
                ShowWYB();
            }
            else if (e.command == "WYB" && e.args == "Close")
            {
                CloseWYB();
            }
            else if (e.command.Equals("SB") && e.args.Equals("SBQ"))
            {
                ShowSBQ();
            }
            else if (e.command == "SBQ" && e.args == "Close")
            {
                CloseSBQ();
            }
            else if (e.command == "SB" && e.args == "GZDQR")
            {
                ShowGZDQR();
            }
            else if (e.command == "JCBG")
            {
                ShowJCBG();
            }
            else if (e.command == "cpxx" && e.args == "zl")
            {
                Manager.GetInstance().Car.CarType = "TV802GL";
                Manager.GetInstance().Car.CarCode = "LFMAPE2G480036594";
                Manager.GetInstance().Car.EnginType = "2ZR";
            }
            else if (e.command == "WGCZ")
            {
                ExcuteWGCZ(e.args);
            }
            else if (e.args == "DKLJ" 
                || e.args == "LJ" 
                || e.args == "ZXJC"
                || e.args == "JCSD"
                || e.args == "JC")
            {
                ExcuteOper(e.args, e.command);
            }
            else if(e.command == "kaohe" || e.command == "shixun")
            {
                Manager.GetInstance().User.Mode = e.command;
            }
            else if(e.command == "TJDA")
            {
                SubmitAnswer();
            }

            ///////////////////////////日志/////////////////////////////////////
            if (e.args != "" || (e.command != "RB" && e.command.Equals("BB")))
            {
                Console.WriteLine("e.command = " + e.command);
                Console.WriteLine("e.args = " + e.args);
            }
        }

        private void Exit()
        {
            DialogResult dr;
            dr = MessageBox.Show("您真的要退出系统吗？", "虚拟仿真教学-卡罗拉", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                AccessHelper.GetInstance().Release();
                Environment.Exit(0);
            }

        }

        private void QuitApp()
        {
            if(Manager.GetInstance().User.Mode == "shixun")
            {
                LoadFlash.FSCommand -= FlashFlashCommand;

                SetGZForm form = new SetGZForm();
                form.ShowDialog(this);

                LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            }
            else
            {
                Exit();
            }


        }

        private void Login(string argv)
        {
            //参数p
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string[] sArray = Regex.Split(argv, ",", RegexOptions.IgnoreCase);
            parameters.Add("stuId", sArray[0]);
            parameters.Add("password", sArray[1]);

            //http请求
            string json = JsonConvert.SerializeObject(parameters);

            System.Net.HttpWebResponse res = Manager.CreatePostHttpResponse(Manager.GetInstance().Config.Http + "/user/stuLogin", json, "POST", 3000, null, null);
            string msg = "";

            if (res == null)
            {
                Console.WriteLine("网络服务异常");
            }
            else
            {
                //获取返回数据转为字符串
                msg = Manager.GetResponseString(res);
                Manager.GetInstance().User.HttpStudent = JsonConvert.DeserializeObject<HttpStudent>(msg);
            }

            if(msg.Length == 0 || Manager.GetInstance().User.HttpStudent.Code == 0)
            {
                MessageBox.Show("用户名或密码错误", "虚拟仿真教学-卡罗拉", MessageBoxButtons.OK,
                         MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            Manager.GetInstance().CleanSubject();

            if (Manager.GetInstance().User.Mode == "kaohe")
            {
                res = Manager.CreatePostHttpResponse(Manager.GetInstance().Config.Http + "/external/gzList?stuId=" + Manager.GetInstance().User.HttpStudent.Data.Id, "", "GET", 3000, null, null);
                msg = Manager.GetResponseString(res);

                if(msg.Length == 0)
                {
                    MessageBox.Show("你还不能考试哦！", "虚拟仿真教学-卡罗拉", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                Manager.GetInstance().User.HttpExam = JsonConvert.DeserializeObject<HttpExam>(msg);

                Manager.GetInstance().InitSubject(Manager.GetInstance().User.HttpExam.TestContent);
            }

            LoadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\index.swf");
            mWYBDvice.DataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM CkValue");
            mSBQDvice.DataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM BYT");


            Manager.GetInstance().UpdateSubject();

            LoadFlash.SetVariable("Temp", Manager.GetInstance().Config.Temp);
            LoadFlash.SetVariable("Pressure", Manager.GetInstance().Config.Pressure);

            if(Manager.GetInstance().User.Mode == "kaohe")
            {
                TimeSpan timeSpan = Manager.GetInstance().User.HttpExam.TEndTime - DateTime.Now.ToLocalTime();
                LoadFlash.SetVariable("LoginInf", Manager.GetInstance().User.HttpStudent.Data.StuId
                    + "," + Manager.GetInstance().User.HttpStudent.Data.StuName
                    + "," + timeSpan.TotalMinutes
                    + "," + Manager.GetInstance().SubjectRows.Length
                    + "," + Manager.GetInstance().User.HttpStudent.Data.ClassNum
                    );

                Manager.GetInstance().User.PracticID = Manager.GetInstance().User.HttpExam.TId + "-" + Manager.GetInstance().User.HttpStudent.Data.StuId;

            }
            else
            {
                Manager.GetInstance().User.PracticID = Manager.GetInstance().User.HttpStudent.Data.StuId + DateTime.Now.ToFileTimeUtc().ToString();
            }

        }

        private void ShowSetting()
        {
            Form winform = new SettingForm();
            winform.ShowDialog();
        }

        private void ShowReference()
        {
            Form winform = new ReferenceForm();
            winform.ShowDialog();
        }

        private void ShowZDY()
        {
            IT2Form iT2Form = new IT2Form();
            iT2Form.Show(this);
        }

        private void ShowWYB()
        {
            CloseSBQ();
            mWYBDvice.TipValue = "";
            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mWYBDvice.FlashFlashCommand);
        }

        private void CloseWYB()
        {
            LoadFlash.FSCommand -= mWYBDvice.FlashFlashCommand;
        }

        private void ShowSBQ()
        {
            CloseWYB();
            mSBQDvice.TipValue = "";
            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mSBQDvice.FlashFlashCommand);

        }

        private void CloseSBQ()
        {
            LoadFlash.FSCommand -= mSBQDvice.FlashFlashCommand;
        }

        private void ShowK600()
        {
            K600Form k600Form = new K600Form();
            k600Form.Show(this);
        }

        private void ShowGZDQR()
        {
            GZDQRForm form = new GZDQRForm();
            form.ShowDialog(this);
        }

        private void ShowJCBG()
        {
            GZJLForm form = new GZJLForm();
            form.ShowDialog(this);
        }

        private void ExcuteWGCZ(string id)
        {
            if(id == "")
            {
                return;
            }

            Manager.GetInstance().User.AddWgczCount();

            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM WGCZ WHERE ID =" + id);
            DataRow[] rows = dataTable.Select();

            string sql = "insert into RecordOper (OPeration,TestID,OperTime,wgcz,Ename) values ('"
                        + rows[0]["Dist"] + "','"
                        + Manager.GetInstance().User.PracticID + "','"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + 1 + "','"
                        + rows[0]["Ename"]
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);


            sql = "insert into SubmitReport (ID,Ename,Oper,wgcz,TestID) values ('"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + rows[0]["Ename"] + "','"
                        + rows[0]["Dist"] + "','"
                        + 1 + "','"
                        + Manager.GetInstance().User.PracticID
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);
        }

        private void ExcuteOper(string operID, string eID)
        {
            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM Element WHERE ID ='" + eID + "'");
            DataRow[] rows = dataTable.Select();

            string oper = GetOperName(operID);

            string sql = "insert into RecordOper (EID,OPeration,TestID,OperTime,Ename) values ('"
                        + eID + "','"
                        + oper + "','"
                        + Manager.GetInstance().User.PracticID + "','"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + rows[0]["Ename"]
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);


            sql = "insert into SubmitReport (ID,Ename,Oper,TestID) values ('"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + rows[0]["Ename"] + "','"
                        + oper + "','"
                        + Manager.GetInstance().User.PracticID
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);
        }

        private string GetOperName(string id)
        {
            string oper = "";

            switch (id)
            {
                case "DKLJ":
                    {
                        oper = "断开连接";
                        break;
                    }
                case "LJ":
                    {
                        oper = "连接";
                        break;
                    }
                case "ZXJC":
                    {
                        oper = "在线检测";
                        break;
                    }
                case "JC":
                    {
                        oper = "检测";
                        break;
                    }
                case "JCSD":
                    {
                        oper = "检查松动";
                        break;
                    }
                default:
                    break;
            }

            return oper;
        }

        private void SubmitAnswer()
        {
            if(Manager.GetInstance().User.Mode == "kaohe")
            {
                HttpSubmitReport httpSubmitReport = new HttpSubmitReport();
                httpSubmitReport.stuId = int.Parse(Manager.GetInstance().User.HttpStudent.Data.StuId);
                httpSubmitReport.testId = Manager.GetInstance().User.HttpExam.TId;
                httpSubmitReport.answers = Manager.GetInstance().SelectSubjects;
                httpSubmitReport.wgczCount = Manager.GetInstance().User.WgczCount;

                httpSubmitReport.contents = Manager.GetInstance().GetSubmitReport();

                string json = JsonConvert.SerializeObject(httpSubmitReport);

                System.Net.HttpWebResponse res = Manager.CreatePostHttpResponse(Manager.GetInstance().Config.Http + "/external/sendScore", json, "POST", 3000, null, null);
                string msg = "";

                if (res == null)
                {
                    Console.WriteLine("网络服务异常");
                }
                else
                {
                    //获取返回数据转为字符串
                    msg = Manager.GetResponseString(res);
                }
            }
        }
    }
}
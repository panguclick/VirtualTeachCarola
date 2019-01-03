using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    public partial class MainForm : Form
    {
        Car mCar = new Car();
        WYBDevice mWYBDvice = new WYBDevice();
        SBQDevice mSBQDvice = new SBQDevice();
        private User mUser = new User();

        internal User MUser { get => mUser; set => mUser = value; }

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
            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mCar.FlashFlashCommand);

            mWYBDvice.Car = mCar;
            mWYBDvice.FlashContrl = LoadFlash;

            mSBQDvice.Car = mCar;
            mSBQDvice.FlashContrl = LoadFlash;
        }

        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
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
                mCar.CarType = "TV802GL";
                mCar.CarCode = "LFMAPE2G480036594";
                mCar.EnginType = "2ZR";
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
                System.Environment.Exit(0);
            }

        }

        private void QuitApp()
        {
            LoadFlash.FSCommand -= FlashFlashCommand;

            SetGZForm form = new SetGZForm();
            form.ShowDialog(this);

            LoadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);

        }

        private void Login(string argv)
        {

            string[] sArray = Regex.Split(argv, ",", RegexOptions.IgnoreCase);

            DataTable userDataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM Reg");
            DataRow[] rows = userDataTable.Select("UserID='" + sArray[0] + "' And Pwd='" + sArray[1] + "'");

            if(rows.Length == 0)
            {
                MessageBox.Show("用户名或密码错误", "虚拟仿真教学-卡罗拉", MessageBoxButtons.OK,
                         MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }


            LoadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\index.swf");
            mWYBDvice.DataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM CkValue");
            mSBQDvice.DataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM BYT");

            MUser.LoginID = sArray[0];
            MUser.LoginPws = sArray[1];

            MUser.PracticID = MUser.LoginID + DateTime.Now.ToFileTimeUtc().ToString();
            Manager.GetInstance().UpdateSubject();
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
            iT2Form.MCar = mCar;
            iT2Form.MUser = MUser;
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
            k600Form.MCar = mCar;
            k600Form.Show(this);
        }

        private void ShowGZDQR()
        {
            GZDQRForm form = new GZDQRForm();
            form.MUser = MUser;
            form.ShowDialog(this);
        }

        private void ShowJCBG()
        {
            GZJLForm form = new GZJLForm();
            form.MUser = MUser;
            form.MCar = mCar;
            form.ShowDialog(this);
        }

        private void ExcuteWGCZ(string id)
        {
            if(id == "")
            {
                return;
            }

            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM WGCZ WHERE ID =" + id);
            DataRow[] rows = dataTable.Select();

            string sql = "insert into RecordOper (OPeration,TestID,OperTime,wgcz,Ename) values ('"
                        + rows[0]["Dist"] + "','"
                        + MUser.PracticID + "','"
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
                        + MUser.PracticID
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
                        + MUser.PracticID + "','"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + rows[0]["Ename"]
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);


            sql = "insert into SubmitReport (ID,Ename,Oper,TestID) values ('"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + rows[0]["Ename"] + "','"
                        + oper + "','"
                        + MUser.PracticID
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
    }
}
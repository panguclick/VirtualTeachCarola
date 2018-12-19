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

namespace VirtualTeachCarola
{
    public partial class MainForm : Form
    {
        Car mCar = new Car();
        DataTable mWYBTable = null;
        DataTable mSBQTable = null;
        WYBDevice mWYBDvice = new WYBDevice();
        SBQDevice mSBQDvice = new SBQDevice();

        User mUser = new User();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            loadFlash.AlignMode = 4;
            loadFlash.DisableLocalSecurity();
            loadFlash.Dock = DockStyle.Fill;
            loadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\login.swf");
            loadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            loadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mCar.FlashFlashCommand);
            loadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mWYBDvice.FlashFlashCommand);
            loadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(mSBQDvice.FlashFlashCommand);

            mWYBDvice.Car = mCar;
            mWYBDvice.FlashContrl = loadFlash;
        }

        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            // We only want to react if we got our command
            if (e.command == "shezhijiemian" )
            {
                ShowSetting();
            }
            else if (e.command == "Quit")
            {
                Exit();
            }
            else if(e.command == "AppQuit")
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
            else if (e.command == "vt" && e.args == "0")
            {
                CloseWYB();
            }
            else if (e.command.Equals("SB") && e.args.Equals("SBQ"))
            {
                ShowSBQ();
            }
            else if(e.command == "SB" && e.args == "GZDQR")
            {
                ShowGZDQR();
            }
            else if(e.command == "JCBG")
            {
                ShowJCBG();
            }
            else if(e.command == "cpxx" && e.args == "zl")
            {
                mCar.CarType = "TV802GL";
                mCar.CarCode = "LFMAPE2G480036594";
                mCar.EnginType = "2ZR";
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
            loadFlash.FSCommand -= FlashFlashCommand;

            SetGZForm form = new SetGZForm();
            form.ShowDialog(this);

            loadFlash.FSCommand += new _IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);

        }

        private void Login(string argv)
        {
            loadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\index.swf");
            mWYBTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM CkValue");
            mSBQTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM BYT");

            string[] sArray = Regex.Split(argv, ",", RegexOptions.IgnoreCase);
            mUser.LoginID = sArray[0];
            mUser.LoginPws = sArray[1];

            mUser.PracticID = mUser.LoginID + DateTime.Now.ToFileTimeUtc().ToString();
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
            mWYBDvice.TipValue = "";
            mWYBDvice.DataTable = mWYBTable;
        }

        private void CloseWYB()
        {

        }

        private void ShowSBQ()
        {
            mWYBDvice.DataTable = mSBQTable;
            mWYBDvice.TipValue = "";
            loadFlash.FSCommand -= mWYBDvice.FlashFlashCommand;
        }

        private void CloseSBQ()
        {

        }

        private void ShowK600()
        {
            K600Form k600Form = new K600Form();
            k600Form.Show(this);
        }

        private void ShowGZDQR()
        {
            GZDQRForm form = new GZDQRForm();
            form.MUser = mUser;
            form.ShowDialog(this);
        }

        private void ShowJCBG()
        {
            GZJLForm form = new GZJLForm();
            form.MUser = mUser;
            form.MCar = mCar;
            form.ShowDialog(this);
        }
    }
}

using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VirtualTeachCarola
{
    public partial class MainForm : Form
    {
        Car mCar = new Car();
        DataTable mDataTable = null;
        WYBDevice mWYBDvice = new WYBDevice();

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
            loadFlash.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(FlashFlashCall);
            loadFlash.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            loadFlash.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(mCar.FlashFlashCommand);
            loadFlash.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(mWYBDvice.FlashFlashCommand);

            mWYBDvice.Car = mCar;
            mWYBDvice.FlashContrl = loadFlash;

        }

        void FlashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            // We only want to react if we got our command
            if (e.command == "shezhijiemian" )
            {
                ShowSetting();
            }
            else if (e.command == "Quit")
            {
                QuitApp();
            }
            else if (e.command == "Login")
            {
                Login();
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
            else if(e.command == "vt" && e.args == "0")
            {
                CloseWYB();
            }

            ///////////////////////////日志/////////////////////////////////////
            if (e.args != "" || (e.command != "RB" && e.command.Equals("BB")))
            {
                Console.WriteLine("e.command = " + e.command);
                Console.WriteLine("e.args = " + e.args);
            }
        }

        private void QuitApp()
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

        private void Login()
        {
            loadFlash.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\index.swf");
            mDataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM CkValue");
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
            mWYBDvice.DataTable = mDataTable;
        }

        private void CloseWYB()
        {
        }

        private void ShowK600()
        {
            K600Form k600Form = new K600Form();
            k600Form.Show(this);
        }
    }
}

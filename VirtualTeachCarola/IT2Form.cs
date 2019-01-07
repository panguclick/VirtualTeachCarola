using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    public partial class IT2Form : Form
    {
        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;

        public IT2Form()
        {
            InitializeComponent();
        }

        private void InitData()
        {
            if(this.listViewDATA.Items.Count > 0)
            {
                return;
            }

            DataRow[] rows = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM zdy").Select();

            for (int i = 0; i < rows.Length; i++)
            {
                ListViewItem lvi = new ListViewItem();

                if (rows[i]["Choice"].GetType().Name == "DBNull")
                {
                    lvi.Text = " ";
                }
                else
                {
                    lvi.Text = (string)rows[i]["Choice"];
                }
                lvi.SubItems.Add((string)rows[i]["ProName"]);
                lvi.SubItems.Add((string)rows[i]["PValues"]);

                if (rows[i]["Units"].GetType().Name == "DBNull")
                {
                    lvi.SubItems.Add(" ");
                }
                else
                {
                    lvi.SubItems.Add((string)rows[i]["Units"]);
                }

                this.listViewDATA.Items.Add(lvi);
            }

            rows = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo").Select();

            for (int i = 0; i < rows.Length; i++)
            {

                if (rows[i]["Choice"].ToString() == "选择")
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = (string)rows[i]["DTID"];
                    lvi.SubItems.Add((string)rows[i]["DTC"]);

                    this.listViewDTC.Items.Add(lvi);
                }
            }

        }

        private void Flash_Enter(object sender, EventArgs e)
        {

            if(axShockwaveFlash1.Movie != null)
            {
                return;
            }

            pictureBox1.Load(System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\zdy_title.jpg");
            axShockwaveFlash1.DisableLocalSecurity();
            axShockwaveFlash1.ScaleMode = 0;
            axShockwaveFlash1.SAlign = "B";
            axShockwaveFlash1.Dock = DockStyle.Bottom;

            axShockwaveFlash1.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            Manager.GetInstance().RegisterEvent(new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand));
            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\zdy.swf");

            if (Manager.GetInstance().Car.Power() == 2)
            {
                axShockwaveFlash1.SetVariable("enterBTN", "1");
            }
            else
            {
                axShockwaveFlash1.SetVariable("enterBTN", "0");
            }

            InitData();
            HideControl();
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if(e.command == "hidezdy")
            {
                Manager.GetInstance().UnRegisterEvent(FlashFlashCommand);

                this.Close();
            }
            else if(e.command == "DTC" && e.args =="FDJ")
            {
                ShowDTC();
            }
            else if (e.command == "DLIST" && e.args == "FDJ")
            {
                ShowDLIST();
            }
            else if (e.command == "hide")
            {
                HideControl();
            }
            else if (e.command == "RJL" && e.args == "FDJ")
            {
                for (int i = 0; i < listViewDTC.Items.Count; i++)
                {
                    string id = listViewDTC.Items[i].SubItems[0].Text.ToString().Trim();
                    string value = listViewDTC.Items[i].SubItems[1].Text.ToString().Trim();
                    Record(id, value);
                }
            }
            else if (e.command == "JL" && e.args == "FDJ")
            {
                for (int i = 0; i < listViewDATA.Items.Count; i++)
                {
                    string select = listViewDATA.Items[i].SubItems[0].Text.ToString().Trim();
                    if(select == "√")
                    {
                        string id = listViewDATA.Items[i].SubItems[1].Text.ToString().Trim();
                        string value = listViewDATA.Items[i].SubItems[2].Text.ToString().Trim();
                        Record(id, value);
                    }
                }
            }
            else if (e.command == "power")
            {
                if (Manager.GetInstance().Car.Power() == 2)
                {
                    axShockwaveFlash1.SetVariable("enterBTN", "1");
                }
                else
                {
                    axShockwaveFlash1.SetVariable("enterBTN", "0");
                }
            }

            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void ShowDTC()
        {
            listViewDATA.Visible = false;
            listViewDTC.Visible = true;

        }

        private void ShowDLIST()
        {
            listViewDATA.Visible = true;
            listViewDTC.Visible = false;
        }

        private void HideControl()
        {
            listViewDATA.Visible = false;
            listViewDTC.Visible = false;
        }

        private void DTCListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListView_DoubleClick(object sender, MouseEventArgs e)
        {
            if(listViewDATA.Visible == false)
            {
                return;
            }

            for (int i = 0; i < listViewDATA.Items.Count; i++)
            {
                if (listViewDATA.Items[i].Selected)
                {
                    string value = listViewDATA.Items[i].SubItems[0].Text.ToString().Trim();
                    value = value == "√" ? "" : "√";
                    listViewDATA.Items[i].SubItems[0].Text = value;
                    break;
                }
            }
        }

        private void Record(string id, string value)
        {

            string sql = "insert into RecordZdy (ElementName,EValue,TestTime,TestID) values ('"
                        + id + "','"
                        + value + "','"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + Manager.GetInstance().User.PracticID
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);


            sql = "insert into SubmitReport (ID,Ename,Oper,TestID) values ('"
                        + DateTime.Now.ToLocalTime().ToString() + "','"
                        + "诊断仪记录:" + "','"
                        + id + ":" + value + "','"
                        + Manager.GetInstance().User.PracticID
                        + "')";

            AccessHelper.GetInstance().ExcuteSql(sql);
        }
    }
}

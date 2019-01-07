using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    public partial class K600Form : Form
    {
        public K600Form()
        {
            InitializeComponent();
        }

        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;

        private void flashControl_Enter(object sender, EventArgs e)
        {
            if(flashControl.Movie != null)
            {
                return;
            }

            titlePicture.Load(System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\k600.png");
            flashControl.DisableLocalSecurity();
            flashControl.Dock = DockStyle.Bottom;
            flashControl.ScaleMode = 0;
            flashControl.SAlign = "B";
            flashControl.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);

            flashControl.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\KT600.swf");

            if (Manager.GetInstance().Car.Power() == 2)
            {
                flashControl.SetVariable("enterBTN", "1");
            }
            else
            {
                flashControl.SetVariable("enterBTN", "0");
            }

            InitData();

            listView1.Visible = false;
            listBox1.Visible = false;
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
        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            Console.WriteLine("K600Form e.command = " + e.command);
            Console.WriteLine("K600Form e.args = " + e.args);

            if(e.command == "Close")
            {
                this.Close();
            }
            else if(e.command == "dqgzm")
            {
                listBox1.Visible = true;
            }
            else if(e.command == "zyszl")
            {
                listView1.Visible = true;
            }
            else if(e.command == "Record")
            {

            }
            else if(e.command == "ESC")
            {
                listView1.Visible = false;
                listBox1.Visible = false;
            }

        }

        private void InitData()
        {
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

                this.listView1.Items.Add(lvi);
            }

            rows = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo").Select();

            for (int i = 0; i < rows.Length; i++)
            {

                if (rows[i]["Choice"].ToString() == "选择")
                {

                    string id = (string)rows[i]["DTID"];
                    string value = (string)rows[i]["DTC"];

                    this.listBox1.Items.Add(id + " " + value);
                }
            }
        }

        private void titlePicture_Click(object sender, EventArgs e)
        {

        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.Visible == false)
            {
                return;
            }

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                {
                    string value = listView1.Items[i].SubItems[0].Text.ToString().Trim();
                    value = value == "√" ? "" : "√";
                    listView1.Items[i].SubItems[0].Text = value;
                    break;
                }
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }

            e.DrawBackground();
            Brush mybsh = Brushes.Black;
            // 焦点框
            e.DrawFocusRectangle();
            //文本 
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
        }

    }
}

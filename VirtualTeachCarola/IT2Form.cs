using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {
            pictureBox1.Load(System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\zdy_title.jpg");
            axShockwaveFlash1.DisableLocalSecurity();
            axShockwaveFlash1.ScaleMode = 0;
            axShockwaveFlash1.SAlign = "B";
            axShockwaveFlash1.Dock = DockStyle.Bottom;

            axShockwaveFlash1.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            axShockwaveFlash1.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(FlashFlashCall);

            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\zdy.swf");
        }

        void FlashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if(e.command == "hidezdy")
            {
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
            else if (e.command == "ZD" && e.args == "FDJ")
            {

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
            DTCListBox.Visible = true;
            DTCListBox.Items.Clear();
        }

        private void ShowDLIST()
        {

        }

        private void HideControl()
        {
            DTCListBox.Visible = false;

        }

        private void DTCListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

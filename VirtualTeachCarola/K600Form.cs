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
            titlePicture.Load(System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\k600.png");
            flashControl.DisableLocalSecurity();
            flashControl.Dock = DockStyle.Fill;
            flashControl.ScaleMode = 2;
            flashControl.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            flashControl.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(FlashFlashCall);

            flashControl.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\KT600.swf");
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
            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);

            if(e.command == "Close")
            {
                this.Close();
            }
        }

        private void titlePicture_Click(object sender, EventArgs e)
        {

        }

        void FlashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }
    }
}

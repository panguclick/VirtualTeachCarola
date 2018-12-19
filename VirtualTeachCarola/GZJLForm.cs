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
    public partial class GZJLForm : Form
    {
        private User mUser = null;
        private Car mCar = null;

        public GZJLForm()
        {
            InitializeComponent();
        }

        internal User MUser { get => mUser; set => mUser = value; }
        internal Car MCar { get => mCar; set => mCar = value; }

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {
            axShockwaveFlash1.DisableLocalSecurity();
            axShockwaveFlash1.ScaleMode = 2;
            axShockwaveFlash1.Dock = DockStyle.Fill;

            axShockwaveFlash1.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);

            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\gzjl.swf");

            Init();
        }
        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if(e.command == "quit")
            {
                this.Close();
            }
        }

        private void Init()
        {
            labelXueHao.Text = mUser.LoginID;
            labelName.Text = mUser.LoginName;
            labelSex.Text = mUser.Sex;
            labelClass.Text = mUser.ClassName;
            labelDate.Text = DateTime.Now.ToString("Y");
            labelCarType.Text = mCar.CarType;
            labelCarCode.Text = mCar.CarCode;
            labelEnginType.Text = mCar.EnginType;

            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM SubmitReport WHERE TestID = '-2018121918617' ORDER BY ID");
            DataRow[] rows = dataTable.Select();

            for(int i = 0; i < rows.Length; i++)
            {
                string content = ((DateTime)rows[i]["ID"]).ToString("d") + " ";

                if(rows[i]["Ename"] != "")
                {
                    content += " " + rows[i]["Ename"];
                }

                if (rows[i]["Oper"] != "")
                {
                    content += " " + rows[i]["Oper"];
                }

                if (rows[i]["SubMit"] != "")
                {
                    content += " " + rows[i]["SubMit"];
                }

                if(rows[i]["wgcz"] != null && rows[i]["wgcz"].ToString() == "1")
                {
                    content += " 违规操作";
                }

                listBox1.Items.Add(content);
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            string content = ((ListBox)sender).Items[e.Index].ToString();

            e.DrawBackground();
            Brush mybsh = Brushes.Black;

            if (content.Contains("违规操作"))
            {
                mybsh = Brushes.Red;
            }

            // 焦点框
            e.DrawFocusRectangle();
            //文本 
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
        }
    }
}

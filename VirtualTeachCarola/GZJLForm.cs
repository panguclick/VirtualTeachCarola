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
    public partial class GZJLForm : Form
    {
        public GZJLForm()
        {
            InitializeComponent();
        }

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
            labelXueHao.Text = Manager.GetInstance().User.HttpStudent.Data.StuId;
            labelName.Text = Manager.GetInstance().User.HttpStudent.Data.StuName;
            labelSex.Text = Manager.GetInstance().User.HttpStudent.Data.GetSex();
            labelClass.Text = Manager.GetInstance().User.HttpStudent.Data.ClassNum;
            labelDate.Text = DateTime.Now.ToString("Y");
            labelCarType.Text = Manager.GetInstance().Car.CarType;
            labelCarCode.Text = Manager.GetInstance().Car.CarCode;
            labelEnginType.Text = Manager.GetInstance().Car.EnginType;

            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM SubmitReport WHERE TestID = '"+ Manager.GetInstance().User.PracticID +"' ORDER BY ID");
            DataRow[] rows = dataTable.Select();

            for(int i = 0; i < rows.Length; i++)
            {
                string content = ((DateTime)rows[i]["ID"]).ToString("d");

                if (rows[i]["wgcz"] != null && rows[i]["wgcz"].ToString() == "1")
                {
                    content += " 违规操作:";
                }

                if (rows[i]["Ename"].ToString() != "")
                {
                    content += " " + rows[i]["Ename"];
                }

                if (rows[i]["Oper"].ToString() != "")
                {
                    content += " " + rows[i]["Oper"];
                }

                if (rows[i]["SubMit"].ToString() != "")
                {
                    content += " " + rows[i]["SubMit"];
                }

                listBox1.Items.Add(content);
            }
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if(e.Index == -1)
            {
                return;
            }

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

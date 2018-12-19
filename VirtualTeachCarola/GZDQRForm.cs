using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace VirtualTeachCarola
{

    public partial class GZDQRForm : Form
    {
        private Button mButton = new Button();
        private User mUser = null;

        internal User MUser { get => mUser; set => mUser = value; }

        public GZDQRForm()
        {
            InitializeComponent();
        }

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {
            axShockwaveFlash1.DisableLocalSecurity();
            axShockwaveFlash1.ScaleMode = 2;
            axShockwaveFlash1.Dock = DockStyle.Fill;

            axShockwaveFlash1.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(FlashFlashCommand);
            axShockwaveFlash1.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(FlashFlashCall);

            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\gzdqr.swf");

            Init();
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);

            if(e.command == "quit")
            {
                this.Close();
            }
        }

        private void Init()
        {
            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo ORDER BY GZID");
            DataRow[] dataRows = dataTable.Select();

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  

            for (int i = 0; i < dataRows.Length; i++)   //添加10行数据  
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add((string)dataRows[i]["DTID"]);
                lvi.SubItems.Add((string)dataRows[i]["EName"]);
                lvi.SubItems.Add((string)dataRows[i]["GZName"]);
                lvi.SubItems.Add((string)dataRows[i]["XF"]);
                this.listView1.Items.Add(lvi);

                //ListViewItem[] lvs = new ListViewItem[1];
                //lvs[0] = new ListViewItem(new string[] { "", (string)dataRows[i]["DTID"], (string)dataRows[i]["EName"], (string)dataRows[i]["GZName"], ""});
                //this.listView1.Items.AddRange(lvs);

                //mButton.Visible = true;
                //mButton.Text = (string)dataRows[i]["XF"];
                //mButton.Click += this.button_Click;
                //mButton.Size = new Size(this.listView1.Items[i].SubItems[4].Bounds.Width, this.listView1.Items[i].SubItems[4].Bounds.Height);
                //listView1.Controls.Add(mButton);
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        void FlashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button_Click(object sender, EventArgs e)
        {

        }

        private void Mouse_DoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = listView1.GetItemAt(e.X, e.Y);

            if(lvi.Checked)
            {
                string sql = "insert into RecordOper (OPeration,TestID,OperTime) values ('" 
                    + lvi.SubItems[3].Text + "','" 
                    + mUser.PracticID + "','" 
                    + DateTime.Now.ToLocalTime().ToString() + "')";

                AccessHelper.GetInstance().ExcuteSql(sql);
            }
        }
    }
}

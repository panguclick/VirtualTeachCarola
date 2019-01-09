using System;
using System.Data;
using System.Windows.Forms;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{

    public partial class GZDQRForm : Form
    {
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

            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\gzdqr.swf");

            Init();
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);

            if(e.command == "quit")
            {
                Close();
            }
        }

        private void Init()
        {
            DataTable dataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo ORDER BY GZID");
            DataRow[] dataRows = dataTable.Select();

            listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  

            for (int i = 0; i < dataRows.Length; i++)   //添加10行数据  
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add((string)dataRows[i]["DTID"]);
                lvi.SubItems.Add((string)dataRows[i]["EName"]);
                lvi.SubItems.Add((string)dataRows[i]["GZName"]);
                lvi.SubItems.Add((string)dataRows[i]["XF"]);
                lvi.Tag = (int)dataRows[i]["GZID"];

                if (Manager.GetInstance().SelectSubjects.Contains((int)dataRows[i]["GZID"]))
                {
                    lvi.Checked = true;
                }

                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem lvi = e.Item;

            if (lvi.Checked)
            {

                if (Manager.GetInstance().SelectSubjects.Count >= Manager.GetInstance().SubjectRows.Length)
                {
                    lvi.Checked = false;

                    MessageBox.Show("答案个数已超过故障数", "虚拟仿真教学-卡罗拉", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                string sql = "insert into RecordOper (OPeration,TestID,OperTime) values ('"
                    + lvi.SubItems[3].Text + "','"
                    + Manager.GetInstance().User.PracticID + "','"
                    + DateTime.Now.ToLocalTime().ToString() + "')";

                AccessHelper.GetInstance().ExcuteSql(sql);

                Manager.GetInstance().SelectSubjects.Add((int)lvi.Tag);

            }
            else
            {
                Manager.GetInstance().SelectSubjects.Remove((int)lvi.Tag);
            }
        }
    }
}

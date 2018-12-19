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

namespace VirtualTeachCarola
{
    public partial class SetGZForm : Form
    {
        DataTable mDataTable = null;
        public SetGZForm()
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

            axShockwaveFlash1.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\setgz.swf");

            Init();
        }

        private void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);

            if (e.command == "quit")
            {
                DialogResult dr;
                dr = MessageBox.Show("您真的要退出系统吗？", "虚拟仿真教学-卡罗拉", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    AccessHelper.GetInstance().Release();
                    System.Environment.Exit(0);
                }
                else
                {
                    this.Close();
                }
            }
            else if(e.command == "Login")
            {
                this.Close();
            }
        }

        void FlashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void Init()
        {
            mDataTable = AccessHelper.GetInstance().GetDataTableFromDB("SELECT * FROM GzInfo ORDER BY GZID");
            DataTable dataTable = AccessHelper.GetDistinctTable(mDataTable, "EName");
            DataRow[] rows = dataTable.Select();

            TreeNode rootNode = new TreeNode("发动机");
            this.treeView1.Nodes.Add(rootNode);

            for(int i = 0; i < rows.Length; i++)
            {
                rootNode.Nodes.Add(new TreeNode((string)rows[i]["EName"]));
            }

            this.treeView1.ExpandAll();

            SelectList1(rootNode.Nodes[0].Text);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Text != "发动机")
            {
                SelectList1(e.Node.Text);
            }
        }

        private void SelectList1(string nodeText)
        {
            listBox1.Items.Clear();

            DataRow[] rows = mDataTable.Select("EName = '" + nodeText + "'");

            if(rows.Length == 0)
            {
                return;
            }

            for(int i = 0; i < rows.Length; i++)
            {
                listBox1.Items.Add((string)rows[i]["DTC"]);
            }


            SelectList2((string)rows[0]["DTC"]);
        }

        private void SelectList2(string nodeText)
        {
            listBox2.Items.Clear();

            DataRow[] rows = mDataTable.Select("DTC = '" + nodeText + "'");

            if (rows.Length == 0)
            {
                return;
            }

            for (int i = 0; i < rows.Length; i++)
            {
                string content = "[" + (string)rows[i]["Choice"] + "] " + (string)rows[i]["GZName"];
                listBox2.Items.Add(content);
            }
        }

        private void ListBox1_SelectChanged(object sender, EventArgs e)
        {
            SelectList2((string)((ListBox)sender).SelectedItem);
        }

        private void ListBox2_DoubleClick(object sender, MouseEventArgs e)
        {
            string v = (string)((ListBox)sender).SelectedItem;
            string[] sArray = Regex.Split(v, " ", RegexOptions.IgnoreCase);

            DataRow[] rows = mDataTable.Select("GZName = '" + sArray[1] + "'");
            if((string)rows[0]["Choice"] == "未选择")
            {
                rows[0]["Choice"] = "选择";
            }
            else
            {
                rows[0]["Choice"] = "未选择";
            }

            string sql = "UPDATE GzInfo SET Choice ='" + (string)rows[0]["Choice"] + "' WHERE GZID =" + rows[0]["GZID"];

            AccessHelper.GetInstance().ExcuteSql(sql);

            int index = this.listBox2.IndexFromPoint(e.Location);
            this.listBox2.Items[index] = "[" + (string)rows[0]["Choice"] + "] " + (string)rows[0]["GZName"];

        }

        private void ListBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            string content = ((ListBox)sender).Items[e.Index].ToString();

            e.DrawBackground();
            Brush mybsh = Brushes.Black;

            if (content.Contains("[选择]"))
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

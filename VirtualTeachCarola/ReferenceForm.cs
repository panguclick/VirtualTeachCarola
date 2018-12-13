using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace VirtualTeachCarola
{

    public partial class ReferenceForm : Form
    {
        XmlElement xmlElement;
        XmlNodeList xmlNodeList;
        public ReferenceForm()
        {
            InitializeComponent();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.IO.Directory.GetCurrentDirectory() + "\\Data\\book\\Reference.xml");
            xmlElement = xmlDoc.DocumentElement;//取到根结点

        }

        private void ReferenceForm_Load(object sender, EventArgs e)
        {
            BooklistView.BorderStyle = BorderStyle.None;
            flashContrl.DisableLocalSecurity();
            flashContrl.Dock = DockStyle.Fill;
            flashContrl.ScaleMode = 2;
            flashContrl.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\book.swf");
            flashContrl.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(flashFlashCall);
            flashContrl.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(flashFlashCommand);
            selectTab("caidan1");
        }

        private void ReferenceForm_DoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = BooklistView.HitTest(e.X, e.Y);
            excuteSelect(info.Item.Index);
        }

        void flashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void flashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            // We only want to react if we got our command
            if (e.command == "fanhui" || e.command == "duichu")
            {
                this.Close();
            }
            else if (e.command == "caidan1" 
                || e.command == "caidan2" 
                || e.command == "caidan3" 
                || e.command == "caidan4" 
                || e.command == "caidan5")
            {
                selectTab(e.command); 
            }

        }

        private void excuteSelect(int index)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + xmlNodeList[index].Attributes.GetNamedItem("url").Value;
            Process.Start(path);
        }

        private void selectTab(string command)
        {
            this.BooklistView.Clear();

            xmlNodeList = xmlElement.SelectNodes(command);

            ColumnHeader column = this.BooklistView.Columns.Add("", this.BooklistView.Width - 10, HorizontalAlignment.Left);

            this.BooklistView.View = View.Details;
            this.BooklistView.BeginUpdate();

            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                this.BooklistView.Items.Add("", xmlNodeList[i].Attributes.GetNamedItem("label").Value, 0);
            }

            this.BooklistView.EndUpdate();
        }
    }
}

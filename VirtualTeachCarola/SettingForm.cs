using System;
using System.Windows.Forms;

namespace VirtualTeachCarola
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            flashContrl.DisableLocalSecurity();
            flashContrl.Dock = DockStyle.Fill;
            flashContrl.ScaleMode = 2;
            flashContrl.LoadMovie(0, System.IO.Directory.GetCurrentDirectory() + "\\Data\\Surface\\param.swf");
            flashContrl.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(flashFlashCall);
            flashContrl.FSCommand += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEventHandler(flashFlashCommand);
        }

        void flashFlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
        }

        private void flashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            // We only want to react if we got our command
            if ((e.command == "SZ" && e.args == "FH") || e.command == "Quit")
            {
                this.Close();
            }
    
        }
    }
}

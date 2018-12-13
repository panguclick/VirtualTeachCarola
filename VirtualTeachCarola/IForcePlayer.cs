using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualTeachCarola
{
    public class IForcePlayer : AxShockwaveFlash
    {
        const int WM_RBUTTONDOWN = 0x0204;

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == WM_RBUTTONDOWN)
            //{
            //    m.Result = IntPtr.Zero;
            //    return;
            //}
            base.WndProc(ref m);
        }
    }
}

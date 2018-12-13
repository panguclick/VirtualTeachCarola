using System.Windows.Forms;

namespace System
{
    internal class MouseEventHandler
    {
        private Action<object, MouseEventArgs> referenceForm_DoubleClick;

        public MouseEventHandler(Action<object, MouseEventArgs> referenceForm_DoubleClick)
        {
            this.referenceForm_DoubleClick = referenceForm_DoubleClick;
        }
    }
}
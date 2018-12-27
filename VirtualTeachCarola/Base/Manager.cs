using AxShockwaveFlashObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{
    class Manager
    {
        private static Manager manager = null;

        public static Manager GetInstance()
        {
            if (manager == null)
            {
                manager = new Manager();
            }

            return manager;
        }

        private MainForm mMainForm = null;

        public MainForm MMainForm { get => mMainForm; set => mMainForm = value; }

        public void RegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand += eventFun;
        }

        public void UnRegisterEvent(_IShockwaveFlashEvents_FSCommandEventHandler eventFun)
        {
            mMainForm.LoadFlash.FSCommand -= eventFun;
        }
    }
}

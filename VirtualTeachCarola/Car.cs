using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola
{
    class Car
    {
        string accorrun = "off";
        int breakType = 0;
        int speed = 0;
        string gearshift = "P";
        int isLine = 0;
        int isBatty = 0;
        int isTemp = 0;

        string carType = "";
        string carCode = "";
        string enginType = "";
        int enginRunTime = 0;

        System.Timers.Timer timer = null;

        public string Accorrun { get => accorrun; set => accorrun = value; }
        public int BreakType { get => breakType; set => breakType = value; }
        public int Speed { get => speed; set => speed = value; }
        public int IsLine { get => isLine; set => isLine = value; }
        public int IsBatty { get => isBatty; set => isBatty = value; }
        public int IsTemp { get => isTemp; set => isTemp = value; }
        public string Gearshift { get => gearshift; set => gearshift = value; }
        public string CarType { get => carType; set => carType = value; }
        public string CarCode { get => carCode; set => carCode = value; }
        public string EnginType { get => enginType; set => enginType = value; }
        public int EnginRunTime { get => enginRunTime; set => enginRunTime = value; }

        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if (e.command == "power")
            {
                accorrun = e.args;
                UpdateEnginRunTime();
            }
            else if(e.command == "sc")
            {
                breakType = int.Parse(e.args);
            }
            else if(e.command == "youmeng")
            {
                speed = int.Parse(e.args);
            }
            else if(e.command == "speed")
            {
                gearshift = e.args;
            }
            if (e.args == "ZXJC")
            {
                isLine = 1;
            }
            else if (e.args == "JC")
            {
                isLine = 0;
            }
        }

        public int Power()
        {
            int res = 0;
            if (accorrun.Equals("off"))
            {
                res = 0;
            }
            else if (accorrun.Equals("acc"))
            {
                res = 0;
            }
            else if (accorrun.Equals("on"))
            {
                res = 1;
            }
            else if (accorrun.Equals("start"))
            {
                res = 2;
            }

            return res;
        }

        private void UpdateEnginRunTime()
        {
            if (accorrun.Equals("start"))
            {
                if(timer == null)
                {
                    timer = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒；
                    timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                    timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimerMethod);//到达时间的时候执行事件
                }

                timer.Start();

            }
            else if(accorrun == "off" || accorrun == "acc")
            {
                if(timer != null)
                {
                    timer.Stop();
                }
                enginRunTime = 0;
            }
        }

        private void UpdateTimerMethod(object source, System.Timers.ElapsedEventArgs e)
        {
            enginRunTime++;
        }
    }
}

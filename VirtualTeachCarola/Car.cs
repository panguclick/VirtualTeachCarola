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

        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            if (e.command == "power")
            {
                accorrun = e.args;
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
    }
}

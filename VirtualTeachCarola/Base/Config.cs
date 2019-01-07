using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{
    class Config
    {
        private string http = "";
        private string temp = "0";
        private string pressure = "0";

        public string Http { get => http; set => http = value; }
        public string Temp { get => temp; set => temp = value; }
        public string Pressure { get => pressure; set => pressure = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    class User
    {
        string practicID = "";
        string loginID = "";
        string loginName = "";
        string loginPws = "";
        string mode = "shixun";
        HttpStudent httpStudent = new HttpStudent();

        public string PracticID { get => practicID; set => practicID = value; }
        public string LoginName { get => loginName; set => loginName = value; }
        public string LoginPws { get => loginPws; set => loginPws = value; }
        public string LoginID { get => loginID; set => loginID = value; }
        public string Mode { get => mode; set => mode = value; }
        internal HttpStudent HttpStudent { get => httpStudent; set => httpStudent = value; }
    }
}

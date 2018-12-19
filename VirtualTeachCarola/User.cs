using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola
{
    class User
    {
        string practicID = "";
        string loginID = "";
        string loginName = "";
        string loginPws = "";
        string sex = "";
        string className = "";

        public string PracticID { get => practicID; set => practicID = value; }
        public string LoginName { get => loginName; set => loginName = value; }
        public string LoginPws { get => loginPws; set => loginPws = value; }
        public string LoginID { get => loginID; set => loginID = value; }
        public string Sex { get => sex; set => sex = value; }
        public string ClassName { get => className; set => className = value; }
    }
}

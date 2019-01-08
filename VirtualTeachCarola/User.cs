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
        string mode = "shixun";
        HttpStudent httpStudent = new HttpStudent();
        HttpExam httpExam = new HttpExam();

        public string PracticID { get => practicID; set => practicID = value; }
        public string Mode { get => mode; set => mode = value; }
        internal HttpStudent HttpStudent { get => httpStudent; set => httpStudent = value; }
        internal HttpExam HttpExam { get => httpExam; set => httpExam = value; }
    }
}

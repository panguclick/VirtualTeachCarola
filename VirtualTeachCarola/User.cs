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
        int wgczCount = 0;

        public string PracticID { get => practicID; set => practicID = value; }
        public string Mode { get => mode; set => mode = value; }
        public int WgczCount { get => wgczCount; set => wgczCount = value; }
        internal HttpStudent HttpStudent { get => httpStudent; set => httpStudent = value; }
        internal HttpExam HttpExam { get => httpExam; set => httpExam = value; }

        public void AddWgczCount()
        {
            ++WgczCount;
        }
    }
}

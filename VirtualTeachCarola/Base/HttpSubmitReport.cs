using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{
    class HttpSubmitReport
    {
        private string stuId = "";
        private int testId = 0;
        private List<int> answers = new List<int>();
        private int wgczCount = 0;
        private List<string> contents = new List<string>();

        public string StuId { get => stuId; set => stuId = value; }
        public int TestId { get => testId; set => testId = value; }
        public List<int> Answers { get => answers; set => answers = value; }
        public List<string> Contents { get => contents; set => contents = value; }
        public int WgczCount { get => wgczCount; set => wgczCount = value; }
    }
}

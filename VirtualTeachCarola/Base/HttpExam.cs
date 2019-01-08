using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{
    class HttpExam
    {
        private int tId = 0;
        private DateTime tStartTime;
        private DateTime tEndTime;
        private string testContent;

        public int TId { get => tId; set => tId = value; }
        public DateTime TStartTime { get => tStartTime; set => tStartTime = value; }
        public DateTime TEndTime { get => tEndTime; set => tEndTime = value; }
        public string TestContent { get => testContent; set => testContent = value; }
    }
}

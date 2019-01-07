using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//{"code":200,"data":{"id":1,"stuId":"1801","stuName":"admin","stuSex":1,"stuBorn":"2019-01-15 00:00:00","stuPwd":""}}

namespace VirtualTeachCarola.Base
{
    class HttpStudent
    {
        private int code = 0;
        private Student data = new Student();

        public int Code { get => code; set => code = value; }
        public Student Data { get => data; set => data = value; }
    }
}

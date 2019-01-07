using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTeachCarola.Base
{

    class Student
    {
        private int id = 0;
        private string stuId = "";
        private string stuName = "";
        private int stuSex = 0;
        private DateTime stuBorn;
        private string stuPwd = "";
        private string classNum = "";

        public int Id { get => id; set => id = value; }
        public string StuId { get => stuId; set => stuId = value; }
        public string StuName { get => stuName; set => stuName = value; }
        public int StuSex { get => stuSex; set => stuSex = value; }
        public DateTime StuBorn { get => stuBorn; set => stuBorn = value; }
        public string StuPwd { get => stuPwd; set => stuPwd = value; }
        public string ClassNum { get => classNum; set => classNum = value; }

        public string GetSex()
        {
            return stuSex == 0 ? "男" : "女";
        }
    }
}

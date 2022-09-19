using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Ent
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime DoB { get; set; }
        public string strDoB { get; set; }
        public string NIC { get; set; }
        public int Title { get; set; }
        public string StrTitle { get; set; }
        public int Grade { get; set; }
    }
}

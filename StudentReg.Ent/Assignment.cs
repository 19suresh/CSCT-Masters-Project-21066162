using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Ent
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Subject Subject { get; set; }
        public int Year { get; set; }
        public int Grade { get; set; }
        public int Term { get; set; }
        public int SubjectId { get; set; }
    }
}

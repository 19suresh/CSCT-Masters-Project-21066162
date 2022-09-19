using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Ent
{
    public class AssingmentResult
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int AssingmentId { get; set; }
        public int Mark { get; set; }
        public int Grade { get; set; }
        public string Remarks { get; set; }
        public int EvaluatedBy { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }

        public string AssingmnetName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

    }
}

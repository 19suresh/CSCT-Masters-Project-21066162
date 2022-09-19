using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Ent
{
    public class SRegResponse
    {
        public bool ResStatus { get; set; }
        public string ResMsg { get; set; }
        public int ResCode { get; set; }
        public object ResData1 { get; set; }
        public object ResData2 { get; set; }
        public object ResData3 { get; set; }
        public Exception Exception { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentReg.Dal.EntityDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_EXAM_RESULT
    {
        public int Id { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public Nullable<int> ExamId { get; set; }
        public Nullable<int> Mark { get; set; }
        public Nullable<int> Grade { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> EvaluatedBy { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Term { get; set; }
        public string RegNo { get; set; }
    
        public virtual TBL_EXAM TBL_EXAM { get; set; }
        public virtual TBL_SUBJECT TBL_SUBJECT { get; set; }
    }
}

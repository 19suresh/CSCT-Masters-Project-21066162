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
    
    public partial class TBL_EXAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_EXAM()
        {
            this.TBL_EXAM_RESULT = new HashSet<TBL_EXAM_RESULT>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Term { get; set; }
        public Nullable<int> Grade { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXAM_RESULT> TBL_EXAM_RESULT { get; set; }
    }
}

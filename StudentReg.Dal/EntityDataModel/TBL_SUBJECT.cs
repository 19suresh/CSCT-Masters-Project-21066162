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
    
    public partial class TBL_SUBJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_SUBJECT()
        {
            this.TBL_ASSIGNMENT = new HashSet<TBL_ASSIGNMENT>();
            this.TBL_EXAM_RESULT = new HashSet<TBL_EXAM_RESULT>();
            this.TBL_ASSINGMENT_RESULT = new HashSet<TBL_ASSINGMENT_RESULT>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Grade { get; set; }
        public Nullable<int> Term { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ASSIGNMENT> TBL_ASSIGNMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXAM_RESULT> TBL_EXAM_RESULT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ASSINGMENT_RESULT> TBL_ASSINGMENT_RESULT { get; set; }
    }
}

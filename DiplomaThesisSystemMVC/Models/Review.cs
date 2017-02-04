//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomaThesisSystemMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Review()
        {
            this.Student = new HashSet<Student>();
        }
    
        public int ID { get; set; }
        public int DiplomaThesisID { get; set; }
        public string ReviewerID { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public Nullable<int> Grade { get; set; }
    
        public virtual DiplomaThesis DiplomaThesis { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Student { get; set; }
    }
}

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
    
    public partial class Defence
    {
        public int ID { get; set; }
        public string StudentID { get; set; }
        public int ThesisDefenceCommisionID { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual ThesisDefenceCommision ThesisDefenceCommision { get; set; }
    }
}
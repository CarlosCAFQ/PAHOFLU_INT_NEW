//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paho
{
    using System;
    using System.Collections.Generic;
    
    public partial class FluCase
    {
        public FluCase()
        {
            this.CaseLabTests = new HashSet<CaseLabTest>();
        }
    
        public int ID { get; set; }
        public long HospitalID { get; set; }
        public System.DateTime HospitalDate { get; set; }
        public System.DateTime RegDate { get; set; }
        public string FName1 { get; set; }
        public string FName2 { get; set; }
        public string LName1 { get; set; }
        public string LName2 { get; set; }
        public string NationalId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> AMeasure { get; set; }
        public Nullable<int> AgeGroup { get; set; }
        public int Gender { get; set; }
        public Nullable<int> UserID { get; set; }
        public System.DateTime InsertDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public Nullable<long> InstID { get; set; }
        public Nullable<long> CaseID { get; set; }
        public string Sex { get; set; }
        public string AgeM { get; set; }
        public string Users { get; set; }
        public Nullable<System.DateTime> OldDate { get; set; }
    
        public virtual CaseGEO CaseGEO { get; set; }
        public virtual CaseHospital CaseHospital { get; set; }
        public virtual CaseLab CaseLab { get; set; }
        public virtual ICollection<CaseLabTest> CaseLabTests { get; set; }
        public virtual CaseRisk CaseRisk { get; set; }
        public virtual Institution Institution { get; set; }
    }
}

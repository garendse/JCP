//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JCP
{
    using System;
    using System.Collections.Generic;
    
    public partial class j_supplier_branches
    {
        public string id { get; set; }
        public string supplier_id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> lat { get; set; }
        public Nullable<decimal> lgn { get; set; }
        public string contact_person { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string address_line_3 { get; set; }
        public string postal { get; set; }
    
        public virtual j_suppliers supplier { get; set; }
    }
}

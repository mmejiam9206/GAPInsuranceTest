//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAPInsurancesTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Policy
    {
        public int policy_id { get; set; }
        public string policy_name { get; set; }
        public string policy_description { get; set; }
        public int coverage_type_id { get; set; }
        public double coverage_percent { get; set; }
        public decimal policy_price { get; set; }
        public int risk_type_id { get; set; }
    
        public virtual CoverageType CoverageType { get; set; }
        public virtual RiskType RiskType { get; set; }
    }
}

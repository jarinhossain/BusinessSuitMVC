//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessSuitMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Obd_Request
    {
        public int Id { get; set; }
        public Nullable<int> Obd_Bulk_Id { get; set; }
        public Nullable<int> CDR_Obd_Id { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Source_Id { get; set; }
        public Nullable<int> Retry_Count { get; set; }
        public int Call_Duration { get; set; }
        public int Billsec { get; set; }
        public string Uniqueid { get; set; }
        public string Lastdata { get; set; }
        public Nullable<bool> Is_Obd_Generated { get; set; }
    
        public virtual CDR_Obd CDR_Obd { get; set; }
        public virtual Obd_Bulk Obd_Bulk { get; set; }
    }
}
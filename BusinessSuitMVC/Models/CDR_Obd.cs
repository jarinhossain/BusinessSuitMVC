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
    
    public partial class CDR_Obd
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Obd_Request_Id { get; set; }
        public Nullable<int> Client_Id { get; set; }
        public string Context { get; set; }
        public string Last_App { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Start_Time { get; set; }
        public Nullable<System.DateTime> End_Time { get; set; }
        public string Disposition { get; set; }
        public Nullable<int> Bill_Sec { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Remarks { get; set; }
        public string Server { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
    }
}

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
        public CDR_Obd()
        {
            this.Obd_Request = new HashSet<Obd_Request>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Obd_Request_Id { get; set; }
        public string Context { get; set; }
        public System.DateTime Start_Time { get; set; }
        public System.DateTime Answer_Time { get; set; }
        public System.DateTime End_Time { get; set; }
        public string Clid { get; set; }
        public string Src { get; set; }
        public string Dst { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
        public string Disposition { get; set; }
        public int Amaflags { get; set; }
        public string Uniqueid { get; set; }
        public string Last_Data { get; set; }
        public string Last_App { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
    
        public virtual ICollection<Obd_Request> Obd_Request { get; set; }
    }
}

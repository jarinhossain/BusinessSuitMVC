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
    
    public partial class Number
    {
        public int Id { get; set; }
        public Nullable<int> Number1 { get; set; }
        public Nullable<int> Operator_Id { get; set; }
        public Nullable<int> Sms_Tried { get; set; }
        public Nullable<int> Sms_Succeed { get; set; }
        public Nullable<System.DateTime> Last_Succeeded_Sms_Date { get; set; }
        public Nullable<int> Obd_Tried { get; set; }
        public Nullable<int> Obd_Succeed { get; set; }
        public Nullable<System.DateTime> Last_Succeeded_Obd_Date { get; set; }
        public Nullable<int> Address_Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public Nullable<int> Number_Id { get; set; }
        public Nullable<int> Source_Id { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
    
        public virtual Source Source { get; set; }
        public virtual Operator Operator { get; set; }
    }
}
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
    
    public partial class Token
    {
        public int Id { get; set; }
        public string Token_Data { get; set; }
        public string Display_Name { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
    }
}

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
    
    public partial class Permission_User
    {
        public int Id { get; set; }
        public Nullable<int> Permission_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<int> User_Type { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual User_Login User_Login { get; set; }
    }
}
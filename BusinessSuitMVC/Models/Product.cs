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
    
    public partial class Product
    {
        public Product()
        {
            this.Online_Order_Detalis = new HashSet<Online_Order_Detalis>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Is_Online { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<int> Is_Deleted { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
    
        public virtual ICollection<Online_Order_Detalis> Online_Order_Detalis { get; set; }
    }
}

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
    
    public partial class Order
    {
        public Order()
        {
            this.Offline_Order_Detalis = new HashSet<Offline_Order_Detalis>();
            this.Online_Order_Detalis = new HashSet<Online_Order_Detalis>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Is_Package { get; set; }
        public Nullable<double> Total_Paid { get; set; }
        public Nullable<double> Total_Bill { get; set; }
        public string order_status { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<int> Updated_By { get; set; }
    
        public virtual ICollection<Offline_Order_Detalis> Offline_Order_Detalis { get; set; }
        public virtual ICollection<Online_Order_Detalis> Online_Order_Detalis { get; set; }
    }
}

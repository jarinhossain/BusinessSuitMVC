//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessSuitMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Offline_Order_Details = new HashSet<Offline_Order_Details>();
            this.Online_Order_Details = new HashSet<Online_Order_Details>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> Is_Package { get; set; }
        public Nullable<double> Total_Paid { get; set; }
        public Nullable<double> Total_Bill { get; set; }
        public Nullable<int> Order_Status { get; set; }
        public Nullable<int> Client_Id { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
    
        public virtual Client_List Client_List { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offline_Order_Details> Offline_Order_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Online_Order_Details> Online_Order_Details { get; set; }
    }
}

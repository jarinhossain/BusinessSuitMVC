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
    
    public partial class Client_List
    {
        public Client_List()
        {
            this.User_Login = new HashSet<User_Login>();
            this.Client_Inventory = new HashSet<Client_Inventory>();
            this.Obd_Bulk = new HashSet<Obd_Bulk>();
            this.Obd_Ward_Details = new HashSet<Obd_Ward_Details>();
        }
    
        public int Id { get; set; }
        public string Counsilor_Name { get; set; }
        public string Bangla_Name { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public Nullable<int> ward { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Is_Elected { get; set; }
        public Nullable<int> Client_Type { get; set; }
        public Nullable<bool> Image { get; set; }
        public Nullable<int> Present_Position { get; set; }
        public Nullable<int> District_Id { get; set; }
    
        public virtual ICollection<User_Login> User_Login { get; set; }
        public virtual ICollection<Client_Inventory> Client_Inventory { get; set; }
        public virtual ICollection<Obd_Bulk> Obd_Bulk { get; set; }
        public virtual ICollection<Obd_Ward_Details> Obd_Ward_Details { get; set; }
        public virtual District District { get; set; }
    }
}

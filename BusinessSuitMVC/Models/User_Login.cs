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
    
    public partial class User_Login
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Login()
        {
            this.Client_Inventory = new HashSet<Client_Inventory>();
            this.Permission_User = new HashSet<Permission_User>();
            this.Role_User = new HashSet<Role_User>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Role_Id { get; set; }
        public Nullable<int> User_Profile_Id { get; set; }
        public Nullable<bool> Is_Client { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Ceated_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<int> Is_Deleted { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
        public Nullable<int> Client_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_Inventory> Client_Inventory { get; set; }
        public virtual Client_List Client_List { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permission_User> Permission_User { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role_User> Role_User { get; set; }
        public virtual User_Profile User_Profile { get; set; }
    }
}

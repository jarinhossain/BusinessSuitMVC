﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Client_List> Client_List { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Expense_Type> Expense_Type { get; set; }
        public DbSet<Notification_Obd_Bulk> Notification_Obd_Bulk { get; set; }
        public DbSet<Notification_Obd_Request> Notification_Obd_Request { get; set; }
        public DbSet<Notification_Sms_Bulk> Notification_Sms_Bulk { get; set; }
        public DbSet<Notification_Sms_Request> Notification_Sms_Request { get; set; }
        public DbSet<Offline_Order_Detalis> Offline_Order_Detalis { get; set; }
        public DbSet<Online_Order_Detalis> Online_Order_Detalis { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Permission_Role> Permission_Role { get; set; }
        public DbSet<Permission_User> Permission_User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion_Type> Promotion_Type { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_User> Role_User { get; set; }
        public DbSet<Role1> Roles1 { get; set; }
        public DbSet<Sippeer> Sippeers { get; set; }
        public DbSet<Slip_Formats> Slip_Formats { get; set; }
        public DbSet<Status_> Status_ { get; set; }
        public DbSet<User_Login> User_Login { get; set; }
        public DbSet<User_Profile> User_Profile { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voter_Slip_Image_Type> Voter_Slip_Image_Type { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<CDR_Instant> CDR_Instant { get; set; }
    }
}

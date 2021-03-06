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
    
    public partial class Offline_Order_Detalis
    {
        public int Id { get; set; }
        public Nullable<int> Order_Id { get; set; }
        public Nullable<int> Slip_Format_Id { get; set; }
        public Nullable<double> Price_Per_Piece { get; set; }
        public Nullable<int> Estimated_Voters { get; set; }
        public Nullable<int> Format_No { get; set; }
        public Nullable<System.DateTime> Delivery_Date { get; set; }
        public string Delivery_Type { get; set; }
        public Nullable<int> Free_Blank_Slip { get; set; }
        public Nullable<int> Paid_Blank_Slip { get; set; }
        public Nullable<bool> Sample_Slip_Image { get; set; }
        public string Slip_Content { get; set; }
        public Nullable<int> Voter_List_File { get; set; }
        public Nullable<bool> Is_Cd_Provided { get; set; }
        public Nullable<int> Voter_Slip_Image_Type { get; set; }
        public Nullable<bool> Center_Name_List_Image { get; set; }
        public string Center_Name_List { get; set; }
        public Nullable<int> Voter_List_Print { get; set; }
        public string Voter_List_Print_Comment { get; set; }
        public Nullable<double> C_Voter_List_Print_Price { get; set; }
        public Nullable<bool> Liflet_Image { get; set; }
        public Nullable<System.DateTime> Election_Date { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<int> Status { get; set; }
        public string Remarks { get; set; }
        public string Marka { get; set; }
        public Nullable<bool> Marka_Image { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Ceated_On { get; set; }
        public Nullable<int> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<int> Is_Deleted { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
        public string Download_Link { get; set; }
        public Nullable<bool> Can_Download { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Slip_Formats_ Slip_Formats_ { get; set; }
    }
}

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
    
    public partial class Source
    {
        public Source()
        {
            this.Numbers = new HashSet<Number>();
        }
    
        public int Id { get; set; }
        public string Contact_Name { get; set; }
        public string Company_Name { get; set; }
        public Nullable<int> Source_Type { get; set; }
        public Nullable<int> Ref_Id { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public Nullable<System.DateTime> Estimated_Book_Date { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> Division_Id { get; set; }
        public Nullable<int> District_Id { get; set; }
        public Nullable<bool> Image { get; set; }
        public Nullable<int> Ward { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> Created_On { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_On { get; set; }
        public Nullable<int> updated_By { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
        public Nullable<int> Deleted_By { get; set; }
        public Nullable<System.DateTime> Deleted_On { get; set; }
    
        public virtual ICollection<Number> Numbers { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLBorrow
    {
        public int BorrowID { get; set; }
        public Nullable<int> Book { get; set; }
        public Nullable<int> Member { get; set; }
        public Nullable<System.DateTime> BorrowDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
    
        public virtual TBLBook TBLBook { get; set; }
        public virtual TBLMember TBLMember { get; set; }
    }
}
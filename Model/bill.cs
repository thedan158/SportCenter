//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportCenter.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class bill
    {
        public int id { get; set; }
        public Nullable<decimal> totalmoney { get; set; }
        public int idBookingInfo { get; set; }
    
        public virtual bookingInfo bookingInfo { get; set; }
    }
}

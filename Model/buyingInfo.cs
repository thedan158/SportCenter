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
    

    public partial class buyingInfo:BaseViewModel
    {

        private int _id;
        public int id { get => _id; set { _id = value; OnPropertyChanged(); } }

        private Nullable <int> _idGood;
        public Nullable <int> idGood
        { get => _idGood; set { _idGood = value; OnPropertyChanged(); } }

        private Nullable <int> _quantity;
        public Nullable <int> quantity
        { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }

        private int _idBookingInfo;
        public int idBookingInfo { get => _idBookingInfo; set { _idBookingInfo = value; OnPropertyChanged(); } }

        private Nullable <decimal> _orderprice;
        public Nullable <decimal> orderprice { get => _orderprice; set { _orderprice = value; OnPropertyChanged(); } }


        public virtual bookingInfo bookingInfo { get; set; }
        public virtual good good { get; set; }
    }
}

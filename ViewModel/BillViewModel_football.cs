using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportCenter.ViewModel
{
    class BillViewModel_football: BillViewModel
    {
        private ObservableCollection<ListBill> _Listfield;
        public ObservableCollection<ListBill> List_Field_fb { get => _Listfield; set { _Listfield = value; OnPropertyChanged(); } }
        private ObservableCollection<ListBill> _ListBooking;
        public ObservableCollection<ListBill> List_booking_idfb { get => _ListBooking; set { _ListBooking = value; OnPropertyChanged(); } }

        private ObservableCollection<ListBill> _List_fieldwithBooking;
        public ObservableCollection<ListBill> List_fieldwwithBooking { get => _List_fieldwithBooking; set { _List_fieldwithBooking = value; OnPropertyChanged(); } }


        public ICommand Show_Pay { get; set; }



        public BillViewModel_football()
        {
            _List_fieldwithBooking = new ObservableCollection<ListBill>();
            Get_field();
            Get_booking();
            //Booking_ex(); 
            Show_Pay = new RelayCommand<object>((parameter) => true, (parameter) => Show_Payment());
        }

        //private void Booking_ex()
        //{
        //    foreach(var item in _Listfield)
        //    {
        //        ListBill temp = new ListBill();
        //        temp = item;
        //        _List_fieldwithBooking.Add(temp);
        //    }
        //    foreach (var item in _ListBooking)
        //    {
        //        ListBill temp = new ListBill();
        //        foreach (var item2 in _List_fieldwithBooking)
        //        {
        //            if (item.List_Booking.idField == item2.List_Field.id)
        //            {

        //            }
        //        }
        //    }
        //}

        private void Show_Payment()
        {
            PayMent_tem Pay = new PayMent_tem();
            Pay.Show();
        }

        private void Get_booking()
        {
            _ListBooking = new ObservableCollection<ListBill>();
            
            foreach(var item in List_Booking_idfb)
            {
                ListBill temp = new ListBill();
                temp = item;
                _ListBooking.Add(temp);
            }
            foreach(var item in _ListBooking)
            {
                ListBill temp = new ListBill();
                foreach(var item2 in _Listfield)
                {
                    if (item2.List_Field.id == item.List_Booking.idField)
                    {
                        temp.List_Field = new field();
                        temp.List_Booking = new bookingInfo();
                        temp.List_Booking.id = item.List_Booking.id;
                        temp.List_Booking.idField = item.List_Booking.idField;
                        temp.List_Booking.datePlay = item.List_Booking.datePlay;
                        temp.List_Booking.start_time = item.List_Booking.start_time;
                        temp.List_Booking.end_time = item.List_Booking.end_time;
                        temp.List_Field.name = item2.List_Field.name;
                        _List_fieldwithBooking.Add(temp);
                    }
                }
            }
        }

        private void Get_field()
        {
            _Listfield = new ObservableCollection<ListBill>();
            foreach(var item in List_field_fb)
            {
                ListBill temp = new ListBill();
                temp = item;
                _Listfield.Add(temp);
            }
        }
    }
}

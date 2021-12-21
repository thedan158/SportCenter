using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    public class BillViewModel : BaseViewModel
    {

        private ObservableCollection<ListBill> _ListBill;
        public ObservableCollection<ListBill> ListBill { get => _ListBill; set { _ListBill = value; OnPropertyChanged(); } }
        private ObservableCollection<ListBill> _Listbooking;
        public ObservableCollection<ListBill> ListBooking { get => _Listbooking; set { _Listbooking = value; OnPropertyChanged();} }
        protected List<ListBill> List_Booking_idfb;
        protected List<ListBill> List_Booking_idbk;
        protected List<ListBill> List_Booking_idvl;
        protected List<ListBill> List_field_fb;
        protected List<ListBill> List_field_bk;
        protected List<ListBill> List_field_vl;


        private ObservableCollection<ListBill> _ListBooking_soccer;
        public ObservableCollection<ListBill> ListBooking_soccer { get => _ListBooking_soccer; set { _ListBooking_soccer = value; OnPropertyChanged(); } }
        private ObservableCollection<ListBill> _ListBooking_Bk;
        public ObservableCollection<ListBill> ListBooking_Bk { get => _ListBooking_Bk; set { _ListBooking_Bk = value; OnPropertyChanged(); } }
        private ObservableCollection<ListBill> _ListBooking_VL;
        public ObservableCollection<ListBill> ListBooking_VL { get => _ListBooking_VL; set { _ListBooking_VL = value; OnPropertyChanged(); } }

        public ICommand Show_PaymentTemplate { get; set; }
        
        public BillViewModel()
        {


            _Listbooking = new ObservableCollection<ListBill>();
            _ListBill = new ObservableCollection<ListBill>();
            List_Booking_idfb = new List<ListBill>();
            List_Booking_idbk = new List<ListBill>();
            List_Booking_idvl = new List<ListBill>();
            List_field_fb = new List<ListBill>();
            List_field_bk = new List<ListBill>();
            List_field_vl = new List<ListBill>();
            Load_Listfield();
            Load_ListBooking();
            Device_ListBooking();


        }

        private void Load_Listfield()
        {
            var Temp_field = DataProvider.Ins.DB.fields;
            
            foreach (var item in Temp_field)
            {
                if (item.idType == 1)
                {
                    ListBill temp = new ListBill();
                    temp.List_Field = item;
                    List_field_fb.Add(temp);
                }
                if(item.idType == 2)
                {
                    ListBill temp = new ListBill();
                    temp.List_Field = item;
                    List_field_bk.Add(temp);
                }
                if (item.idType == 3)
                {
                    ListBill temp = new ListBill();
                    temp.List_Field = item;
                    List_field_vl.Add(temp);
                }
            }
        }

        private void Load_ListBooking()
        {
            var Temp_field = DataProvider.Ins.DB.fields;
            var Temp_booking = DataProvider.Ins.DB.bookingInfoes;
            foreach(var item_booking in Temp_booking)
            {
                if (Temp_booking == null)
                {
                    break;
                }
                else
                {
                    ListBill temp = new ListBill();
                    temp.List_Booking = item_booking;
                    _Listbooking.Add(temp);
                }
            }
            foreach(var item in _Listbooking)
            {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (item.List_Booking.idField != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    foreach(var item2 in Temp_field)
                    {
                        if (item2.id == item.List_Booking.idField)
                        {
                            if (item2.idType == 1)
                            {
                                ListBill temp = new ListBill();
                                temp = item;
                                List_Booking_idfb.Add(temp);
                            }
                            if (item2.idType == 2)
                            {
                                ListBill temp = new ListBill();
                                temp = item;
                                List_Booking_idvl.Add(temp);
                            }
                            if (item2.idType == 3)
                            {
                                ListBill temp = new ListBill();
                                temp = item;
                                List_Booking_idbk.Add(temp);
                            }
                        }
                    }
                }
                
            }
            
            return;
        }
        private void Device_ListBooking()
        {
            if (_Listbooking == null)
            {
                return;
            }
            _ListBooking_soccer = new ObservableCollection<ListBill>();
            _ListBooking_Bk = new ObservableCollection<ListBill>();
            _ListBooking_VL = new ObservableCollection<ListBill>();
            foreach (var item in _Listbooking)
            {
                
                if (item.List_Booking.field.idType == 1)    //football field
                {
                    _ListBooking_soccer.Add(item);
                }
                if (item.List_Booking.field.idType == 2)    //Vollleyball court
                {
                    _ListBooking_VL.Add(item);
                }
                if (item.List_Booking.field.idType == 3)    //Basketball court
                {
                    _ListBooking_Bk.Add(item);
                }
            }

        }

        private void Load_Field()

        {
            _ListBill = new ObservableCollection<ListBill>();
            var Ex = DataProvider.Ins.DB.fields;
            List<ListBill> In_funtion = new List<ListBill>();
            foreach(var item in Ex)
            {
                ListBill temp = new ListBill();
                temp.List_Field = item;
                In_funtion.Add(temp);
            }

            foreach(var item in In_funtion)
            {
                if (item.List_Field.idType == 1)
                {
                    _ListBill.Add(item);
                }
            }
        }

        
    }
}

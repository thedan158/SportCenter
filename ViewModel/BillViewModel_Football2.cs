using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.ViewModel
{
    public class BillViewModel_Football2 : BaseViewModel
    {
        private ObservableCollection<bookingInfo> _DayList;
        public ObservableCollection<bookingInfo> DayList { get => _DayList; set { _DayList = value; OnPropertyChanged(); } }

        public int _idbooking_Payment;
        public int idbooking_Payment { get => _idbooking_Payment; set { _idbooking_Payment = SelectedItem.id;OnPropertyChanged(); } }

        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }
        private int _CustomerPhoneNum;
        public int CustomerPhoneNum { get => _CustomerPhoneNum; set { _CustomerPhoneNum = value; OnPropertyChanged(); } }
        private string _NameField;
        public string NameField { get => _NameField; set { _NameField = value; OnPropertyChanged(); } }
        private int? _Field_id;
        public int? Field_id { get => _Field_id; set { _Field_id = value; OnPropertyChanged(); } }
        public int _Booking_id;
        public int Booking_id { get => _Booking_id; set { _Booking_id = value;OnPropertyChanged(); } }
        private decimal? _FieldPrice;
        public decimal? FieldPrice { get => _FieldPrice; set { _FieldPrice = value; OnPropertyChanged(); } }
        private DateTime? _DateBooking;
        public DateTime? DateBooking { get => _DateBooking; set { _DateBooking = value; OnPropertyChanged();} }
        private DateTime? _StartTime;
        public DateTime? StartTime { get => _StartTime; set { _StartTime = value; OnPropertyChanged(); } }
        private DateTime? _EndTime;
        public DateTime? EndTime { get => _EndTime; set { _EndTime = value; OnPropertyChanged(); } }
        protected List<buyingInfo> _Listgoodsbooking;
        


        private bookingInfo _SelectedItem;

        public BillViewModel_Football2()
        {
            _DayList = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            _Listgoodsbooking = new List<buyingInfo>(DataProvider.Ins.DB.buyingInfoes);
        }

        public bookingInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {                    
                    Booking_id = SelectedItem.id;
                    idbooking_Payment = SelectedItem.id;
                    NameField = SelectedItem.field.name;
                    FieldPrice = SelectedItem.field.fieldtype.price;
                    Field_id = SelectedItem.idField;
                    DateBooking = SelectedItem.datePlay;
                    StartTime = SelectedItem.start_time;
                    EndTime = SelectedItem.end_time;
                    CustomerName = SelectedItem.Customer_name;
                    CustomerPhoneNum = SelectedItem.Customer_PhoneNum.Value;
                    
                }
            }
        }

    }
}

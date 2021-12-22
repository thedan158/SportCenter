using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SportCenter.ViewModel
{
    class BillViewModel_VolleyBall : BaseViewModel
    {
        
        private ObservableCollection<bookingInfo> _BookigList;
        public ObservableCollection<bookingInfo> BookingList_footballVM { get => _BookigList; set { _BookigList = value; OnPropertyChanged(); } }
        private ObservableCollection<bookingInfo> _DayList;
        public ObservableCollection<bookingInfo> DayList { get => _DayList; set { _DayList = value; OnPropertyChanged(); } }

        public int _idbooking_Payment;
        public int idbooking_Payment { get => _idbooking_Payment; set { _idbooking_Payment = SelectedItem.id; OnPropertyChanged(); } }

        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }
        private int _CustomerPhoneNum;
        public int CustomerPhoneNum { get => _CustomerPhoneNum; set { _CustomerPhoneNum = value; OnPropertyChanged(); } }
        private string _NameField;
        public string NameField { get => _NameField; set { _NameField = value; OnPropertyChanged(); } }
        private int? _Field_id;
        public int? Field_id { get => _Field_id; set { _Field_id = value; OnPropertyChanged(); } }
        public int _Booking_id;
        public int Booking_id { get => _Booking_id; set { _Booking_id = value; OnPropertyChanged(); } }
        private decimal? _FieldPrice;
        public decimal? FieldPrice { get => _FieldPrice; set { _FieldPrice = value; OnPropertyChanged(); } }
        private DateTime? _DateBooking;
        public DateTime? DateBooking { get => _DateBooking; set { _DateBooking = value; OnPropertyChanged(); } }
        private DateTime? _StartTime;
        public DateTime? StartTime { get => _StartTime; set { _StartTime = value; OnPropertyChanged(); } }
        private DateTime? _EndTime;
        public DateTime? EndTime { get => _EndTime; set { _EndTime = value; OnPropertyChanged(); } }


        public ICommand PaymentCMD { get; set; }

        private bookingInfo _SelectedItem;

        public BillViewModel_VolleyBall()
        {
            _DayList = new ObservableCollection<bookingInfo>();
            _BookigList = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            Update_DatagridView();
            Load_List_footballPayment();

            PaymentCMD = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var temp_check = DataProvider.Ins.DB.bookingInfoes;
                decimal fieldprice = 0;
                foreach (var item in temp_check)
                {
                    if (item.id == Booking_id)
                    {
                        fieldprice = item.field.fieldtype.price.Value;
                    }
                }
                if (DateBooking == null || StartTime == null || EndTime == null)
                {
                    MessageBox.Show("Select bill for Payment.");
                    return;
                }
                string date = DateBooking.Value.ToString("MM/dd/yyyy");
                string starttiem = StartTime.Value.ToString("hh:mm tt");
                string endtiem = EndTime.Value.ToString("hh:mm tt");
                PayMent_tem WD_pay = new PayMent_tem(Booking_id, CustomerName, CustomerPhoneNum.ToString(), date, starttiem, endtiem, fieldprice);
                WD_pay.ShowDialog();
                Update_DatagridView();
                Load_List_footballPayment();
            });
        }


        private void Update_DatagridView()
        {
            Booking_id = 0;
            CustomerName = null;
            CustomerPhoneNum = 0;
            StartTime = null;
            EndTime = null;
            DateBooking = null;
            FieldPrice = null;
            foreach (var item in _DayList.ToList())
            {
                _DayList.Remove(item);
            }

        }
        private void Load_List_footballPayment()
        {
            foreach (var info in _BookigList)
            {
                if (info.field.idType == 2)
                {
                    if (info.Status !=  "Pay")
                    {
                        bookingInfo temp = new bookingInfo();
                        temp = info;
                        _DayList.Add(temp);
                    }
                }
            }
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


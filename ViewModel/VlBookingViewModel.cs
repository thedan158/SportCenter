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
    public class VlBookingViewModel : VolleyballFieldViewModel
    {
        public ICommand AddbookingCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancleCommand { get; set; }
        private string _addcustomername;
        public string addcustomername { get => _addcustomername; set { _addcustomername = value; OnPropertyChanged(); } }
        private int? _addcustomerphone;
        public int? addcustomerphone { get => _addcustomerphone; set { _addcustomerphone = value; OnPropertyChanged(); } }
        private DateTime _addstarttime;
        public DateTime addstarttime { get => _addstarttime; set { _addstarttime = value; OnPropertyChanged(); } }
        private DateTime _addendtime;
        public DateTime addendtime { get => _addstarttime; set { _addstarttime = value; OnPropertyChanged(); } }
        private DateTime _adddateplay;
        public DateTime adddateplay { get => _adddateplay; set { _adddateplay = value; OnPropertyChanged(); } }
        private string _addstatus;
        public string addstatus { get => _addstatus; set { _addstatus = value; OnPropertyChanged(); } }

        private ObservableCollection<bookingInfo> _bookingInfo;
        public ObservableCollection<bookingInfo> ListbookingInfoVl { get => _bookingInfo; set { _bookingInfo = value; OnPropertyChanged(); } }
        public List<string> displayComboxbookingInfo;
        private bookingInfo _SelectedItemBooking;
        public bookingInfo SelectedItemBooking
        {
            get => _SelectedItemBooking;
            set
            {
                _SelectedItemBooking = value;
                OnPropertyChanged();
                if (_SelectedItemBooking != null)
                {
                    addcustomername = _SelectedItemBooking.Customer_name;
                    addcustomerphone = _SelectedItemBooking.Customer_PhoneNum;
                    addstatus = _SelectedItemBooking.Status;
                    addstarttime = _SelectedItemBooking.start_time;
                    addendtime = _SelectedItemBooking.end_time;
                    adddateplay = _SelectedItemBooking.datePlay;
                }
            }
        }
        public VlBookingViewModel()
        {
            _bookingInfo = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 1));
            Update_ListbookingVolleyball();
            Load_ListbookingVolleyball();
            EditCommand = new RelayCommand<object>((p) =>
            {
                //if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldCondition))
                //    return false;
                //var displayListname = DataProvider.Ins.DB.fields.Where(x => x.name == FieldName);
                //if (displayListname == null || displayListname.Count() != 0)
                //    return false;
                return true;
            }, (p) =>
            {
                var booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.id == SelectedItemBooking.id).SingleOrDefault();
                booking.Customer_name = addcustomername;
                booking.Customer_PhoneNum = addcustomerphone;
                booking.datePlay = adddateplay;
                booking.start_time = addstarttime;
                booking.end_time = addendtime;
                booking.Status = addstatus;
                DataProvider.Ins.DB.bookingInfoes.Add(booking);
                DataProvider.Ins.DB.SaveChanges();
                SelectedItemBooking.Customer_name = addcustomername;
                SelectedItemBooking.Customer_PhoneNum = addcustomerphone;
                SelectedItemBooking.datePlay = adddateplay;
                SelectedItemBooking.start_time = addstarttime;
                SelectedItemBooking.end_time = addendtime;
                SelectedItemBooking.Status = addstatus;
                Update_ListbookingVolleyball();
                Load_ListbookingVolleyball();
            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                //if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldCondition))
                //    return false;
                //var displayListname = DataProvider.Ins.DB.fields.Where(x => x.name == FieldName);
                //if (displayListname == null || displayListname.Count() != 0)
                //    return false;


                return true;
            }, (p) =>
            {

                var booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.id == SelectedItemBooking.id).SingleOrDefault();
                var bookingstatus = DataProvider.Ins.DB.bookingInfoes;
                foreach (var item in bookingstatus)
                {
                    if (SelectedItemBooking.id == item.id && item.Status != "unpay")
                    {
                        MessageBox.Show("Cannot delete");
                        return;
                    }
                }
                DataProvider.Ins.DB.bookingInfoes.Remove(booking);
                DataProvider.Ins.DB.SaveChanges();
                Update_ListbookingVolleyball();
                Load_ListbookingVolleyball();
            });

        }

        public void Load_ListbookingVolleyball()
        {
            var Temp_booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 2);
            foreach (var item in Temp_booking)
            {
                ListbookingInfoVl.Add(item);
            }
        }
        public void Update_ListbookingVolleyball()
        {
            if (ListbookingInfoVl == null)
            {
                return;
            }
            foreach (var item in ListbookingInfoVl.ToList())
            {
                ListbookingInfoVl.Remove(item);
            }
        }
    }
}

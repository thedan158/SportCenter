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
    public class BookingViewModel : BaseViewModel
    {
        public ICommand AddbookingCommand { get; set; }
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
        public ObservableCollection<bookingInfo> ListbookingInfo { get => _bookingInfo; set { _bookingInfo = value; OnPropertyChanged(); } }
        public List<string> displayComboxbookingInfo;    
        public BookingViewModel()
        {
            _bookingInfo = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            displayComboxbookingInfo = new List<string>();
            AddbookingCommand = new RelayCommand<object>((parameter) => true, (parameter) =>
            {
                AddbookingFunction();
            }
            );     
        }

        private void AddbookingFunction()
        {
            if (addcustomername!= null)
            {
                bookingInfo temp = new bookingInfo();
                temp.Customer_name = addcustomername;
                temp.Customer_PhoneNum = addcustomerphone;
                temp.datePlay = adddateplay;
                temp.start_time = addstarttime;
                temp.end_time = addendtime;
                temp.Status = "";
                DataProvider.Ins.DB.bookingInfoes.Add(temp);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Added Done.");
            }
            else
            {
                MessageBox.Show("No booking added.");
                return;
            }
        }
    }
}

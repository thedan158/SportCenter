using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SportCenter.ViewModel
{
    public class FastBookingVolleyballViewModel : BaseViewModel
    {
        private string _FBcustomername;
        public string FBcustomername { get => _FBcustomername; set { _FBcustomername = value; OnPropertyChanged(); } }

        private int? _FBcustomerphone;
        public int? FBcustomerphone { get => _FBcustomerphone; set { _FBcustomerphone = value; OnPropertyChanged(); } }

        private DateTime _FBstarttime;
        public DateTime FBstarttime { get => _FBstarttime; set { _FBstarttime = value; OnPropertyChanged(); } }

        private DateTime _FBendtime;
        public DateTime FBendtime { get => _FBendtime; set { _FBendtime = value; OnPropertyChanged(); } }

        private DateTime _FBdateplay;
        public DateTime FBdateplay { get => _FBdateplay; set { _FBdateplay = value; OnPropertyChanged(); } }

        public ICommand FastbookingCommand { get; set; }

        public FastBookingVolleyballViewModel()
        {
            FBdateplay = DateTime.Today;
     
            FastbookingCommand = new RelayCommand<object>((parameter) => true, (parameter) =>
            {
                if (FBcustomername == "" || FBcustomerphone.ToString() == "")
                {
                    MessageBox.Show("Please insert all value!!!");
                    return;
                }
                if (FBcustomername != "" && FBcustomerphone.ToString() != "")
                {
                    bookingInfo temp = new bookingInfo();
                    temp.Customer_name = FBcustomername;
                    temp.Customer_PhoneNum = FBcustomerphone;
                    temp.datePlay = FBdateplay;
                    temp.start_time = FBstarttime;
                    temp.end_time = FBendtime;
                    temp.Status = "unpay";
                    temp.end_time = temp.end_time.AddYears(-(temp.end_time.Year - 1));
                    temp.end_time = temp.end_time.AddMonths(-(temp.end_time.Month - 1));
                    temp.end_time = temp.end_time.AddDays(-(temp.end_time.Day - 1));
                    temp.start_time = temp.start_time.AddYears(-(temp.start_time.Year - 1));
                    temp.start_time = temp.start_time.AddMonths(-(temp.start_time.Month - 1));
                    temp.start_time = temp.start_time.AddDays(-(temp.start_time.Day - 1));
                    temp.end_time = temp.end_time.AddYears(temp.datePlay.Year - 1);
                    temp.end_time = temp.end_time.AddMonths(temp.datePlay.Month - 1);
                    temp.end_time = temp.end_time.AddDays(temp.datePlay.Day - 1);
                    temp.start_time = temp.start_time.AddYears(temp.datePlay.Year - 1);
                    temp.start_time = temp.start_time.AddMonths(temp.datePlay.Month - 1);
                    temp.start_time = temp.start_time.AddDays(temp.datePlay.Day - 1);
                    if (temp.start_time >= temp.end_time)
                    {
                        MessageBox.Show("Start time must be early than endtime!!");
                        return;
                    }
                    if (temp.datePlay < DateTime.Today || temp.start_time < DateTime.Now)
                    {
                        MessageBox.Show("Cannot choose a time before now!!");
                        return;
                    }
                    List<field> listfieldfull = new List<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 2));
                    var tempbooking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 2);
                    List<field> list = new List<field>();
                    foreach (var field in listfieldfull.ToList())
                    {

                        int count = 0;
                        foreach (var booking in tempbooking.ToList())
                        {
                            if (booking.field.id == field.id && booking.Status == "unpay")
                            {
                                if ((temp.start_time >= booking.start_time && temp.start_time <= booking.end_time) || (temp.end_time >= booking.start_time && temp.end_time <= booking.end_time) || (temp.start_time <= booking.start_time && temp.end_time >= booking.end_time))
                                {
                                    count++;
                                    break;
                                }
                            }
                        }
                        if (count != 0)
                            continue;
                        temp.idField = field.id;
                        temp.end_time = temp.end_time.AddYears(-(temp.end_time.Year - 1));
                        temp.end_time = temp.end_time.AddMonths(-(temp.end_time.Month - 1));
                        temp.end_time = temp.end_time.AddDays(-(temp.end_time.Day - 1));
                        temp.start_time = temp.start_time.AddYears(-(temp.start_time.Year - 1));
                        temp.start_time = temp.start_time.AddMonths(-(temp.start_time.Month - 1));
                        temp.start_time = temp.start_time.AddDays(-(temp.start_time.Day - 1));
                        temp.end_time = temp.end_time.AddYears(temp.datePlay.Year - 1);
                        temp.end_time = temp.end_time.AddMonths(temp.datePlay.Month - 1);
                        temp.end_time = temp.end_time.AddDays(temp.datePlay.Day - 1);
                        temp.start_time = temp.start_time.AddYears(temp.datePlay.Year - 1);
                        temp.start_time = temp.start_time.AddMonths(temp.datePlay.Month - 1);
                        temp.start_time = temp.start_time.AddDays(temp.datePlay.Day - 1);
                        DataProvider.Ins.DB.bookingInfoes.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();

                        string message = "Booking success \nBooking infomation:\nId field: " + temp.idField + "\nField name: " + temp.field.name + "\nCustomer name: " + temp.Customer_name + "\nPhone number: " + temp.Customer_PhoneNum + "\n" + "Timezone: " + temp.start_time.ToString("hh:mm tt") + " to " + temp.end_time.ToString("hh:mm tt");
                        MessageBox.Show(message);
                        FBcustomername = null;
                        FBcustomerphone = null;
                        FBstarttime = DateTime.Now;
                        FBendtime = DateTime.Now;
                        FBdateplay = DateTime.Today;
                        return;
                    }
                    MessageBox.Show("No field meet requirement");
                    temp.end_time = temp.end_time.AddYears(-(temp.end_time.Year - 1));
                    temp.end_time = temp.end_time.AddMonths(-(temp.end_time.Month - 1));
                    temp.end_time = temp.end_time.AddDays(-(temp.end_time.Day - 1));
                    temp.start_time = temp.start_time.AddYears(-(temp.start_time.Year - 1));
                    temp.start_time = temp.start_time.AddMonths(-(temp.start_time.Month - 1));
                    temp.start_time = temp.start_time.AddDays(-(temp.start_time.Day - 1));
                    return;
                }
            }
            );
        }


    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SportCenter.Model;
using SportCenter.ViewModel;

namespace SportCenter
{
    /// <summary>
    /// Interaction logic for InfoField.xaml
    /// </summary>
    public partial class InfoFieldVl : Window
    {
        public int _idFieldadding;
        public InfoFieldVl(int id_field)
        {

            InitializeComponent();
            txbIdfield.Text = id_field.ToString();
            dp.SelectedDate = DateTime.Today;
            var temp_fieldlist = DataProvider.Ins.DB.fields;
            foreach (var item in temp_fieldlist)
            {
                if (item.id == id_field)
                {
                    TitleTxt.Text = item.name.ToString();
                }
            }
            _idFieldadding = id_field;

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

            if (dp.SelectedDate == null || startT_picker.SelectedTime == null || endT_picker.SelectedTime == null || txbCusName.Text == "" || txbCusPhone.Text == "")
            {
                MessageBox.Show("Please insert all value!!!");
                return;
            }
            DateTime selectedDate = dp.SelectedDate.Value;
            String.Format("{0:MM-dd-yyyy}", selectedDate);
            DateTime selectedStarttime = startT_picker.SelectedTime.Value;
            String.Format("{0:MM-dd-yyyy h:mm:ss}", selectedStarttime);
            DateTime selectedEndtime = endT_picker.SelectedTime.Value;
            String.Format("{0:MM-dd-yyyy h:mm:ss}", selectedEndtime);
            string Cusname = txbCusName.Text;
            string Cusphone = txbCusPhone.Text;
            string m_seletDate = dp.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
            string m_seletStarttime = startT_picker.SelectedTime.Value.ToString("hh:mm tt");
            string m_seletEndtime = endT_picker.SelectedTime.Value.ToString("hh:mm tt");


            var temp_listBooking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 2);
            List<bookingInfo> temp_listVolleyballlistbooking = new List<bookingInfo>();
            bookingInfo adding_element = new bookingInfo();     //parameter to adding to DB

            //Adding list Volleyball booking.
            foreach (var item in temp_listBooking)
            {
                if (item.field.id == _idFieldadding && item.Status == "unpay")
                {
                    temp_listVolleyballlistbooking.Add(item);
                }
            }


            if (dp.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Cannot choose a time before now!!");
                return;
            }
            if (selectedStarttime >= selectedEndtime)
            {
                MessageBox.Show("Start time must be early than endtime!!");
                return;
            }
            //Adding item to list booking in Database
            adding_element.idField = _idFieldadding;
            adding_element.datePlay = selectedDate;
            adding_element.start_time = selectedStarttime;
            adding_element.end_time = selectedEndtime;
            adding_element.Status = "unpay";
            adding_element.Customer_name = Cusname;
            adding_element.Customer_PhoneNum = int.Parse(Cusphone);
            adding_element.end_time = adding_element.end_time.AddYears(-(adding_element.end_time.Year - 1));
            adding_element.end_time = adding_element.end_time.AddMonths(-(adding_element.end_time.Month - 1));
            adding_element.end_time = adding_element.end_time.AddDays(-(adding_element.end_time.Day - 1));
            adding_element.start_time = adding_element.start_time.AddYears(-(adding_element.start_time.Year - 1));
            adding_element.start_time = adding_element.start_time.AddMonths(-(adding_element.start_time.Month - 1));
            adding_element.start_time = adding_element.start_time.AddDays(-(adding_element.start_time.Day - 1));
            adding_element.end_time = adding_element.end_time.AddYears(adding_element.datePlay.Year - 1);
            adding_element.end_time = adding_element.end_time.AddMonths(adding_element.datePlay.Month - 1);
            adding_element.end_time = adding_element.end_time.AddDays(adding_element.datePlay.Day - 1);
            adding_element.start_time = adding_element.start_time.AddYears(adding_element.datePlay.Year - 1);
            adding_element.start_time = adding_element.start_time.AddMonths(adding_element.datePlay.Month - 1);
            adding_element.start_time = adding_element.start_time.AddDays(adding_element.datePlay.Day - 1);
            //Checking condition for adding stament.
            foreach (var item in temp_listVolleyballlistbooking.ToList())
            {
                if (adding_element.idField == item.idField)
                {
                    if ((adding_element.start_time >= item.start_time) && (adding_element.start_time <= item.end_time) || (adding_element.end_time >= item.start_time) && (adding_element.end_time <= item.end_time) || (adding_element.start_time < item.start_time && adding_element.end_time > item.start_time))
                    {
                        MessageBox.Show("Already booking time zone!!!");
                        return;
                    }
                }
            }
            if (adding_element.start_time < DateTime.Now)
            {
                MessageBox.Show("Cannot choose a time before now!!");
                return;
            }
            DataProvider.Ins.DB.bookingInfoes.Add(adding_element);
            DataProvider.Ins.DB.SaveChanges();
            string message = "Booking success \nBooking infomation: " + Cusname + " " + Cusphone + "\n" + "Timezone: " + m_seletStarttime + " " + m_seletEndtime;
            MessageBox.Show(message);
            dp.SelectedDate = DateTime.Today;
            txbCusName.Text = null;
            txbCusPhone.Text = null;
            startT_picker.SelectedTime = DateTime.Now;
            endT_picker.SelectedTime = DateTime.Now;
            var VlBookingVM = this.DataContext;
            (this.DataContext as VolleyballFieldViewModel).Update_ListbookingVolleyball();
            (this.DataContext as VolleyballFieldViewModel).Load_ListbookingVolleyball();
            (this.DataContext as VolleyballFieldViewModel).Update_DatagridView12();
            (this.DataContext as VolleyballFieldViewModel).Load_List_footballPayment();

        }
    }
}

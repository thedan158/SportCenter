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
    public partial class InfoFieldBk : Window
    {
        int _idFieldadding;
        public InfoFieldBk(int id_field)
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
            List<field> List_BasketballField = new List<field>();
            foreach (var item in temp_fieldlist)
            {
                if (item.fieldtype.id == 3)
                {
                    List_BasketballField.Add(item);
                }
            }
            _idFieldadding = id_field;

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

            if (dp.SelectedDate == null || startT_picker.SelectedTime == null || endT_picker.SelectedTime == null || txbCusName == null || txbCusPhone == null)
            {
                MessageBox.Show("Please insert all value!!!");
                return;
            }
            DateTime selectedDate = dp.SelectedDate.Value;
            String.Format("{0:MM-dd-yyyy}", selectedDate);
            DateTime selectedStarttime = startT_picker.SelectedTime.Value;
            String.Format("{0:h:mm:ss}", selectedStarttime);
            DateTime selectedEndtime = endT_picker.SelectedTime.Value;
            String.Format("{0:h:mm:ss}", selectedEndtime);
            string Cusname = txbCusName.Text;
            string Cusphone = txbCusPhone.Text;
            string m_seletDate = dp.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
            string m_seletStarttime = startT_picker.SelectedTime.Value.ToString("hh:mm tt");
            string m_seletEndtime = endT_picker.SelectedTime.Value.ToString("hh:mm tt");


            var temp_listBooking = DataProvider.Ins.DB.bookingInfoes;
            List<bookingInfo> temp_listBasketballlistbooking = new List<bookingInfo>();
            bookingInfo adding_element = new bookingInfo();     //parameter to adding to DB

            //Adding list Basketball booking.
            foreach (var item in temp_listBooking)
            {
                if (item.field.idType == 3)
                {
                    temp_listBasketballlistbooking.Add(item);
                }
            }

            //Checking condition for adding stament.
            foreach (var item in temp_listBasketballlistbooking)
            {
                if (item.Status == "unpay" && item.datePlay == selectedDate && item.start_time == selectedStarttime && item.end_time == selectedEndtime)
                {
                    MessageBox.Show("Already booking time zone!!!");
                    return;
                }

            }
            if (dp.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Can not choose a date before now!!");
                return;
            }
            if (selectedStarttime > selectedEndtime)
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
            var BkBookingVM = this.DataContext;
            (this.DataContext as BasketballFieldViewModel).Update_ListbookingBasketball();
            (this.DataContext as BasketballFieldViewModel).Load_ListbookingBasketball();
            (this.DataContext as BasketballFieldViewModel).Update_DatagridView12();
            (this.DataContext as BasketballFieldViewModel).Load_List_footballPayment();

        }
    }
}

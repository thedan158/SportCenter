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
    public partial class InfoFieldSc : Window
    {
        int _idFieldadding;
        public InfoFieldSc(int id_field)
        {
            InitializeComponent();
            dp.SelectedDate = DateTime.Today;
            var temp_fieldlist = DataProvider.Ins.DB.fields;
            foreach (var item in temp_fieldlist)
            {
                if (item.id == id_field)
                {
                    TitleTxt.Text = item.name.ToString();
                }
            }
            List<field> List_soccerField = new List<field>();
            foreach (var item in temp_fieldlist)
            {
                if (item.fieldtype.id == 1)
                {
                    List_soccerField.Add(item);
                }
            }
            _idFieldadding = id_field;

            // Display list booking with confirm idField

            ObservableCollection<bookingInfo> ListBookinginfo = new ObservableCollection<bookingInfo>();
            var temp_booking_list = DataProvider.Ins.DB.bookingInfoes;



        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

            if (dp.SelectedDate == null || startT_picker.SelectedTime == null || endT_picker.SelectedTime == null || txbCusName == null || txbCusPhone == null)
            {
                MessageBox.Show("Please insert all value!!!");
                return;
            }
            DateTime selectedDate = dp.SelectedDate.Value;
            DateTime selectedStarttime = startT_picker.SelectedTime.Value;
            DateTime selectedEndtime = endT_picker.SelectedTime.Value;
            string Cusname = txbCusName.Text;
            string Cusphone = txbCusPhone.Text;
            string m_seletDate = dp.SelectedDate.Value.ToString("dddd, dd MMMM yyyy");
            string m_seletStarttime = startT_picker.SelectedTime.Value.ToString("hh:mm tt");
            string m_seletEndtime = endT_picker.SelectedTime.Value.ToString("hh:mm tt");


            var temp_listBooking = DataProvider.Ins.DB.bookingInfoes;
            List<bookingInfo> temp_listsoccerlistbooking = new List<bookingInfo>();
            bookingInfo adding_element = new bookingInfo();     //parameter to adding to DB

            //Adding list soccer booking.
            foreach (var item in temp_listBooking)
            {
                if (item.field.idType == 1)
                {
                    temp_listsoccerlistbooking.Add(item);
                }
            }

            //Checking condition for adding stament.
            foreach (var item in temp_listsoccerlistbooking)
            {
                if (item.Status == "unpay" && item.datePlay == selectedDate && item.start_time == selectedStarttime && item.end_time == selectedEndtime)
                {
                    MessageBox.Show("Already booking time zone!!!");
                    return;
                }

            }
            DateTime now = DateTime.Now;
            if (dp.SelectedDate < now)
            {
                MessageBox.Show("Please select another date!!!");
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

            DataProvider.Ins.DB.bookingInfoes.Add(adding_element);
            DataProvider.Ins.DB.SaveChanges();
            string message = "Booking success \nBooking infomation: " + Cusname + " " + Cusphone + "\n" + "Timezone: " + m_seletStarttime + " " + m_seletEndtime;
            MessageBox.Show(message);
            dp.SelectedDate = null;
            txbCusName.Text = null;
            txbCusPhone.Text = null;
            startT_picker.SelectedTime = null;
            endT_picker.SelectedTime = null;
            var ScBookingVM = this.DataContext;
            (this.DataContext as ScBookingViewModel).Update_Listbookingsoccer();
            (this.DataContext as ScBookingViewModel).Load_Listbookingsoccer();


        }
    }
}

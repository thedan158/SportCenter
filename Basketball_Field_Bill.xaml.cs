using System;
using System.Collections.Generic;
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
using SportCenter.ViewModel;

namespace SportCenter
{
    /// <summary>
    /// Interaction logic for Basketball_Field_Bill.xaml
    /// </summary>
    public partial class Basketball_Field_Bill : Window
    {
        public Basketball_Field_Bill()
        {
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int id_booking = int.Parse(Booking_id.Text);
            string CustomerName = C_name.Text;
            string CustomerPhone = C_phone.Text;
            string datebooking = DateBooking.Text;
            string starttime = Starttime.Text;
            string endtime = EndTime.Text;
            string fieldprice = txbTotalMoney.Text;
            PayMent_tem P_window = new PayMent_tem(id_booking, CustomerName, CustomerPhone, datebooking, starttime, endtime, fieldprice);
            P_window.Show();
        }
    }
}

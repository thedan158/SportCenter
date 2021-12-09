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

namespace SportCenter
{
    /// <summary>
    /// Interaction logic for Football_Field_Bill.xaml
    /// </summary>
    public partial class Football_Field_Bill : Window
    {
        public Football_Field_Bill()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    int id_booking = int.Parse(Booking_id.Text);
        //    string CustomerName = C_name.Text;
        //    string CustomerPhone = C_phone.Text;
        //    string datebooking = DateBooking.Text;
        //    string starttime = Starttime.Text;
        //    string endtime = EndTime.Text;
        //    string fieldprice = txbTotalMoney.Text;
        //    if (id_booking == 0)
        //    {
        //        MessageBox.Show("Select the section in list payment.");
        //        return;
        //    }
        //    PayMent_tem P_window = new PayMent_tem(id_booking, CustomerName, CustomerPhone, datebooking, starttime, endtime, fieldprice);
        //    P_window.Show();
        //}
    }
}

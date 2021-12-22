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
using SportCenter.Model;
using System.Collections.ObjectModel;

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
            (this.DataContext as SoccerFieldViewModel).Update_DatagridView12();
            (this.DataContext as SoccerFieldViewModel).Load_List_footballPayment();

            var _datacontext = this.DataContext as BillViewModel_Football2;
            Load_list();

        }
        
        private void Load_list()
        {
            var temp_booking = DataProvider.Ins.DB.bookingInfoes;
            ObservableCollection<bookingInfo> List = new ObservableCollection<bookingInfo>();

            foreach (var item in temp_booking)
            {
                if (item.field.idType == 1)
                {
                    if (item.Status != "Pay")
                    {
                        List.Add(item);
                    }
                }

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp_check = DataProvider.Ins.DB.bookingInfoes;
            var _datacontext = this.DataContext as BillViewModel_Football2;
            decimal fieldprice = 0;
            foreach (var item in temp_check)
            {
                if (item.id == _datacontext.Booking_id)
                {
                    fieldprice = item.field.fieldtype.price.Value;
                }
            }
            if (_datacontext.DateBooking == null || _datacontext.StartTime == null || _datacontext.EndTime == null)
            {
                MessageBox.Show("Select bill for Payment.");
                return;
            }
            string date = _datacontext.DateBooking.Value.ToString("MM/dd/yyyy");
            string starttiem = _datacontext.StartTime.Value.ToString("hh:mm tt");
            string endtiem = _datacontext.EndTime.Value.ToString("hh:mm tt");
            PayMent_tem WD_pay = new PayMent_tem(_datacontext.Booking_id, _datacontext.CustomerName, _datacontext.CustomerPhoneNum.ToString(), date, starttiem, endtiem, fieldprice);
            WD_pay.ShowDialog();

            


        }
        
    }
}

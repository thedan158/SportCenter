using SportCenter.Model;
using SportCenter.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for PayMent_tem.xaml
    /// </summary>
    public partial class PayMent_tem
    {
        //global para for adding to DB
        int idbooking_DB_add;
        decimal totalmoney_DB_add;
        public PayMent_tem(int id_booking, string CustomerName, string CustomerPhone, string Booking_date, string start_time, string end_time, decimal Field_price)
        {
            InitializeComponent();
            // Generate
            string _cPhone = CustomerPhone;
            string _cName = CustomerName;
            string _txtbookingdate = Booking_date;
            string _txtstarttime = start_time;
            string _txtendtime = end_time;
            string _txtfieldprice = (decimal.ToInt32(Field_price)).ToString();
;           string _txtidbooking = id_booking.ToString();
            int _goodtotalvalue = 0;
            idbooking_DB_add = id_booking;

            // Calculate goods total and bill total
            List<buyingInfo> _Listbuying = new List<buyingInfo>(DataProvider.Ins.DB.buyingInfoes);
            List<good> _Listgood = new List<good>(DataProvider.Ins.DB.goods);
            ObservableCollection<FormatDisplayGoodsBill> ListgoodBook = new ObservableCollection<FormatDisplayGoodsBill>();
            List<ListBill> _Listgoodbooking = new List<ListBill>(); //List good with corresponding idbooking
            foreach (var item in _Listbuying)
            {
                if (item.bookingInfo.id == id_booking)
                {
                    ListBill temp_good_add = new ListBill();
                    int _quantity = item.quantity.Value;
                    int _good_price = Decimal.ToInt32(item.good.price.Value);
                    _goodtotalvalue += (_quantity * _good_price);
                    foreach (var item_goodInfoBuying in _Listgood)
                    {
                        if (item.good.id == item_goodInfoBuying.id)
                        {
                            temp_good_add.Good_info_base = item_goodInfoBuying;
                            temp_good_add.List_Buying = item;
                            temp_good_add.total_goods = (_quantity * _good_price);
                        }
                    }
                    _Listgoodbooking.Add(temp_good_add);
                }
            }
            int _totalbillvalue = (Decimal.ToInt32(Field_price) + _goodtotalvalue);
            totalmoney_DB_add = _totalbillvalue;
            foreach (var item in _Listgoodbooking)
            {
                FormatDisplayGoodsBill temp = new FormatDisplayGoodsBill();

                temp.Name = item.Good_info_base.name;
                temp.Price = Decimal.ToInt32(item.Good_info_base.price.Value);
                temp.Quantity = item.List_Buying.quantity;
                temp.Unit = item.Good_info_base.unit;
                temp.Total = item.total_goods;
                ListgoodBook.Add(temp);
            }
            for (int i = 0; i < ListgoodBook.Count(); i++)
            {
                ListgoodBook[i].No = i + 1;
            }

            // Set display value for .xaml            
            txbIdBooking.Text = _txtidbooking;
            txbDateBooking.Text = _txtbookingdate;
            txbCheckInTime.Text = _txtstarttime;
            txbCheckOutTime.Text = _txtendtime;
            txbfieldprice.Text = _txtfieldprice;
            txbgoodspricetotal.Text = _goodtotalvalue.ToString();
            txbTotal.Text = _totalbillvalue.ToString();
            txbCustomerName.Text = _cName;
            txbCustomerPhoneNumber.Text = _cPhone;

            // --------- Setup datagrid value for binding to .xaml ----------
            DG_goodListbooking.ItemsSource = ListgoodBook;
            int RowCount = ListgoodBook.Count();
            for (int i = 0; i < RowCount; i++)
            {
                if (i % 2 == 0)
                {

                }
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bill adding_DB = new bill();
            {
                adding_DB.idBookingInfo = idbooking_DB_add;
                adding_DB.totalmoney = totalmoney_DB_add;
            }
            List<bookingInfo> Update_bookigStatus = new List<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            foreach (var item in Update_bookigStatus)
            {
                if (item.id == adding_DB.idBookingInfo)
                {
                    item.Status = "Pay";
                }
            }

            DataProvider.Ins.DB.bills.Add(adding_DB);
            await DataProvider.Ins.DB.SaveChangesAsync();
            //MainWindow mainWindow = new MainWindow();
            //MainViewModel mainVM = mainWindow.DataContext as MainViewModel;
            //mainVM.LoadStatictics();
            this.Close();
        }
    }
}

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SportCenter.ViewModel
{
    public class MainViewModel : BaseViewModel

    {
        //ListBill
        private ObservableCollection<bill> _Listbills;
        public ObservableCollection<bill> Listbills { get => _Listbills; set { _Listbills = value; OnPropertyChanged(); } }
        //Statictis
        private string alltimerevenue = "0";
        public string Alltimerevenue { get => alltimerevenue; set { alltimerevenue = value; OnPropertyChanged(); } }
        private string alltimerevenueUSD = "0";
        public string AlltimerevenueUSD { get => alltimerevenueUSD; set { alltimerevenueUSD = value; OnPropertyChanged(); } }
        private string thisMonthRevenue = "0";
        public string ThisMonthRevenue { get => thisMonthRevenue; set { thisMonthRevenue = value; OnPropertyChanged(); } }

        //ListFields
        private ObservableCollection<field> _Listfields;
        private ObservableCollection<fieldtype> _Listfieldtypes;
        //CartesianChart
        public Func<double, string> YFormatter { get; set; }
        private SeriesCollection _SeriesCollection1;
        public SeriesCollection SeriesCollection1 { get => _SeriesCollection1; set { _SeriesCollection1 = value; OnPropertyChanged(); } }
        public string[] Labels { get; set; }
        //PieChart
        public Func<ChartPoint, string> PointLabel { get; set; }
        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection { get => _SeriesCollection; set { _SeriesCollection = value; OnPropertyChanged(); } }

        //List fields which was booked
        private ObservableCollection<bookingInfo> _Listbooking;
        public ObservableCollection<bookingInfo> Listbooking { get => _Listbooking; set { _Listbooking = value; OnPropertyChanged(); } }

        //List goods in storage
        private ObservableCollection<good> _Listgood;
        public ObservableCollection<good> Listgood { get => _Listgood; set { _Listgood = value; OnPropertyChanged(); } }

        //List goods which was ordered
        private ObservableCollection<buyingInfo> _Listbuying;
        public ObservableCollection<buyingInfo> Listbuying { get => _Listbuying; set { _Listbuying = value; OnPropertyChanged(); } }

        private ObservableCollection<buyingInfo> _Listorder;
        public ObservableCollection<buyingInfo> Listorder { get => _Listorder; set { _Listorder = value; OnPropertyChanged(); } }

        private ObservableCollection<BaseCustomerInfo> _ListCustomerInfo;
        public ObservableCollection<BaseCustomerInfo> ListCustomerInfo { get => _ListCustomerInfo; set { _ListCustomerInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<bookingInfo> _ListbookingCombobox;
        public ObservableCollection<bookingInfo> ListbookingCombobox { get => _ListbookingCombobox; set { _ListbookingCombobox = value; OnPropertyChanged(); } }

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ShowWindowCommand_FB { get; set; }
        public ICommand ShowWindowCommand_BK { get; set; }
        public ICommand ShowWindowCommand_VL { get; set; }
        public ICommand ShowFootballFieldCommand { get; set; }
        public ICommand ShowVolleyballFieldCommand { get; set; }
        public ICommand ShowBasketballFieldCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ReloadStatictics { get; set; }
        public ICommand DeleteAllBillCommand { get; set; }



        //Storage VM

        private string imageFileName;

        private int _idgood;
        public int idgood { get => _idgood; set { _idgood = value; OnPropertyChanged(); } }

        private string _namegood;
        public string namegood { get => _namegood; set { _namegood = value; OnPropertyChanged(); } }

        private decimal? _pricegood;
        public decimal? pricegood { get => _pricegood; set { _pricegood = value; OnPropertyChanged(); } }

        private string _unitgood;
        public string unitgood { get => _unitgood; set { _unitgood = value; OnPropertyChanged(); } }





        public ICommand OpenBillReportWindow { get; set; }



        // Good VM
        public ICommand addCommand { get; set; }
        public ICommand editCommand { get; set; }
        public ICommand deleteCommand { get; set; }

        public ICommand SelectImageCommand { get; set; }

        private good _SelectedItem;
        public good SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    idgood = SelectedItem.id;
                    namegood = SelectedItem.name;
                    pricegood = SelectedItem.price;
                    unitgood = SelectedItem.unit;
                }

            }
        }


        //Order VM


        private decimal? _total = 0;
        public decimal? total { get => _total; set { _total = value; OnPropertyChanged(); } }


        public ICommand AddbuyingCommand => new RelayCommand<object>(CanExecuted, AddExecuted);
        public ICommand DelbuyingCommand => new RelayCommand<object>(CanExecuted, DelExecuted);
        public ICommand PlusCommand => new RelayCommand<object>(Plus_CanExecuted, Plus_Executed);
        public ICommand MinusCommand => new RelayCommand<object>(Minus_CanExecuted, Minus_Executed);
        public ICommand ClearAllCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        private int _Selectedidbooking;
        public int Selectedidbooking { get => _Selectedidbooking; set { _Selectedidbooking = value; OnPropertyChanged(); } }

        private bookingInfo _FieldSelectedItem;
        public bookingInfo FieldSelectedItem
        {
            get => _FieldSelectedItem;
            set
            {
                _FieldSelectedItem = value;
                OnPropertyChanged();
                if (FieldSelectedItem != null)
                {
                    Selectedidbooking = FieldSelectedItem.id;
                }

            }
        }
        private MainWindow mainWindow;
        public MainWindow MainWindow { get => mainWindow; set => mainWindow = value; }

        public MainViewModel()
        {

            _ListbookingCombobox = new ObservableCollection<bookingInfo>();
            _Listbooking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            _Listbooking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            _Listbuying = new ObservableCollection<buyingInfo>(DataProvider.Ins.DB.buyingInfoes);
            _Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
            _Listorder = new ObservableCollection<buyingInfo>();
            _ListCustomerInfo = new ObservableCollection<BaseCustomerInfo>();

            _Listfields = new ObservableCollection<field>(DataProvider.Ins.DB.fields);
            _Listfieldtypes = new ObservableCollection<fieldtype>(DataProvider.Ins.DB.fieldtypes);
            _SeriesCollection = new SeriesCollection();
            _SeriesCollection1 = new SeriesCollection();

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadStorageData();
                    Update_ListCustomerInfo();
                    LoadListCustomerInfo();
                    LoadStatictics();
                }
                else
                {
                    p.Close();
                }

            });
            ShowWindowCommand_FB = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_FB());
            ShowWindowCommand_BK = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_BK());
            ShowWindowCommand_VL = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_VL());
            ShowFootballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowFootballFieldFunction());
            ShowVolleyballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowVolleyballFieldFunction());
            ShowBasketballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowBasketballFieldFuction());
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ChooseImage(parameter));
            ReloadCommand = new RelayCommand<object>((parameter) => true, (parameter) => ReloadBookingFunction());
            OpenBillReportWindow = new RelayCommand<object>((parameter) => true, (parameter) => f_Open_Bill_Report());
            ReloadStatictics = new RelayCommand<object>((parameter) => true, (parameter) => LoadStatictics());
            DeleteAllBillCommand = new RelayCommand<object>((parameter) => true, (parameter) => DeleteAllBillFunction());
            LogoutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadStorageData();
                    Update_ListCustomerInfo();
                    LoadListCustomerInfo();
                    //LoadStatictics();
                }
                else
                {
                    p.Close();
                }
            });








            // Add goods
            addCommand = new RelayCommand<Grid>((parameter) =>
            {
                if (string.IsNullOrEmpty(namegood))
                {
                    return false;
                }

                var nameList = DataProvider.Ins.DB.goods.Where(p => p.name == namegood);
                if (nameList == null || nameList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            },
            (parameter) => AddGoods(parameter));


            //Edit goods
            editCommand = new RelayCommand<Grid>((parameter) =>
            {

                if (string.IsNullOrEmpty(namegood) || SelectedItem == null)
                    return false;
                var nameList = DataProvider.Ins.DB.goods.Where(p => p.id == idgood);
                if (nameList != null && nameList.Count() != 0)
                    return true;
                return false;
            }, (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Confirm edit?", "Note", MessageBoxButton.YesNo);
                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);

                if (result == MessageBoxResult.Yes)
                {
                    if (imageFileName != null)
                    {
                        byte[] imgByteArr;
                        imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
                        var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                        good.name = namegood;
                        good.price = pricegood;
                        good.unit = unitgood;
                        good.imageFile = imgByteArr;

                        DataProvider.Ins.DB.SaveChanges();
                        parameter.Background = null;
                        parameter.Children[0].Visibility = Visibility.Visible;
                        parameter.Children[2].Visibility = Visibility.Visible;
                        MessageBox.Show("Edit good successfully!!");
                    }
                    else
                    {
                        var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                        good.name = namegood;
                        good.price = pricegood;
                        good.unit = unitgood;


                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Edit good successfully!!");
                    }
                }

            });

            //Delete goods
            deleteCommand = new RelayCommand<Grid>((parameter) => true,
            (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Confirm delete?", "Note", MessageBoxButton.YesNo);

                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                if (result == MessageBoxResult.Yes)
                {
                    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    DataProvider.Ins.DB.goods.Remove(good);
                    DataProvider.Ins.DB.SaveChanges();
                    Listgood.Remove(good);
                    idgood = 0;
                    namegood = null;
                    pricegood = null;
                    unitgood = null;
                    MessageBox.Show("Delete good successfully!!");
                }
            });

            OrderCommand = new RelayCommand<object>((parameter) => true,
            (parameter) =>
            {
                if (FieldSelectedItem == null)
                {
                    MessageBox.Show("Please choose field!!");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Confirm?", "Note", MessageBoxButton.YesNo);


                    if (result == MessageBoxResult.Yes)
                    {

                        foreach (var item in Listorder)
                        {
                            //buyingInfo buying = new buyingInfo {idGood=item.idGood,quantity=item.quantity,idBookingInfo=Selectedidbooking,};
                            buyingInfo buying = new buyingInfo();
                            buying.idGood = item.good.id;
                            buying.quantity = item.quantity;
                            buying.orderprice = item.orderprice;
                            buying.idBookingInfo = Selectedidbooking;

                            DataProvider.Ins.DB.buyingInfoes.Add(buying);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Order successfully!!!");

                        Listorder.Clear();
                    }
                }
            });
            ClearAllCommand = new RelayCommand<object>((parameter) => true,
            (parameter) =>
            {
                Listorder.Clear();
                total = 0;
            });
        }

        public void ReloadBookingFunction()
        {
            var tempbooking = DataProvider.Ins.DB.bookingInfoes;
            if (ListbookingCombobox != null)
            {
                ListbookingCombobox.Clear();
            }
                foreach (var item in tempbooking)
            {
                if (item.Status == "unpay")
                {
                    ListbookingCombobox.Add(item);
                }
            }
            
        }

        public void AddGoods(Grid parameter)
        {
            {



                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                if (imageFileName == null)
                {
                    MessageBox.Show("Please choose image!!");
                }
                else
                {

                    byte[] imgByteArr;
                    imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
                    var good = new good() { name = namegood, id = idgood, price = pricegood, unit = unitgood, imageFile = imgByteArr };
                    DataProvider.Ins.DB.goods.Add(good);
                    DataProvider.Ins.DB.SaveChanges();
                    Listgood.Add(good);
                    idgood = 0;
                    namegood = null;
                    pricegood = null;
                    unitgood = null;
                    parameter.Background = null;
                    parameter.Children[0].Visibility = Visibility.Visible;
                    parameter.Children[2].Visibility = Visibility.Visible;
                    MessageBox.Show("Add good successfully!!");
                }

            }
        }

        public void DeleteAllBillFunction()
        {
            _Listbills = new ObservableCollection<bill>(DataProvider.Ins.DB.bills);
            foreach (var itemBill in _Listbills)
            {
                DataProvider.Ins.DB.bills.Remove(itemBill);

            }
            DataProvider.Ins.DB.SaveChangesAsync();
            MessageBox.Show("Cleared all data!");
            Update_ListCustomerInfo();
            LoadListCustomerInfo();
            LoadStatictics();
        }

        public void LoadStatictics()
        {
            DateTime moment = DateTime.Now;
            _Listbills = new ObservableCollection<bill>(DataProvider.Ins.DB.bills);
            _Listbooking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            string monbongda = "bongda";
            string monbongchuyen = "bongchuyen";
            string monbongro = "bongro";
            ///Income For every sport all time
            decimal allincome = _Listbills.Select(x => x.totalmoney).Sum();
            Alltimerevenue = allincome.ToString("F");
            AlltimerevenueUSD = (allincome / 23035).ToString("F");
            ///Income for every sport this month
            IEnumerable<bill> _ListBillThisMonth = from a in _Listbills
                                                   join b in _Listbooking on a.idBookingInfo equals b.id
                                                   where b.datePlay.ToString("MM") == moment.ToString("MM")
                                                   select a;
            decimal thismonth = _ListBillThisMonth.Select(y => y.totalmoney).Sum();
            ThisMonthRevenue = thismonth.ToString("F");
            /////Income for football
            IEnumerable<bill> _Listfootball = from a in _Listbills
                                              join b in _Listbooking on a.idBookingInfo equals b.id
                                              join c in _Listfields on b.idField equals c.id
                                              join d in _Listfieldtypes on c.idType equals d.id
                                              where d.name == monbongda
                                              select a;
            decimal allincomefootball = _Listfootball.Select(y => y.totalmoney).Sum();


            ///Income for volleyball
            IEnumerable<bill> _Listvolleyball = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongchuyen
                                                select a;
            decimal allincomevolleyball = _Listvolleyball.Select(y => y.totalmoney).Sum();


            ///Income for basketball
            IEnumerable<bill> _Listbasketball = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongro
                                                select a;
            decimal allincomebasketball = _Listbasketball.Select(y => y.totalmoney).Sum();
            ///Imcome for football quarter 1 of the year
            var quart1 = new[] { "01", "02", "03" };
            IEnumerable<bill> _ListfootballQ1 = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongda
                                                where quart1.Contains(b.datePlay.ToString("MM"))
                                                select a;
            decimal incomefootballQ1 = _ListfootballQ1.Select(y => y.totalmoney).Sum();
            ///Imcome for football quarter 2 of the year
            var quart2 = new[] { "04", "05", "06" };
            IEnumerable<bill> _ListfootballQ2 = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongda
                                                where quart2.Contains(b.datePlay.ToString("MM"))
                                                select a;
            decimal incomefootballQ2 = _ListfootballQ2.Select(y => y.totalmoney).Sum();
            ///Imcome for football quarter 3 of the year
            var quart3 = new[] { "07", "08", "09" };
            IEnumerable<bill> _ListfootballQ3 = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongda
                                                where quart3.Contains(b.datePlay.ToString("MM"))
                                                select a;
            decimal incomefootballQ3 = _ListfootballQ3.Select(y => y.totalmoney).Sum();
            ///Imcome for football quarter 4 of the year
            var quart4 = new[] { "10", "11", "12" };
            IEnumerable<bill> _ListfootballQ4 = from a in _Listbills
                                                join b in _Listbooking on a.idBookingInfo equals b.id
                                                join c in _Listfields on b.idField equals c.id
                                                join d in _Listfieldtypes on c.idType equals d.id
                                                where d.name == monbongda
                                                where quart4.Contains(b.datePlay.ToString("MM"))
                                                select a;
            decimal incomefootballQ4 = _ListfootballQ4.Select(y => y.totalmoney).Sum();
            ///Imcome for VOLLEYBALL quarter 1 of the year          
            IEnumerable<bill> _ListVolleyballQ1 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongchuyen
                                                  where quart1.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomevolleyballQ1 = _ListVolleyballQ1.Select(y => y.totalmoney).Sum();
            ///Imcome for VOLLEYBALL quarter 2 of the year
            IEnumerable<bill> _ListVolleyballQ2 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongchuyen
                                                  where quart2.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomevolleyballQ2 = _ListVolleyballQ2.Select(y => y.totalmoney).Sum();
            ///Imcome for VOLLEYBALL quarter 3 of the year
            IEnumerable<bill> _ListVolleyballQ3 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongchuyen
                                                  where quart3.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomevolleyballQ3 = _ListVolleyballQ3.Select(y => y.totalmoney).Sum();
            ///Imcome for VOLLEYBALL quarter 4 of the year
            IEnumerable<bill> _ListVolleyballQ4 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongchuyen
                                                  where quart4.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomevolleyballQ4 = _ListVolleyballQ4.Select(y => y.totalmoney).Sum();
            ///Imcome for BASKETBALL quarter 1 of the year
            IEnumerable<bill> _ListBasketballQ1 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongro
                                                  where quart1.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomebasketballQ1 = _ListBasketballQ1.Select(y => y.totalmoney).Sum();
            ///Imcome for BASKETBALL quarter 2 of the year
            IEnumerable<bill> _ListBasketballQ2 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongro
                                                  where quart2.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomebasketballQ2 = _ListBasketballQ2.Select(y => y.totalmoney).Sum();
            ///Imcome for BASKETBALL quarter 3 of the year
            IEnumerable<bill> _ListBasketballQ3 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongro
                                                  where quart3.Contains(b.datePlay.ToString("MM"))
                                                  select a;
            decimal incomebasketballQ3 = _ListBasketballQ3.Select(y => y.totalmoney).Sum();
            ///Imcome for BASKETBALL quarter 4 of the year
            IEnumerable<bill> _ListBasketballQ4 = from a in _Listbills
                                                  join b in _Listbooking on a.idBookingInfo equals b.id
                                                  join c in _Listfields on b.idField equals c.id
                                                  join d in _Listfieldtypes on c.idType equals d.id
                                                  where d.name == monbongro
                                                  where quart4.Contains(b.datePlay.ToString("MM"))
                                                  select a;

            decimal incomebasketballQ4 = _ListBasketballQ4.Select(y => y.totalmoney).Sum();


            _SeriesCollection.Clear();
            _SeriesCollection1.Clear();
            //Add Football Stactictisc to PieChart
            var footballseries = new PieSeries
            {
                Title = "Football",
                Values = new ChartValues<ObservableValue> { new ObservableValue(decimal.ToDouble(allincomefootball)) },
                DataLabels = true,
                FontSize = 16,
                LabelPoint = ChartPoint => string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation)
            };
            _SeriesCollection.Add(footballseries);


            //Add Volleyball Stactictisc to PieChart
            var volleyballseries = new PieSeries
            {
                Title = "Volleyball",
                Values = new ChartValues<ObservableValue> { new ObservableValue(decimal.ToDouble(allincomevolleyball)) },
                DataLabels = true,
                FontSize = 16,
                LabelPoint = ChartPoint => string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation)
            };
            _SeriesCollection.Add(volleyballseries);

            //Add basketball Stactictisc to PieChart
            var basketballseries = new PieSeries
            {
                Title = "Basketball",
                Values = new ChartValues<ObservableValue> { new ObservableValue(decimal.ToDouble(allincomebasketball)) },
                DataLabels = true,
                FontSize = 16,
                LabelPoint = ChartPoint => string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation)
            };
            _SeriesCollection.Add(basketballseries);


            var footballline = new LineSeries
            {
                Title = "Football",
                Values = new ChartValues<double> { decimal.ToDouble(incomefootballQ1 / 23035), decimal.ToDouble(incomefootballQ2 / 23035), decimal.ToDouble(incomefootballQ3 / 23035), decimal.ToDouble(incomefootballQ4 / 23035) }
            };
            _SeriesCollection1.Add(footballline);
            var volleyballline = new LineSeries
            {
                Title = "Volleyball",
                Values = new ChartValues<double> { decimal.ToDouble(incomevolleyballQ1 / 23035), decimal.ToDouble(incomevolleyballQ2 / 23035), decimal.ToDouble(incomevolleyballQ3 / 23035), decimal.ToDouble(incomevolleyballQ4 / 23035) }
            };
            _SeriesCollection1.Add(volleyballline);
            var basketballline = new LineSeries
            {
                Title = "Basketball",
                Values = new ChartValues<double> { decimal.ToDouble(incomebasketballQ1 / 23035), decimal.ToDouble(incomebasketballQ2 / 23035), decimal.ToDouble(incomebasketballQ3 / 23035), decimal.ToDouble(incomebasketballQ4 / 23035) }
            };
            _SeriesCollection1.Add(basketballline);
            Labels = new[] { "Quarter1", "Quarter2", "Quarter3", "Quarter4" };
            YFormatter = value => value.ToString("C");


        }


        private void LoadListCustomerInfo()
        {

            var temp_bookingInfo = DataProvider.Ins.DB.bookingInfoes;
            var temp_billInfo = DataProvider.Ins.DB.bills;
            ObservableCollection<BaseCustomerInfo> temp_listCusInfo = new ObservableCollection<BaseCustomerInfo>();
            if (temp_billInfo == null)
            {
                return;
            }
            //Adding Customer info in to ListCustomerInfo
            foreach (var item_bill in temp_billInfo)
            {
                var temp_Cusinfo = new BaseCustomerInfo();
                foreach (var item_booking in temp_bookingInfo)
                {
                    if (item_booking.id == item_bill.idBookingInfo)
                    {
                        temp_Cusinfo.Baseinfo_CusName = item_booking.Customer_name;
                        temp_Cusinfo.Baseinfo_CusPhoneNum = item_booking.Customer_PhoneNum.ToString();
                        temp_Cusinfo.Baseinfo_SumBillAmount = 1;
                        temp_Cusinfo.Baseinfo_SumCusMoneyAmount = decimal.ToInt32(item_bill.totalmoney);
                        temp_Cusinfo.Baseinfo_TypeCus = "Level1";
                        temp_listCusInfo.Add(temp_Cusinfo);
                    }
                }

            }

            // Bill count 

            List<BaseCustomerInfo> temp_list1 = new List<BaseCustomerInfo>();
            List<BaseCustomerInfo> temp_list2 = new List<BaseCustomerInfo>();
            List<BaseCustomerInfo> temp_list3 = new List<BaseCustomerInfo>();

            temp_list2 = temp_listCusInfo.ToList();
            temp_list1 = temp_listCusInfo.ToList();

            for (int i = 0; i < temp_list1.Count(); i++)
            {
                int total1 = temp_list1[i].Baseinfo_SumCusMoneyAmount;
                int billnum = 1;
                for (int j = i; j < temp_list2.Count(); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    else
                    {
                        if (temp_list1[i].Baseinfo_CusName == temp_list2[j].Baseinfo_CusName && temp_list1[i].Baseinfo_CusPhoneNum == temp_list2[j].Baseinfo_CusPhoneNum)
                        {
                            total1 += temp_list2[j].Baseinfo_SumCusMoneyAmount;
                            billnum++;
                            temp_list2.RemoveAt(j);
                        }
                    }
                }
                BaseCustomerInfo adding = new BaseCustomerInfo();
                adding = temp_list1[i];
                adding.Baseinfo_SumCusMoneyAmount = total1;
                adding.Baseinfo_SumBillAmount = billnum;
                temp_list3.Add(adding);
            }
            foreach (var item in temp_list2)
            {
                _ListCustomerInfo.Add(item);
            }



            //Setting STT, member lv
            foreach (var item in _ListCustomerInfo.ToList())
            {
                if (item.Baseinfo_SumCusMoneyAmount >= 1000000)
                {
                    item.Baseinfo_TypeCus = "Level 2";
                }
                if (item.Baseinfo_SumCusMoneyAmount >= 3000000)
                {
                    item.Baseinfo_TypeCus = "Level 3";
                }
                if (item.Baseinfo_SumCusMoneyAmount >= 5000000)
                {
                    item.Baseinfo_TypeCus = "VIP";
                }
            }
            for (int i = 0; i < _ListCustomerInfo.ToList().Count(); i++)
            {
                _ListCustomerInfo[i].STT = i + 1;
            }
        }

        private void Update_ListCustomerInfo()
        {
            if (_ListCustomerInfo == null)
            {
                return;
            }
            foreach (var item in _ListCustomerInfo.ToList())
            {
                _ListCustomerInfo.Remove(item);
            }
        }


        private void f_Open_Bill_Report()
        {
            Bill_Report rp = new Bill_Report();
            rp.Show();
        }

        private void ChooseImage(Grid parameter)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Chọn ảnh";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageFileName = op.FileName;
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imageFileName);
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                parameter.Background = imageBrush;
                if (parameter.Children.Count > 1)
                {
                    parameter.Children[0].Visibility = Visibility.Hidden;
                    parameter.Children[2].Visibility = Visibility.Hidden;

                }
            }
        }



        //Order
        private bool CanExecuted(object sender)
        {
            return true;

        }

        private void AddExecuted(object sender)
        {


            good good = sender as good;
            buyingInfo buys = new buyingInfo();
            buys.good = new good();
            buys.good = good;

            buys.idGood = good.id;
            foreach (var item in Listgood)
            {
                if (item.id == buys.idGood)
                {
                    buys.orderprice = item.price;
                }
            }
            buys.quantity = 1;
            Listorder.Add(buys);

            total = Calc();




        }
        private void DelExecuted(object sender)
        {

            if (sender is buyingInfo)
            {
                Listorder.Remove(sender as buyingInfo);
                total = Calc();
            }
        }
        public decimal? Calc()
        {
            return Listorder.Sum(p => p.quantity * p.good.price);
        }

        private void Minus_Executed(object sender)
        {
            buyingInfo order = sender as buyingInfo;
            BaseGood buys = new BaseGood();
            buys.g_basebuying = new buyingInfo();
            buys.g_basegood = new good();
            buys.g_basebuying = order;
            order.quantity--;
            foreach (var item in Listgood)
            {
                if (item.id == order.idGood)
                {
                    order.orderprice = order.quantity * item.price;
                }

            }

            total = Calc();
        }
        private void Plus_Executed(object sender)
        {

            buyingInfo order = sender as buyingInfo;
            BaseGood buys = new BaseGood();
            buys.g_basebuying = new buyingInfo();
            buys.g_basegood = new good();
            buys.g_basebuying = order;
            order.quantity++;
            foreach (var item in Listgood)
            {
                if (item.id == order.idGood)
                {
                    order.orderprice = order.quantity * item.price;
                }

            }



            total = Calc();
        }

        private bool Plus_CanExecuted(object obj)
        {
            return true;
        }

        private bool Minus_CanExecuted(object obj)
        {
            bool ret = false;
            if (obj is buyingInfo)
            {
                buyingInfo order = obj as buyingInfo;
                if (order.quantity > 1)
                {
                    ret = true;
                }
            }
            return ret;
        }
        internal void LoadStorageData()
        {

            Listgood = new ObservableCollection<good>();
            var listgood = DataProvider.Ins.DB.goods;
            foreach (var item in listgood)
            {
                good Storage = new good();

                Storage = item;

                Listgood.Add(Storage);
            }

        }



        private void ShowWindowFuntion_VL()
        {
            Volleyball_Court_Bill Volleyball_Bill = new Volleyball_Court_Bill();
            Volleyball_Bill.ShowDialog();
            Update_ListCustomerInfo();
            LoadListCustomerInfo();
        }

        private void ShowWindowFuntion_BK()
        {
            Basketball_Field_Bill basketball_bill = new Basketball_Field_Bill();
            basketball_bill.ShowDialog();
            Update_ListCustomerInfo();
            LoadListCustomerInfo();
        }

        public void ShowWindowFuntion_FB()
        {
            Football_Field_Bill football_bill = new Football_Field_Bill();
            football_bill.ShowDialog();
            Update_ListCustomerInfo();
            LoadListCustomerInfo();
        }
        public void ShowFootballFieldFunction()
        {
            SoccerField soccerField = new SoccerField();
            soccerField.ShowDialog();
        }
        public void ShowVolleyballFieldFunction()
        {
            VolleyballField volleyballField = new VolleyballField();
            volleyballField.ShowDialog();
        }
        public void ShowBasketballFieldFuction()
        {
            BasketballField basketballField = new BasketballField();
            basketballField.ShowDialog();
        }






    }



}
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



        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand _ShowWindowCommand_FB { get; set; }
        public ICommand ShowWindowCommand_BK { get; set; }
        public ICommand ShowWindowCommand_VL { get; set; }
        public ICommand ShowFootballFieldCommand { get; set; }
        public ICommand ShowVolleyballFieldCommand { get; set; }
        public ICommand ShowBasketballFieldCommand { get; set; }
        public ICommand LogoutCommand { get; set; }


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

        public MainViewModel()
        {
            _Listbooking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            _Listbuying = new ObservableCollection<buyingInfo>(DataProvider.Ins.DB.buyingInfoes);
            _Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
            _Listorder = new ObservableCollection<buyingInfo>();

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                p.Show();
                LoadStorageData();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {

                }
                else
                {
                    p.Close();
                }

            }); 
            _ShowWindowCommand_FB = new RelayCommand<object>((parameter) => true, (parameter) => _ShowWindowFuntion_FB());
            ShowWindowCommand_BK = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_BK());
            ShowWindowCommand_VL = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_VL());
            ShowFootballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowFootballFieldFunction());
            ShowVolleyballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowVolleyballFieldFunction());
            ShowBasketballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowBasketballFieldFuction());
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ChooseImage(parameter));
            OpenBillReportWindow = new RelayCommand<object>((parameter) => true, (parameter) => f_Open_Bill_Report());
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
                }
                else
                {
                    p.Close();
                }
            });

            
        

        

 
            
            // Add goods
            addCommand = new RelayCommand<object>((parameter) =>
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
            }, (parameter) =>
            {
                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                byte[] imgByteArr;
                imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
                var good = new good() { name = namegood, id = idgood, price = pricegood,unit=unitgood,imageFile=imgByteArr };
                DataProvider.Ins.DB.goods.Add(good);
                DataProvider.Ins.DB.SaveChanges();
                Listgood.Add(good);

            });

            //Edit goods
            editCommand = new RelayCommand<object>((parameter) =>
            {

                if (string.IsNullOrEmpty(namegood)/*||SelectedItem==null*/)
                    return false;
                var nameList = DataProvider.Ins.DB.goods.Where(p => p.id == idgood);
                if (nameList != null && nameList.Count() != 0)
                    return true;
                return false;
            }, (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận sửa hàng hóa?", "Thông báo", MessageBoxButton.YesNo);
                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                //if (result == MessageBoxResult.Yes)
                //{
                //    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                //    good.name = namegood;
                //    good.price = pricegood;
                //    good.quantity = quantitygood;
                //    good.unit = unitgood;

                //    DataProvider.Ins.DB.SaveChanges();
                //}
                
            });

            //Delete goods
            deleteCommand = new RelayCommand<object>((parameter) => true,
            (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận xóa hàng hóa?", "Thông báo", MessageBoxButton.YesNo);

                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                if (result == MessageBoxResult.Yes)
                {
                    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    DataProvider.Ins.DB.goods.Remove(good);
                    DataProvider.Ins.DB.SaveChanges();
                    Listgood.Remove(good);
                }
            });

            OrderCommand = new RelayCommand<object>((parameter) => true,
            (parameter) =>
            {
                if(FieldSelectedItem==null)
                {
                    MessageBox.Show("Vui lòng chọn sân!!");
                }
                else { 
                MessageBoxResult result = MessageBox.Show("Xác nhận đặt hàng?", "Thông báo", MessageBoxButton.YesNo);


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
                    parameter.Children.Remove(parameter.Children[0]);
                    parameter.Children.Remove(parameter.Children[1]);
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
            foreach(var item in Listgood)
            {
                if(item.id==buys.idGood)
                {
                    buys.orderprice = item.price;
                }
            }
            buys.quantity = 1;
            Listorder.Add(buys);

            total = Calc() ;




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
            order.quantity = buys.g_basebuying.quantity--;
            order.good.price = buys.g_basegood.price * buys.g_basebuying.quantity;


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
                if(item.id==order.idGood)
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
        }

        private void ShowWindowFuntion_BK()
        {
            Basketball_Field_Bill basketball_bill = new Basketball_Field_Bill();
            basketball_bill.ShowDialog();
        }

        public void _ShowWindowFuntion_FB()
        {
            Football_Field_Bill football_bill = new Football_Field_Bill();
            football_bill.ShowDialog();
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

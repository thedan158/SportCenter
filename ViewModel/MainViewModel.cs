using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SportCenter.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<good> _Listgood;
        public ObservableCollection<good> Listgood { get => _Listgood; set { _Listgood = value; OnPropertyChanged(); } }

        private int _idgood;
        public int idgood { get => _idgood; set { _idgood = value; OnPropertyChanged(); } }

        private string _namegood;
        public string namegood { get => _namegood; set { _namegood = value; OnPropertyChanged(); } }

        private int? _pricegood;
        public int? pricegood { get => _pricegood; set { _pricegood = value; OnPropertyChanged(); } }

        private int? _quantitygood;
        public int? quantitygood { get => _quantitygood; set { _quantitygood = value; OnPropertyChanged(); } }

        private string _unitgood;
        public string unitgood { get => _unitgood; set { _unitgood = value; OnPropertyChanged(); } }

        private ObservableCollection<BaseCustomerInfo> _ListCustomerInfo;
        public ObservableCollection<BaseCustomerInfo> ListCustomerInfo { get => _ListCustomerInfo; set { _ListCustomerInfo = value; OnPropertyChanged(); } }


        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand _ShowWindowCommand_FB { get; set; }
        public ICommand ShowWindowCommand_BK { get; set; }
        public ICommand ShowWindowCommand_VL { get; set; }
        public ICommand ShowFootballFieldCommand { get; set; }
        public ICommand ShowVolleyballFieldCommand { get; set; }
        public ICommand ShowBasketballFieldCommand { get; set; }
        public ICommand addGoodCommand { get; set; }
        public ICommand OpenBillReportWindow { get; set; }

        public ICommand LogoutCommand { get; set; }

        // Good VM
        public ICommand addCommand { get; set; } 
        public ICommand editCommand { get; set; }
        public ICommand deleteCommand { get; set; }

        private good _SelectedItem;
        //public good SelectedItem
        //{
        //    get => _SelectedItem;
        //    set
        //    {
        //        _SelectedItem = value;
        //        OnPropertyChanged();
        //        if (SelectedItem != null)
        //        {
        //            idgood = SelectedItem.id;
        //            namegood = SelectedItem.name;
        //            pricegood = SelectedItem.price;
        //            quantitygood = SelectedItem.quantity;
        //            unitgood = SelectedItem.unit;
        //        }

        //    }
        //}

        

       
        
        // mọi thứ xử lý sẽ nằm trong này
       
           
                public MainViewModel()
        {
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
                    LoadTonKhoData();
                    LoadListCustomerInfo();
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
            addGoodCommand = new RelayCommand<object>((parameter) => true, (parameter) => AddGoodCommand());
            OpenBillReportWindow = new RelayCommand<object>((parameter) => true, (parameter) => f_Open_Bill_Report());
        }

        private void LoadListCustomerInfo()
        {
            ListCustomerInfo = new ObservableCollection<BaseCustomerInfo>();
            var temp_bookingInfo = DataProvider.Ins.DB.bookingInfoes;
            var temp_billInfo = DataProvider.Ins.DB.bills;
            ObservableCollection<BaseCustomerInfo> temp_listCusInfo = new ObservableCollection<BaseCustomerInfo>();
            if(temp_billInfo == null)
            {
                return;
            }
            //Adding Customer info in to ListCustomerInfo
            foreach (var item_bill in temp_billInfo) 
            {
                var temp_Cusinfo = new BaseCustomerInfo();
                temp_Cusinfo.Baseinfo_SumCusMoneyAmount = Decimal.ToInt32(item_bill.totalmoney.Value);
                foreach(var item_booking in temp_bookingInfo)
                {
                    if (item_booking.id == item_bill.idBookingInfo)
                    {
                        temp_Cusinfo.Baseinfo_CusName = item_booking.Customer_name;
                        temp_Cusinfo.Baseinfo_CusPhoneNum = item_booking.Customer_PhoneNum.ToString();
                        temp_Cusinfo.Baseinfo_SumBillAmount = 1;
                        temp_Cusinfo.Baseinfo_SumCusMoneyAmount = Decimal.ToInt32(item_bill.totalmoney.Value);
                        temp_Cusinfo.Baseinfo_TypeCus = "Lever1";
                        temp_listCusInfo.Add(temp_Cusinfo);
                    }
                }

            }
            // Bill count 
            foreach(var item in temp_listCusInfo)
            {
                foreach(var item_booking in temp_bookingInfo)
                {
                    if (item_booking.Status == "Pay")
                    {
                        if (item.Baseinfo_CusName == item_booking.Customer_name)
                        {
                            if (item.Baseinfo_CusPhoneNum == item_booking.Customer_PhoneNum.ToString())
                            {
                                item.Baseinfo_SumBillAmount++;

                            }
                        }
                    }
                    
                }
            }

            List<BaseCustomerInfo> temp_list1 = new List<BaseCustomerInfo>();
            List<BaseCustomerInfo> temp_list2 = new List<BaseCustomerInfo>();
            temp_list1 = temp_listCusInfo.ToList();
            temp_list2 = temp_listCusInfo.ToList();
            for (int i = 0; i < temp_list1.Count(); i++)
            {
                for(int j = 0;j < temp_list1.Count(); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    else
                    {
                        if(temp_list1[i].Baseinfo_CusName == temp_list1[j].Baseinfo_CusName)
                        {
                            if (temp_list1[i].Baseinfo_CusPhoneNum == temp_list1[j].Baseinfo_CusPhoneNum)
                            {
                                temp_list1[i].Baseinfo_SumBillAmount++;
                                temp_list1[i].Baseinfo_SumCusMoneyAmount += temp_list1[j].Baseinfo_SumCusMoneyAmount;

                            }
                        }
                    }
                }
                
            }


        }

        private void f_Open_Bill_Report()
        {
            Bill_Report rp = new Bill_Report();
            rp.Show();
        }

        private void AddGoodCommand()
        {
            Add_Good add_Good = new Add_Good();
            add_Good.ShowDialog();
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
                    LoadTonKhoData();
                    
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

                var good = new good() { name = namegood, id = idgood, price = pricegood,unit=unitgood/*, quantity = quantitygood*/ };
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
            deleteCommand = new RelayCommand<object>((parameter) =>true, 
            (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận xóa hàng hóa?","Thông báo", MessageBoxButton.YesNo);

                //Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                //if (result == MessageBoxResult.Yes)
                //{
                //    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                //    DataProvider.Ins.DB.goods.Remove(good);


                //    DataProvider.Ins.DB.SaveChanges();
                //    Listgood.Remove(good);
                //}
            });

        }
       
        
        internal void LoadTonKhoData()
        {
            
            Listgood = new ObservableCollection<good>();
            var listgood = DataProvider.Ins.DB.goods;
            foreach (var item in listgood)
            {
                good tonkho = new good();
               
                tonkho = item;

                Listgood.Add(tonkho);
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

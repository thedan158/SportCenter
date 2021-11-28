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

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand _ShowWindowCommand_FB { get; set; }
        public ICommand ShowWindowCommand_BK { get; set; }
        public ICommand ShowWindowCommand_VL { get; set; }
        public ICommand ShowFootballFieldCommand { get; set; }
        public ICommand ShowVolleyballFieldCommand { get; set; }
        public ICommand ShowBasketballFieldCommand { get; set; }

        // Good VM
        public ICommand addCommand { get; set; } 
        public ICommand editCommand { get; set; }
        public ICommand deleteCommand { get; set; }

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
                    quantitygood = SelectedItem.quantity;
                    unitgood = SelectedItem.unit;
                }

            }
        }

        


        public ICommand addGoodCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
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
                }
                else
                {
                    p.Close();
                }

            }
              );
            _ShowWindowCommand_FB = new RelayCommand<object>((parameter) => true, (parameter) => _ShowWindowFuntion_FB());
            ShowWindowCommand_BK = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_BK());
            ShowWindowCommand_VL = new RelayCommand<object>((parameter) => true, (parameter) => ShowWindowFuntion_VL());
            ShowFootballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowFootballFieldFunction());
            ShowVolleyballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowVolleyballFieldFunction());
            ShowBasketballFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowBasketballFieldFuction());


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

                var good = new good() { name = namegood, id = idgood, price = pricegood,unit=unitgood, quantity = quantitygood };
                DataProvider.Ins.DB.goods.Add(good);
                DataProvider.Ins.DB.SaveChanges();
                Listgood.Add(good);

            });

            //Edit goods
            editCommand = new RelayCommand<object>((parameter) =>
            {

                if (string.IsNullOrEmpty(namegood)||SelectedItem==null)
                    return false;
                var nameList = DataProvider.Ins.DB.goods.Where(p => p.id == idgood);
                if (nameList != null && nameList.Count() != 0)
                    return true;
                return false;
            }, (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận sửa hàng hóa?", "Thông báo", MessageBoxButton.YesNo);
                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                if (result == MessageBoxResult.Yes)
                {
                    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    good.name = namegood;
                    good.price = pricegood;
                    good.quantity = quantitygood;
                    good.unit = unitgood;

                    DataProvider.Ins.DB.SaveChanges();
                }
                
            });

            //Delete goods
            deleteCommand = new RelayCommand<object>((parameter) =>true, 
            (parameter) =>
            {
                MessageBoxResult result = MessageBox.Show("Xác nhận xóa hàng hóa?","Thông báo", MessageBoxButton.YesNo);

                Listgood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
                if (result == MessageBoxResult.Yes)
                {
                    var good = DataProvider.Ins.DB.goods.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                    DataProvider.Ins.DB.goods.Remove(good);


                    DataProvider.Ins.DB.SaveChanges();
                    Listgood.Remove(good);

                }
            });

        }
        
        

           
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
            }     );
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

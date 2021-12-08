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
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }
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
            addGoodCommand = new RelayCommand<object>((parameter) => true, (parameter) => AddGoodCommand());
            OpenBillReportWindow = new RelayCommand<object>((parameter) => true, (parameter) => f_Open_Bill_Report());
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

        }
        void LoadTonKhoData()
        {
            
            TonKhoList = new ObservableCollection<TonKho>();
            var listgood = DataProvider.Ins.DB.goods;
            foreach (var item in listgood)
            {
                TonKho tonkho = new TonKho();
               
                tonkho.good = item;

                TonKhoList.Add(tonkho);
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

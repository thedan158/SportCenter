using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    public class PaymentTemplateViewModel : BillViewModel
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        private int _IdBooking;
        public int IdBooking
        {
            get => _IdBooking;
            set
            {
                _IdBooking = value;
                OnPropertyChanged();
            }
        }

        private int _Totalmoney;
        public int TotalMoney
        {
            get => _Totalmoney;
            set
            {
                _Totalmoney = value;
                OnPropertyChanged();
            }
        }
        public ICommand Confirm_Btn { get; set; }


        public PaymentTemplateViewModel()
        {
            List<int> id_bill = DataProvider.Ins.DB.bills.Select(x => x.id).ToList();
            Confirm_Btn = new RelayCommand<object>((p) =>
            {
                var displayidbill = DataProvider.Ins.DB.bills.Where(x => x.id == Id);

                if (displayidbill == null || displayidbill.Count() == 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            { 
                var obj = new bill() { id = Id, idBookingInfo = IdBooking, totalmoney = TotalMoney };
                DataProvider.Ins.DB.bills.Add(obj);
                DataProvider.Ins.DB.SaveChanges();
            });


        }
        

        

        
    }
}

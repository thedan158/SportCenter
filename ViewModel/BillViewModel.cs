using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ListBillViewModel> _ListBill;
        public IEnumerable<ListBillViewModel> ListBill => _ListBill;

        private readonly ObservableCollection<ListBillViewModel> _Listfield;
        public IEnumerable<ListBillViewModel> Listfield => _Listfield;


        public ICommand Show_PaymentTemplate { get; set; }

        public BillViewModel()
        {
            _ListBill = new ObservableCollection<ListBillViewModel>();
            _Listfield = new ObservableCollection<ListBillViewModel>();

            Show_PaymentTemplate = new RelayCommand<object>((parameter) => true, (parameter) => Show_Payment());
            

        }

        private void Show_Payment()
        {
            PayMent_tem Pay = new PayMent_tem();
            Pay.Show();
        }
    }
}

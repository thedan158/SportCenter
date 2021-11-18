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
    public class PaymentTemplateViewModel : BaseViewModel
    {
        public ICommand Confirm_Btn { get; }

        public PaymentTemplateViewModel()
        {
            Confirm_Btn = new RelayCommand<object>((parameter) => true, (parameter) => Confirm_Btn_WD());
        }

        private void Confirm_Btn_WD()
        {
            MessageBox.Show("Đã xác nhận thanh toán", "Xác nhận");
            
            
        }
    }
}

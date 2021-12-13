using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SportCenter.Model;
using SportCenter.ViewModel;

namespace SportCenter
{
    /// <summary>
    /// Interaction logic for ProductDetailsControl.xaml
    /// </summary>
    public partial class ProductDetailsControl : UserControl
    {
        public ProductDetailsControl()
        {
            InitializeComponent();
        }

        int _totalmoney = 0;

        //private void tb_main_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    int totalmoney = Int32.Parse(txbPrice.Text);
        //    var tbx = sender as TextBox;
        //    tbx.Text = decimal.Parse(this.tb_main.Text).ToString();
        //    totalmoney += Int32.Parse(tb_main.Text);

        //}

        //private void tb_main_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {

        //        _totalmoney = Int32.Parse(txbPriceSingle.Text) * Int32.Parse(tb_main.Text);
        //        txbPrice.Text = _totalmoney.ToString();
        //        MainViewModel vm = this.DataContext as MainViewModel;
        //        vm = new MainViewModel();
        //        vm.total = vm.Calc();
               
        //    }
        //}

        private void tb_main_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

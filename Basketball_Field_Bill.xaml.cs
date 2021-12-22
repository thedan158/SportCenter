using SportCenter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Football_Field_Bill.xaml
    /// </summary>
    public partial class Football_Field_Bill : Window
    {
        public Football_Field_Bill()
        {
            InitializeComponent();
            (this.DataContext as BasketballFieldViewModel).Update_DatagridView12();
            (this.DataContext as BasketballFieldViewModel).Load_List_footballPayment();


        }

       
    }
}

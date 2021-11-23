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
    /// Interaction logic for PayMent_tem.xaml
    /// </summary>
    public partial class PayMent_tem : Window
    {
        public PayMent_tem()
        {
            InitializeComponent();
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

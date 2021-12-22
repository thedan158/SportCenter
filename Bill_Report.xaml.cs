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
    /// Interaction logic for Bill_Report.xaml
    /// </summary>
    public partial class Bill_Report : Window
    {
        public Bill_Report()
        {
            InitializeComponent();
            (DataContext as BillReportViewModel).Update_DatagridView();
            (DataContext as BillReportViewModel).Load_DatagridView();
        }

        private void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}

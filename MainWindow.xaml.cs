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
using System.Windows.Navigation;
using System.Windows.Shapes; 
using System.Linq;
using SportCenter.Model;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SportCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //http://materialdesigninxaml.net/home
        public MainWindow() => InitializeComponent();

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        // search good
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var listgoood = new ObservableCollection<good>(DataProvider.Ins.DB.goods);
            var tbx = sender as TextBox;
            if (tbx.Text != "")
            {
                var goodList = listgoood.Where(x => x.name.ToLower().Contains(tbx.Text.ToLower()));
                
                DataGrid.ItemsSource = goodList;
            }
            else
            {
                DataGrid.ItemsSource= listgoood ;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
    }
}
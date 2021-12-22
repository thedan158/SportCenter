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
       
            public MainWindow()
            {
            
                InitializeComponent();
                
            }
        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        //Order good
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
            
        //    Button btn = sender as Button;
        //    var item = btn.DataContext as buyingInfo;
            
        //    order.Items.Add(item);
        //    if (order.Items.Count > 0)
        //    {
        //        Total += item.price;
        //        total.Text = Total.ToString() + " VNĐ";
        //        item.order_quantity = 1;
        //    }



        //}
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        ////Delete all order
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{


        //    order.Items.Clear();
        //    Total = 0;
        //    total.Text = "";
        //}

        //Delete selected item on order
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    var item = order.SelectedItem as good;
        //    order.Items.Remove(item);

        //    if (order.Items.Count > 0)
        //    {
        //        Total -= item.price;
        //        total.Text = Total.ToString() + " VNĐ";
        //    }
        //    else
        //    {
        //        Total = 0;
        //        total.Text = "";
        //    }
        //}

        // Booking
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TabItem_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as MainViewModel).ReloadBookingFunction();
        }

        private void TabItem_MouseDown_2(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
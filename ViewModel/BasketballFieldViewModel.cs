using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SportCenter.Model;

namespace SportCenter.ViewModel
{
    class BasketballFieldViewModel : BaseViewModel
    {
        private ObservableCollection<ListFieldBasketball> _ListField;
        public ObservableCollection<ListFieldBasketball> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldBasketball> List_field_bk;
        public ObservableCollection<ListFieldBasketball> _List_field_bk { get => List_field_bk; set { List_field_bk = value; OnPropertyChanged(); } }
        public BasketballFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldBasketball>();
            List_field_bk = new ObservableCollection<ListFieldBasketball>();
            Load_Listfieldbasketball();
        }

        private void Load_Listfieldbasketball()
        {
            var Temp_field = DataProvider.Ins.DB.fields;
            foreach (var item in Temp_field)
            {
                if (item.idType == 3)
                {
                    ListFieldBasketball temp = new ListFieldBasketball();
                    temp.List_FieldBasketball = item;
                    List_field_bk.Add(temp);
                }
            }
        }
    }
}

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
    class VolleyballFieldViewModel : BaseViewModel
    {
        private ObservableCollection<ListFieldVolleyball> _ListField;
        public ObservableCollection<ListFieldVolleyball> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldVolleyball> List_field_vl;
        public ObservableCollection<ListFieldVolleyball> _List_field_vl { get => List_field_vl; set { List_field_vl = value; OnPropertyChanged(); } }
        public VolleyballFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldVolleyball>();
            List_field_vl = new ObservableCollection<ListFieldVolleyball>();
            Load_Listfieldvolleyball();
        }

        private void Load_Listfieldvolleyball()
        {
            var Temp_field = DataProvider.Ins.DB.fields;
            foreach (var item in Temp_field)
            {
                if (item.idType == 2)
                {
                    ListFieldVolleyball temp = new ListFieldVolleyball();
                    temp.List_FieldVolleyball = item;
                    List_field_vl.Add(temp);
                }
            }
        }
    }
}

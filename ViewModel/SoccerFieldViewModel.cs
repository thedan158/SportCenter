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
    class SoccerFieldViewModel : BaseViewModel
    {
        private ObservableCollection<ListFieldSoccer> _ListField;
        public ObservableCollection<ListFieldSoccer> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldSoccer> List_field_sc;
        public ObservableCollection<ListFieldSoccer> _List_field_sc { get => List_field_sc; set { List_field_sc = value; OnPropertyChanged(); } }
        public SoccerFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldSoccer>();
            List_field_sc = new ObservableCollection<ListFieldSoccer>();
            Load_Listfieldsoccer();
        }

        private void Load_Listfieldsoccer()
        {
            var Temp_field = DataProvider.Ins.DB.fields;
            foreach (var item in Temp_field)
            {
                if (item.idType == 1)
                {
                    ListFieldSoccer temp = new ListFieldSoccer();
                    temp.List_FieldSoccer = item;
                    List_field_sc.Add(temp);
                }
            }
        }
    }
}

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

    public class SoccerFieldViewModel : BaseViewModel
    {
        public ICommand ShowAddCommand { get; set; }
        public ICommand ShowDeleteCommand { get; set; }
        public ICommand ShowBookingCommand { get; set; }
        public ICommand DelfieldCommand => new RelayCommand<object>(CanDel, Del);
        public ICommand ShowInfofieldCommand => new RelayCommand<object>(CanOpen, Open);
        protected int _idfieldbooking;
        public ICommand ShowEditFieldCommand { get; set; }
        public ICommand EditField { get; set; }
        private int _idField;
        public int idField { get => _idField; set { _idField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldSoccer> _ListField;
        public ObservableCollection<ListFieldSoccer> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldSoccer> List_field_sc;
        public ObservableCollection<ListFieldSoccer> _List_field_sc { get => List_field_sc; set { List_field_sc = value; OnPropertyChanged(); } }
        private ObservableCollection<field> List_sc;
        public ObservableCollection<field> _List_sc { get => List_sc; set { List_sc = value; OnPropertyChanged(); } }
        private ObservableCollection<field> _List;
        public ObservableCollection<field> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private IEnumerable<bookingInfo> _Listbooking;
        public IEnumerable<bookingInfo> Listbooking { get => _Listbooking; set { _Listbooking = value; OnPropertyChanged(); } }
        private field _SelectedItem;

        public field SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    FieldName = SelectedItem.name;
                    FieldCondition = SelectedItem.condition;
                    
                }
            }
        }

        private string _FieldName;
        public string FieldName { get => _FieldName; set { _FieldName = value; OnPropertyChanged(); } }
        private string _FieldCondition;
        public string FieldCondition { get => _FieldCondition; set { _FieldCondition = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _ListName;
        public ObservableCollection<string> ListName { get => _ListName; set { _ListName = value; OnPropertyChanged(); } }
        private ObservableCollection<bookingInfo> _ListbookingInfo;
        public ObservableCollection<bookingInfo> ListbookingInfosc { get => _ListbookingInfo; set { _ListbookingInfo = value; OnPropertyChanged(); } }
        public SoccerFieldViewModel()
        {
            _ListbookingInfo = new ObservableCollection<bookingInfo>();
            _ListField = new ObservableCollection<ListFieldSoccer>();
            List_field_sc = new ObservableCollection<ListFieldSoccer>();
            _List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 1));
            Load_Listfieldsoccer();
            //var Listname = new ObservableCollection<string>(DataProvider.Ins.DB.fields.Where(x => x.idType == 1).Select(x => x.name));
            //foreach (var fieldname in Listname)
            //{
            //                      _Listbooking = from a in _ListbookingInfo
            //                      join b in _List on a.idField equals b.id
            //                      where fieldname == b.name
            //                      select a;
            //}

            ShowAddCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowAddFunction());
            ShowEditFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowEditFieldFunction());
            EditField = new RelayCommand<object>((p) =>
            {
                
                if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldCondition))
                    return false;
                return true;
            }, (p) =>
            {
                var field = DataProvider.Ins.DB.fields.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                field.name = FieldName;
                field.condition = FieldCondition;
                DataProvider.Ins.DB.SaveChanges();
                FieldName = null;
                FieldCondition = null;
                Update_Listfieldsoccer();
                Load_Listfieldsoccer();
                Update_Listeditsoccer();
                Load_Listeditsoccer();
            });
        }
        private void Open(object obj)
        {
            if (CanOpen(obj) == true)
            {
                var temp_obj = obj as ListFieldSoccer;
                var temp_field = temp_obj.List_FieldSoccer;
                idField = temp_field.id;
                _idfieldbooking = idField;
                InfoFieldSc info = new InfoFieldSc(idField);
                info.ShowDialog();
            }
        }

        private bool CanOpen(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void Del(object obj)
        {

            if (CanDel(obj) == true)
            {

                MessageBoxResult result = MessageBox.Show("Xác nhận xóa sân?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    List_field_sc.Remove(obj as ListFieldSoccer);
                    var abc = obj as ListFieldSoccer;
                    field xyz = abc.List_FieldSoccer;
                    DataProvider.Ins.DB.fields.Remove(xyz);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
        }

        private bool CanDel(object obj)
        {
            if (obj == null)
            { return false; }
            ListFieldSoccer _field = obj as ListFieldSoccer;
            field soccer = new field();
            soccer = _field.List_FieldSoccer;

            var _booking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            foreach (var item in List_field_sc)
            {
                if (item.List_FieldSoccer.id == _field.List_FieldSoccer.id)
                {
                    foreach (var book in _booking)
                    {
                        if (book.idField == item.List_FieldSoccer.id)
                        {
                            if (book.Status == "unpay")
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        private void ShowEditFieldFunction()
        {
            EditScField editField = new EditScField();
            editField.ShowDialog();
        }

        private void Update_Listfieldsoccer()
        {
            foreach (var item in List_field_sc.ToList())
            {
                List_field_sc.Remove(item);
            }
        }
        private void ShowAddFunction()
        {
            Add_Field add = new Add_Field();
            add.ShowDialog();
            Update_Listfieldsoccer();
            Load_Listfieldsoccer();
            Update_Listeditsoccer();
            Load_Listeditsoccer();
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
        public void Load_Listeditsoccer()
        {
            var Temp_editing = DataProvider.Ins.DB.fields.Where(x => x.idType == 1);
            foreach (var item in Temp_editing)
            {
                List.Add(item);
            }
        }
        public void Update_Listeditsoccer()
        {
            if (List == null)
            {
                return;
            }
            foreach (var item in List.ToList())
            {
                List.Remove(item);
            }
        }
    }
}
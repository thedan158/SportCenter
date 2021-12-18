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

    public class VolleyballFieldViewModel : BaseViewModel
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
        private ObservableCollection<ListFieldVolleyball> _ListField;
        public ObservableCollection<ListFieldVolleyball> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldVolleyball> List_field_vl;
        public ObservableCollection<ListFieldVolleyball> _List_field_vl { get => List_field_vl; set { List_field_vl = value; OnPropertyChanged(); } }
        private ObservableCollection<field> List_vl;
        public ObservableCollection<field> _List_vl { get => List_vl; set { List_vl = value; OnPropertyChanged(); } }
        private ObservableCollection<field> _List;
        public ObservableCollection<field> List { get => _List; set { _List = value; OnPropertyChanged(); } }
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
        public VolleyballFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldVolleyball>();
            List_field_vl = new ObservableCollection<ListFieldVolleyball>();
            _List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 2));
            Load_ListfieldVolleyball();
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
                Update_ListfieldVolleyball();
                Load_ListfieldVolleyball();
                Update_ListeditVolleyball();
                Load_ListeditVolleyball();
            });
        }
        private void Open(object obj)
        {
            if (CanOpen(obj) == true)
            {
                var temp_obj = obj as ListFieldVolleyball;
                var temp_field = temp_obj.List_FieldVolleyball;
                idField = temp_field.id;
                _idfieldbooking = idField;
                InfoFieldVl info = new InfoFieldVl(idField);
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
                    List_field_vl.Remove(obj as ListFieldVolleyball);
                    var abc = obj as ListFieldVolleyball;
                    field xyz = abc.List_FieldVolleyball;
                    DataProvider.Ins.DB.fields.Remove(xyz);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
        }

        private bool CanDel(object obj)
        {
            if (obj == null)
            { return false; }
            ListFieldVolleyball _field = obj as ListFieldVolleyball;
            field Volleyball = new field();
            Volleyball = _field.List_FieldVolleyball;

            var _booking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            foreach (var item in List_field_vl)
            {
                if (item.List_FieldVolleyball.id == _field.List_FieldVolleyball.id)
                {
                    foreach (var book in _booking)
                    {
                        if (book.idField == item.List_FieldVolleyball.id)
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
            EditVlField editField = new EditVlField();
            editField.ShowDialog();
        }

        private void Update_ListfieldVolleyball()
        {
            foreach (var item in List_field_vl.ToList())
            {
                List_field_vl.Remove(item);
            }
        }
        private void ShowAddFunction()
        {
            Add_Field add = new Add_Field();
            add.ShowDialog();
            Update_ListfieldVolleyball();
            Load_ListfieldVolleyball();
            Update_ListeditVolleyball();
            Load_ListeditVolleyball();
        }
        private void Load_ListfieldVolleyball()
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
        public void Load_ListeditVolleyball()
        {
            var Temp_editing = DataProvider.Ins.DB.fields.Where(x => x.idType == 2);
            foreach (var item in Temp_editing)
            {
                List.Add(item);
            }
        }
        public void Update_ListeditVolleyball()
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
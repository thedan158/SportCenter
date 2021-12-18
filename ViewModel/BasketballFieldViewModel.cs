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

    public class BasketballFieldViewModel : BaseViewModel
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
        private ObservableCollection<ListFieldBasketball> _ListField;
        public ObservableCollection<ListFieldBasketball> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldBasketball> List_field_bk;
        public ObservableCollection<ListFieldBasketball> _List_field_bk { get => List_field_bk; set { List_field_bk = value; OnPropertyChanged(); } }
        private ObservableCollection<field> List_bk;
        public ObservableCollection<field> _List_bk { get => List_bk; set { List_bk = value; OnPropertyChanged(); } }
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
        public BasketballFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldBasketball>();
            List_field_bk = new ObservableCollection<ListFieldBasketball>();
            _List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 3));
            Load_ListfieldBasketball();
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
                Update_ListfieldBasketball();
                Load_ListfieldBasketball();
                Update_ListeditBasketball();
                Load_ListeditBasketball();
            });
        }
        private void Open(object obj)
        {
            if (CanOpen(obj) == true)
            {
                var temp_obj = obj as ListFieldBasketball;
                var temp_field = temp_obj.List_FieldBasketball;
                idField = temp_field.id;
                _idfieldbooking = idField;
                InfoFieldBk info = new InfoFieldBk(idField);
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
                    List_field_bk.Remove(obj as ListFieldBasketball);
                    var abc = obj as ListFieldBasketball;
                    field xyz = abc.List_FieldBasketball;
                    DataProvider.Ins.DB.fields.Remove(xyz);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
        }

        private bool CanDel(object obj)
        {
            if (obj == null)
            { return false; }
            ListFieldBasketball _field = obj as ListFieldBasketball;
            field Basketball = new field();
            Basketball = _field.List_FieldBasketball;

            var _booking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            foreach (var item in List_field_bk)
            {
                if (item.List_FieldBasketball.id == _field.List_FieldBasketball.id)
                {
                    foreach (var book in _booking)
                    {
                        if (book.idField == item.List_FieldBasketball.id)
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
            EditBkField editField = new EditBkField();
            editField.ShowDialog();
        }

        private void Update_ListfieldBasketball()
        {
            foreach (var item in List_field_bk.ToList())
            {
                List_field_bk.Remove(item);
            }
        }
        private void ShowAddFunction()
        {
            Add_Field add = new Add_Field();
            add.ShowDialog();
            Update_ListfieldBasketball();
            Load_ListfieldBasketball();
            Update_ListeditBasketball();
            Load_ListeditBasketball();
        }
        private void Load_ListfieldBasketball()
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
        public void Load_ListeditBasketball()
        {
            var Temp_editing = DataProvider.Ins.DB.fields.Where(x => x.idType == 3);
            foreach (var item in Temp_editing)
            {
                List.Add(item);
            }
        }
        public void Update_ListeditBasketball()
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
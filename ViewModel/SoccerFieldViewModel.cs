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
        public ICommand ShowInfoCommand { get; set; }
        public ICommand ShowAddCommand { get; set; }
        public ICommand ShowDeleteCommand { get; set; }
        public ICommand ShowBookingCommand { get; set; }
        public ICommand DelfieldCommand => new RelayCommand<object>(CanDel, Del);
        public ICommand ShowEditFieldCommand { get; set; }
        public ICommand EditField { get; set; }

        private ObservableCollection<ListFieldSoccer> _ListField;
        public ObservableCollection<ListFieldSoccer> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldSoccer> List_field_sc;
        public ObservableCollection<ListFieldSoccer> _List_field_sc { get => List_field_sc; set { List_field_sc = value; OnPropertyChanged(); } }
        private ListFieldSoccer _SelectedItem;
        public ListFieldSoccer SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged(); if (SelectedItem != null)
                {
                    FieldName = SelectedItem.List_FieldSoccer.name;
                    FieldCondition = SelectedItem.List_FieldSoccer.condition;
                }
            }
        }
        private string _FieldName;
        public string FieldName { get => _FieldName; set { _FieldName = value; OnPropertyChanged(); } }
        private string _FieldCondition;
        public string FieldCondition { get => _FieldCondition; set { _FieldCondition = value; OnPropertyChanged(); } }
        public SoccerFieldViewModel()
        {
            _ListField = new ObservableCollection<ListFieldSoccer>();
            List_field_sc = new ObservableCollection<ListFieldSoccer>();
            Load_Listfieldsoccer();
            ShowAddCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowAddFunction());
            ShowBookingCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowBookingFunction());
            ShowDeleteCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowDeleteFunction());
            ShowInfoCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowInfoFunction());
            ShowEditFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowEditFieldFunction());
            EditField = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldCondition))
                    return false;
                var displayListname = DataProvider.Ins.DB.fields.Where(x => x.name == FieldName);
                var displayListcondition = DataProvider.Ins.DB.fields.Where(x => x.condition == FieldCondition);
                if (displayListname == null || displayListcondition == null || displayListcondition.Count() != 0 || displayListname.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var field = DataProvider.Ins.DB.fields.Where(x => x.id == SelectedItem.List_FieldSoccer.id).SingleOrDefault();
                field.name = FieldName;
                field.condition = FieldCondition;
                DataProvider.Ins.DB.SaveChanges();
            });
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
            EditField editField = new EditField();
            editField.ShowDialog();
        }

        private void ShowInfoFunction()
        {

        }

        private void ShowDeleteFunction()
        {
            DeleteField deleteField = new DeleteField();
            deleteField.ShowDialog();
        }

        private void Update_Listfieldsoccer()
        {
            foreach (var item in List_field_sc.ToList())
            {
                List_field_sc.Remove(item);
            }
        }
        private void ShowBookingFunction()
        {
            Booking booking = new Booking();
            booking.ShowDialog();
        }
        private void ShowAddFunction()
        {
            Add_Field add = new Add_Field();
            add.ShowDialog();
            Update_Listfieldsoccer();
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
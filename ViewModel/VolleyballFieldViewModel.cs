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
        public ICommand FastBookingCommand { get; set; }
        public ICommand AddBookingCommand { get; set; }
        public ICommand EditBookingCommand { get; set; }
        public ICommand DeleteBookingCommand { get; set; }
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
        private ObservableCollection<ListFieldVolleyball> List_field_Vl;
        public ObservableCollection<ListFieldVolleyball> _List_field_Vl { get => List_field_Vl; set { List_field_Vl = value; OnPropertyChanged(); } }
        private ObservableCollection<field> List_Vl;
        public ObservableCollection<field> _List_Vl { get => List_Vl; set { List_Vl = value; OnPropertyChanged(); } }
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


        private string _addcustomername;
        public string addcustomername { get => _addcustomername; set { _addcustomername = value; OnPropertyChanged(); } }
        private int? _addcustomerphone;
        public int? addcustomerphone { get => _addcustomerphone; set { _addcustomerphone = value; OnPropertyChanged(); } }
        private DateTime _addstarttime;
        public DateTime addstarttime { get => _addstarttime; set { _addstarttime = value; OnPropertyChanged(); } }
        private DateTime _addendtime;
        public DateTime addendtime { get => _addendtime; set { _addendtime = value; OnPropertyChanged(); } }
        private DateTime _adddateplay;
        public DateTime adddateplay { get => _adddateplay; set { _adddateplay = value; OnPropertyChanged(); } }
        private string _addstatus;
        public string addstatus { get => _addstatus; set { _addstatus = value; OnPropertyChanged(); } }

        private bookingInfo _SelectedItemBooking;
        public bookingInfo SelectedItemBooking
        {
            get => _SelectedItemBooking;
            set
            {
                _SelectedItemBooking = value;
                OnPropertyChanged();
                if (_SelectedItemBooking != null)
                {
                    addcustomername = _SelectedItemBooking.Customer_name;
                    addcustomerphone = _SelectedItemBooking.Customer_PhoneNum;
                    addstatus = _SelectedItemBooking.Status;
                    addstarttime = _SelectedItemBooking.start_time;
                    addendtime = _SelectedItemBooking.end_time;
                    adddateplay = _SelectedItemBooking.datePlay;
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
        public ObservableCollection<bookingInfo> ListbookingInfoVl { get => _ListbookingInfo; set { _ListbookingInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<bookingInfo> _list_with_id;
        public ObservableCollection<bookingInfo> list_with_id { get => _list_with_id; set { _list_with_id = value; OnPropertyChanged(); } }
        public VolleyballFieldViewModel()
        {
            _ListbookingInfo = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 2));
            _ListField = new ObservableCollection<ListFieldVolleyball>();
            List_field_Vl = new ObservableCollection<ListFieldVolleyball>();
            _list_with_id = new ObservableCollection<bookingInfo>();
            _List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 2));
            Update_ListbookingVolleyball();
            Load_ListbookingVolleyball();
            Load_ListfieldVolleyball();
            FastBookingCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowFastBookingFunction());
            ShowAddCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowAddFunction());
            ShowEditFieldCommand = new RelayCommand<object>((parameter) => true, (parameter) => ShowEditFieldFunction());
            EditField = new RelayCommand<object>((p) =>
            {

                if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldCondition))
                    return false;
                return true;
            }, (p) =>
            {
                foreach (var item in List)
                {
                    if (item.name == FieldName)
                    {
                        MessageBox.Show("This name already exists, please choose another name!");
                        return;
                    }
                }
                var field = DataProvider.Ins.DB.fields.Where(x => x.id == SelectedItem.id).SingleOrDefault();
                field.name = FieldName;
                field.condition = FieldCondition;
                DataProvider.Ins.DB.SaveChanges();
                FieldName = null;
                FieldCondition = null;
                Update_ListfieldVolleyball();
                Load_ListfieldVolleyball();
                Update_ListeditVolleyball();
                Load_ListeditVolleyball();
            });
            //AddBookingCommand = new RelayCommand<object>((p) =>
            //{
            //    if (string.IsNullOrEmpty(addcustomername) || string.IsNullOrEmpty(addcustomerphone.ToString()) || string.IsNullOrEmpty(adddateplay.ToString()) || string.IsNullOrEmpty(addstarttime.ToString()) || string.IsNullOrEmpty(addendtime.ToString()))
            //        return false;
            //    return true;
            //}, (p) =>
            //{
            //    var Booking = new bookingInfo() { Customer_name = addcustomername, Customer_PhoneNum = addcustomerphone, datePlay = adddateplay, start_time = addstarttime, Status = "unpay", idField = addidfield};
            //    DataProvider.Ins.DB.bookingInfoes.Add(Booking);
            //    DataProvider.Ins.DB.SaveChanges();
            //    Update_ListbookingVolleyball();
            //    Load_ListbookingVolleyball();
            //});
            EditBookingCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(addcustomername) || string.IsNullOrEmpty(addcustomerphone.ToString()) || string.IsNullOrEmpty(adddateplay.ToString()) || string.IsNullOrEmpty(addstarttime.ToString()) || string.IsNullOrEmpty(addendtime.ToString()))
                    return false;
                return true;
            }, (p) =>
            {
                var booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.id == SelectedItemBooking.id).SingleOrDefault();
                booking.Customer_name = addcustomername;
                booking.Customer_PhoneNum = addcustomerphone;
                booking.datePlay = adddateplay;
                booking.start_time = addstarttime;
                booking.end_time = addendtime;
                booking.Status = addstatus;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItemBooking.Customer_name = addcustomername;
                SelectedItemBooking.Customer_PhoneNum = addcustomerphone;
                SelectedItemBooking.datePlay = adddateplay;
                SelectedItemBooking.start_time = addstarttime;
                SelectedItemBooking.end_time = addendtime;
                SelectedItemBooking.Status = addstatus;
                addcustomername = null;
                addstatus = null;
                addcustomerphone = null;
                Update_ListbookingVolleyball();
                Load_ListbookingVolleyball();
            });
            DeleteBookingCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                var booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.id == SelectedItemBooking.id).SingleOrDefault();
                DataProvider.Ins.DB.bookingInfoes.Remove(booking);
                DataProvider.Ins.DB.SaveChanges();
                Update_ListbookingVolleyball();
                Load_ListbookingVolleyball();
            });
        }

        private void ShowFastBookingFunction()
        {
            FastBookingVl fastBookingVl = new FastBookingVl();
            fastBookingVl.ShowDialog();
        }

        private void Open(object obj)
        {
            if (CanOpen(obj) == true)
            {
                var temp_obj = obj as ListFieldVolleyball;
                var temp_field = temp_obj.List_FieldVolleyball;
                idField = temp_field.id;
                _idfieldbooking = idField;
                Update_ListbookingVolleyball();
                Load_ListbookingVolleyball();
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
                    List_field_Vl.Remove(obj as ListFieldVolleyball);
                    var abc = obj as ListFieldVolleyball;
                    field xyz = abc.List_FieldVolleyball;
                    DataProvider.Ins.DB.fields.Remove(xyz);
                    DataProvider.Ins.DB.SaveChanges();
                    Update_ListeditVolleyball();
                    Load_ListeditVolleyball();
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
            foreach (var item in List_field_Vl)
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
            foreach (var item in List_field_Vl.ToList())
            {
                List_field_Vl.Remove(item);
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
                    List_field_Vl.Add(temp);
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
        public void Load_ListbookingVolleyball()
        {
            var Temp_booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 2);
            foreach (var item in Temp_booking)
            {
                if (item.idField == idField)
                    list_with_id.Add(item);
            }
        }
        public void Update_ListbookingVolleyball()
        {
            if (list_with_id == null)
            {
                return;
            }
            foreach (var item in list_with_id.ToList())
            {
                list_with_id.Remove(item);
            }
        }
    }
}
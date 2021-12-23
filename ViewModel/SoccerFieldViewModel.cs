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
        

        

        public ICommand PaymentCMD { get; set; }


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
        private ObservableCollection<ListFieldSoccer> _ListField;
        public ObservableCollection<ListFieldSoccer> ListField { get => _ListField; set { _ListField = value; OnPropertyChanged(); } }
        private ObservableCollection<ListFieldSoccer> List_field_Sc;
        public ObservableCollection<ListFieldSoccer> _List_field_Sc { get => List_field_Sc; set { List_field_Sc = value; OnPropertyChanged(); } }
        private ObservableCollection<field> List_Sc;
        public ObservableCollection<field> _List_Sc { get => List_Sc; set { List_Sc = value; OnPropertyChanged(); } }
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


        //Payment info declare
        private ObservableCollection<bookingInfo> _BookigList;
        public ObservableCollection<bookingInfo> BookingList_footballVM { get => _BookigList; set { _BookigList = value; OnPropertyChanged(); } }
        private ObservableCollection<bookingInfo> _DayList;
        public ObservableCollection<bookingInfo> DayList { get => _DayList; set { _DayList = value; OnPropertyChanged(); } }
        public int _idbooking_Payment;
        public int idbooking_Payment { get => _idbooking_Payment; set { _idbooking_Payment = value; OnPropertyChanged(); } }
        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }
        private int _CustomerPhoneNum;
        public int CustomerPhoneNum { get => _CustomerPhoneNum; set { _CustomerPhoneNum = value; OnPropertyChanged(); } }
        private string _NameField;
        public string NameField { get => _NameField; set { _NameField = value; OnPropertyChanged(); } }
        private int? _Field_id;
        public int? Field_id { get => _Field_id; set { _Field_id = value; OnPropertyChanged(); } }
        public int _Booking_id;
        public int Booking_id { get => _Booking_id; set { _Booking_id = value; OnPropertyChanged(); } }
        private decimal? _FieldPrice;
        public decimal? FieldPrice { get => _FieldPrice; set { _FieldPrice = value; OnPropertyChanged(); } }
        private DateTime? _DateBooking;
        public DateTime? DateBooking { get => _DateBooking; set { _DateBooking = value; OnPropertyChanged(); } }
        private DateTime? _StartTime;
        public DateTime? StartTime { get => _StartTime; set { _StartTime = value; OnPropertyChanged(); } }
        private DateTime? _EndTime;
        public DateTime? EndTime { get => _EndTime; set { _EndTime = value; OnPropertyChanged(); } }

        private bookingInfo _SelectedItemPayment;
        public bookingInfo SelectedItemPayment
        {
            get => _SelectedItemPayment;
            set
            {
                _SelectedItemPayment = value;
                OnPropertyChanged();
                if (SelectedItemPayment != null)
                {
                    Booking_id = SelectedItemPayment.id;
                    idbooking_Payment = SelectedItemPayment.id;
                    NameField = SelectedItemPayment.field.name;
                    FieldPrice = SelectedItemPayment.field.fieldtype.price;
                    Field_id = SelectedItemPayment.idField;
                    DateBooking = SelectedItemPayment.datePlay;
                    StartTime = SelectedItemPayment.start_time;
                    EndTime = SelectedItemPayment.end_time;
                    CustomerName = SelectedItemPayment.Customer_name;
                    CustomerPhoneNum = SelectedItemPayment.Customer_PhoneNum.Value;

                }
            }
        }

        //--------------------------- End Payment info declare




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
        public ObservableCollection<bookingInfo> ListbookingInfoSc { get => _ListbookingInfo; set { _ListbookingInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<bookingInfo> _list_with_id;
        public ObservableCollection<bookingInfo> list_with_id { get => _list_with_id; set { _list_with_id = value; OnPropertyChanged(); } }
      

        public SoccerFieldViewModel()
        {
            _ListbookingInfo = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 1));
            _ListField = new ObservableCollection<ListFieldSoccer>();
            _DayList = new ObservableCollection<bookingInfo>();
            List_field_Sc = new ObservableCollection<ListFieldSoccer>();
            _BookigList = new ObservableCollection<bookingInfo>();
            _list_with_id = new ObservableCollection<bookingInfo>();
            _List = new ObservableCollection<field>(DataProvider.Ins.DB.fields.Where(x => x.idType == 1));
            Update_ListbookingSoccer();
            Load_ListbookingSoccer();
            Load_Listfieldsoccer();
            Update_DatagridView12();
            Load_List_footballPayment();

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
                if (SelectedItem.name == FieldName)
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
                    Update_DatagridView12();
                    Load_List_footballPayment();
                }
                else
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
                    Update_Listfieldsoccer();
                    Load_Listfieldsoccer();
                    Update_Listeditsoccer();
                    Load_Listeditsoccer();
                    Update_DatagridView12();
                    Load_List_footballPayment();
                }

               
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
            //    Update_ListbookingSoccer();
            //    Load_ListbookingSoccer();
            //});
            EditBookingCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemBooking == null)
                    return false;
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
                Update_ListbookingSoccer();
                Load_ListbookingSoccer();
                Update_DatagridView12();
                Load_List_footballPayment();
            });
            DeleteBookingCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemBooking == null)
                    return false;
                return true;
            }, (p) =>
            {

                var booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.id == SelectedItemBooking.id).SingleOrDefault();
                ObservableCollection<buyingInfo> Listbuying = new ObservableCollection<buyingInfo>(DataProvider.Ins.DB.buyingInfoes.Where(x => x.idBookingInfo == SelectedItemBooking.id));
                if (Listbuying.Count > 0)
                {
                    MessageBox.Show("There's something you have to pay!");
                    return;

                }
                DataProvider.Ins.DB.bookingInfoes.Remove(booking);
                DataProvider.Ins.DB.SaveChanges();
                Update_ListbookingSoccer();
                Load_ListbookingSoccer();
                Update_DatagridView12();
                Load_List_footballPayment();
                addcustomername = null;
                addcustomerphone = null;                   
            });


            // Payment contr --------------------------
            
            PaymentCMD = new RelayCommand<Window>((p) => { return true; }, (p) => {
                var temp_check = DataProvider.Ins.DB.bookingInfoes;
                decimal fieldprice = 0;
                foreach (var item in temp_check)
                {
                    if (item.id == Booking_id)
                    {
                        fieldprice = item.field.fieldtype.price.Value;
                    }
                }
                if (DateBooking == null || StartTime == null || EndTime == null)
                {
                    MessageBox.Show("Select bill for Payment.");
                    return;
                }
                string date = DateBooking.Value.ToString("MM/dd/yyyy");
                string starttiem = StartTime.Value.ToString("hh:mm tt");
                string endtiem = EndTime.Value.ToString("hh:mm tt");
                PayMent_tem WD_pay = new PayMent_tem(Booking_id, CustomerName, CustomerPhoneNum.ToString(), date, starttiem, endtiem, fieldprice);
                WD_pay.ShowDialog();
                Update_DatagridView();
                Load_List_footballPayment();
            });
            //-------------------------------------------- End Payment contr

        }

        //Payment fuction ----------------------------
        public void Update_DatagridView12()
        {
            if(DayList != null)
                DayList.Clear();
            return;
        }

        private void Update_DatagridView()
        {
            Booking_id = 0;
            CustomerName = null;
            CustomerPhoneNum = 0;
            StartTime = null;
            EndTime = null;
            DateBooking = null;
            FieldPrice = null;
            DayList.Clear();

        }
        public void Load_List_footballPayment()
        {

            var temp_List = DataProvider.Ins.DB.bookingInfoes;
                foreach (var info in temp_List)
                {
                    if (info.field.idType == 1)
                    {
                        int count = info.Status.Length;
                        if (info.Status != "Pay")
                        {
                            _DayList.Add(info);
                        }
                    }

                }
            return;
        }

        // ------------------------------------------ End Payment fuction

        private void ShowFastBookingFunction()
        {
            FastBookingSc fastBookingSc = new FastBookingSc();
            fastBookingSc.ShowDialog();
        }

        private void Open(object obj)
        {
            if (CanOpen(obj) == true)
            {
                var temp_obj = obj as ListFieldSoccer;
                var temp_field = temp_obj.List_FieldSoccer;
                idField = temp_field.id;
                _idfieldbooking = idField;
                Update_ListbookingSoccer();
                Load_ListbookingSoccer();
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
                    List_field_Sc.Remove(obj as ListFieldSoccer);
                    var abc = obj as ListFieldSoccer;
                    field xyz = abc.List_FieldSoccer;
                    ObservableCollection<bill> Listbill = new ObservableCollection<bill>(DataProvider.Ins.DB.bills.Where(x => x.bookingInfo.idField == xyz.id));
                    foreach(var item in Listbill.ToList()){
                        Listbill.Remove(item);
                        DataProvider.Ins.DB.bills.Remove(item);
                    }
                    ObservableCollection<buyingInfo> Listbuying = new ObservableCollection<buyingInfo>(DataProvider.Ins.DB.buyingInfoes.Where(x => x.bookingInfo.idField == xyz.id));
                    foreach (var item in Listbuying.ToList())
                    {
                        Listbuying.Remove(item);
                        DataProvider.Ins.DB.buyingInfoes.Remove(item);
                    }
                    ObservableCollection<bookingInfo> Listbooking = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes.Where(x => x.idField == xyz.id));
                    foreach (var item in Listbooking.ToList())
                    {
                        Listbooking.Remove(item);
                        DataProvider.Ins.DB.bookingInfoes.Remove(item);
                    }
                    DataProvider.Ins.DB.fields.Remove(xyz);
                    DataProvider.Ins.DB.SaveChanges();
                    Update_Listeditsoccer();
                    Load_Listeditsoccer();
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
            foreach (var item in List_field_Sc)
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
            foreach (var item in List_field_Sc.ToList())
            {
                List_field_Sc.Remove(item);
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
                    List_field_Sc.Add(temp);
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
        public void Load_ListbookingSoccer()
        {
            var Temp_booking = DataProvider.Ins.DB.bookingInfoes.Where(x => x.field.idType == 1);
            foreach (var item in Temp_booking)
            {
                if (item.idField == idField && item.Status=="unpay")
                    list_with_id.Add(item);
            }
        }
        public void Update_ListbookingSoccer()
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
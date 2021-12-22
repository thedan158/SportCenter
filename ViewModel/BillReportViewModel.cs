using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportCenter.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SportCenter.ViewModel
{
    public class BillReportViewModel : BaseViewModel
    {
        protected ObservableCollection<bookingInfo> _bookingList; //Main 
        protected ObservableCollection<bookingInfo> bookinglist
        {
            get => _bookingList;
            set
            {
                _bookingList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<bill> _billList;  //sup
        protected ObservableCollection<bill> billList
        {
            get => _billList;
            set
            {
                _billList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<field> _fieldList;
        protected ObservableCollection<field> fieldList
        {
            get => _fieldList;
            set
            {
                _fieldList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BaseBill2> _DatagridListView; //display
        public ObservableCollection<BaseBill2> DatagridListView
        {
            get => _DatagridListView;
            set
            {
                _DatagridListView = value;
                OnPropertyChanged();
            }
        }


        public BillReportViewModel()
        {
            _bookingList = new ObservableCollection<bookingInfo>(DataProvider.Ins.DB.bookingInfoes);
            _billList = new ObservableCollection<bill>(DataProvider.Ins.DB.bills);
            _fieldList = new ObservableCollection<field>(DataProvider.Ins.DB.fields);
            _DatagridListView = new ObservableCollection<BaseBill2>();
            Update_DatagridView();
            Load_DatagridView();
            
        }

        public void Update_DatagridView()
        {
            DatagridListView.Clear();
        }

        public void Load_DatagridView()
        {

            var temp_booking = DataProvider.Ins.DB.bookingInfoes;
            var temp_bill = DataProvider.Ins.DB.bills;
            var temp_field = DataProvider.Ins.DB.fields;

            foreach (var item2 in temp_bill)   // take list booking out of DB
            {
                BaseBill2 Adding = new BaseBill2();
                Adding._TotalMoney = item2.totalmoney;
                foreach (var item in temp_booking)     // take bill 
                {
                    String.Format("{0:DD-MM-yyyy}", item.datePlay);
                    String.Format("{0:hh:mm tt}", item.start_time);
                    String.Format("{0:hh:mm tt}", item.end_time);
                    if (item.Status == "Pay")
                    {
                        if (item.id == item2.idBookingInfo)
                        {
                            Adding._TotalMoney = item2.totalmoney;
                            Adding.b_bookinginfo = item;
                        }
                    }
                    foreach (var item3 in temp_field)    // take field info
                    {
                        if (item.idField == item3.id)
                        {
                            Adding.b_field = item3;
                        }
                    }
                }
                Adding._GoodMoney = Adding._TotalMoney - Adding.b_field.fieldtype.price.Value;
                DatagridListView.Add(Adding);

            }

        }
    }
}
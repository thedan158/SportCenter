using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportCenter.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SportCenter.ViewModel
{
    class BillReportViewModel : BaseViewModel
    {
        protected ObservableCollection<bookingInfo> _bookingList; //Main 
        public ObservableCollection<bookingInfo> bookinglist
        {
            get => _bookingList;
            set
            {
                _bookingList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<bill> _billList;  //sup
        public ObservableCollection<bill> billList
        {
            get => _billList;
            set
            {
                _billList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<field> _fieldList;
        public ObservableCollection<field> fieldList
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

            Load_DatagridView();
            Update_DatagridView();
        }

        private void Update_DatagridView()
        {
            foreach (var item in _DatagridListView.ToList())
            {
                _DatagridListView.Remove(item);
            }
        }

        private void Load_DatagridView()
        {
            foreach (var item2 in _billList)   // take list booking out of DB
            {
                BaseBill2 Adding = new BaseBill2();
                foreach (var item in _bookingList)     // take bill 
                {

                    if (item.Status == "Pay")
                    {
                        if (item.id == item2.idBookingInfo)
                        {
                            Adding._TotalMoney = item2.totalmoney;
                            Adding.b_bookinginfo = item;
                        }
                    }
                    foreach (var item3 in _fieldList)    // take field info
                    {
                        if (item.idField == item3.id)
                        {
                            Adding.b_field = item3;
                        }
                    }
                }
                _DatagridListView.Add(Adding);

            }

        }
    }
}
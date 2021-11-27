using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportCenter.Model;
using SportCenter.ViewModel;

namespace SportCenter.ViewModel
{
    class BaseBill2 : BaseViewModel
    {
        public field b_field { get; set; }
        public List<bookingInfo> b_ListBooking { get; set; }
        public List<buyingInfo> b_ListBuying { get; set; }
        public Decimal _TotalMoney { get; set; }

    }
}

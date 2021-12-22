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
    public class BaseBill2 : BaseViewModel
    {
        public field b_field { get; set; }
        public List<bookingInfo> b_ListBooking { get; set; }
        public List<buyingInfo> b_ListBuying { get; set; }
        public bookingInfo b_bookinginfo { get; set; }
        public Decimal _TotalMoney { get; set; }
        public Decimal _GoodMoney { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportCenter.Model;
using System.Runtime;

namespace SportCenter.Model
{
    public class ListBill
    {
        public field List_Field { get; set; }
        public bill List_Bill { get; set; }
        public bookingInfo List_Booking { get; set; }
        public buyingInfo List_Buying { get; set; }
        public good Good_info_base { get; set; }
        public int total_goods { get; set; }

    }
}

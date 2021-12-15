using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.ViewModel
{
    public class BaseCustomerInfo : BaseViewModel
    {
        public int STT { get; set; }
        public string Baseinfo_CusName { get; set; }
        public string Baseinfo_CusPhoneNum { get; set; }
        public int Baseinfo_SumCusMoneyAmount { get; set; }
        public int Baseinfo_SumBillAmount { get; set; }
        public string Baseinfo_TypeCus { get; set; }

    }
}

using SportCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.ViewModel
{
    public class ListBillViewModel : BaseViewModel
    {
        private bill _listbill { get; set; }
        private bookingInfo _BookingIF { get; set; }
        private field _field { get; set; }
        public buyingInfo _buying { get; set; }

        public string id_bill => _listbill.id.ToString();
        public string idBookkinginfo => _listbill.bookingInfo.ToString();
        public string totalMoney => _listbill.totalmoney.ToString();
        public string booking => _BookingIF.id.ToString();
        

        public string id_field => _field.id.ToString();
        public string id_fieldType => _field.idType.ToString();
        public string field_name => _field.name.ToString();
        public string field_condition => _field.condition.ToString();
    }

    
}

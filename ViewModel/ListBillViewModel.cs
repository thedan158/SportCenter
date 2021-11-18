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
        private readonly bill _listbill;
        private readonly bookingInfo _BookingIF;
        private readonly field _field;

        public string id_bill => _listbill.id.ToString();
        public string idBookkinginfo => _listbill.idBookinginfo.ToString();
        public string idBuyinginfo => _listbill.idBuyinginfo.ToString();
        public string totalMoney => _listbill.totalmoney.ToString();
        public string booking => _BookingIF.id.ToString();
        public string startdate => _BookingIF.starttime.ToString();
        public string enddate => _BookingIF.endtime.ToString();

        public string id_field => _field.id.ToString();
        public string id_fieldType => _field.idType.ToString();
        public string field_name => _field.name.ToString();
        public string field_condition => _field.condition.ToString();

        

    }

    

    public class BookingiF : BaseViewModel
    {
        public int id { get; }
        public int id_Field { get; }
        public DateTime starttime { get; }
        public DateTime endtime { get; }
        public TimeSpan Lenght => endtime.Subtract(starttime);
    }
}

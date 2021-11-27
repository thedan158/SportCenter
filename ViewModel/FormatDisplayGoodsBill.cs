using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.ViewModel
{
    public class FormatDisplayGoodsBill
    {
        public int No { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Quantity { get; set; }
        public string Unit { get; set; }
        public int Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class DataAmount
    {

        public Decimal AmountTotalPEN { get; set; }

        public Decimal AmountTotalUSD { get; set; }

        public Decimal AmountFilteredPEN { get; set; }

        public Decimal AmountFilteredUSD { get; set; }

        public String AmountPercentaje { get; set; } 
    }
}

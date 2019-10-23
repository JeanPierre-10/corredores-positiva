using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class DataAvance
    {
        public Decimal AmountTotalPEN { get; set; }
        public Decimal AmountTotalUSD { get; set; }
        public Decimal AmountFilteredPEN { get; set; }
        public Decimal AmountFilteredUSD { get; set; }
        public String AmountPercentaje { get; set; }

        public String calcularPorcentaje(Decimal AmountTotalPEN, Decimal AmountFilteredPEN)
        {
            if (AmountTotalPEN == 0 || AmountFilteredPEN == 0)
                return "0 %";

            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2; 
            return (AmountFilteredPEN * 100 / AmountTotalPEN).ToString("N", setPrecision) + " %";
        }

        public DataAvance(Decimal AmountTotalPEN, Decimal AmountTotalUSD, Decimal AmountFilteredUSD, Decimal AmountFilteredPEN)
        {
            this.AmountFilteredPEN = AmountFilteredPEN;
            this.AmountTotalPEN = AmountTotalPEN;
            this.AmountTotalUSD = AmountTotalUSD;
            this.AmountFilteredUSD = AmountFilteredUSD;
            this.AmountPercentaje = calcularPorcentaje(AmountTotalPEN, AmountFilteredPEN);

        }

    }
}

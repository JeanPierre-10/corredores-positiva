using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEDataCoasegPorCompania 
    {
        public int CodigoCompania { get; set; }
        public String NombreCompania { get; set; }
        public String MontoPEN { get; set; }
        public String MontoUSD { get; set; }

        public String CodigoAnio { get; set; }
    }
}

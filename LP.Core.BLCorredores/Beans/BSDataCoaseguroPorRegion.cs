using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSDataCoaseguroPorRegion : IEqualityComparer<BSDataCoaseguroPorRegion>
    {
        public int CodigoRegion { get; set; }
        public String NombreRegion { get; set; }
        public String MontoPEN { get; set; }
        public String MontoUSD { get; set; }
        public String MontoAnteriorPEN { get; set; }
        public String MontoAnteriorUSD { get; set; }

        public bool Equals(BSDataCoaseguroPorRegion x, BSDataCoaseguroPorRegion y)
        {
            return x.CodigoRegion == y.CodigoRegion;
        }
 
        public int GetHashCode(BSDataCoaseguroPorRegion obj)
        {
            return obj.CodigoRegion.GetHashCode();
        }
    }
}

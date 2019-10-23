using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSDataCoaseguroPorRamo : IEqualityComparer<BSDataCoaseguroPorRamo>
    {
        public String CodigoRamo { get; set; }
        public String NombreRamo { get; set; }
        public String MontoPEN { get; set; }
        public String MontoUSD { get; set; }
        public String MontoAnteriorPEN { get; set; }
        public String MontoAnteriorUSD { get; set; }

        public bool Equals(BSDataCoaseguroPorRamo x, BSDataCoaseguroPorRamo y)
        {
            return x.CodigoRamo == y.CodigoRamo;
        }

        public int GetHashCode(BSDataCoaseguroPorRamo obj)
        {
            return obj.CodigoRamo.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSDataCoaseguroPorCompania : IEqualityComparer<BSDataCoaseguroPorCompania>
    {
        public int CodigoCompania { get; set; }
        public String NombreCompania { get; set; }
        public String MontoPEN { get; set; }
        public String MontoUSD { get; set; }
        public String MontoAnteriorPEN { get; set; }
        public String MontoAnteriorUSD { get; set; }



      

        public bool Equals(BSDataCoaseguroPorCompania x, BSDataCoaseguroPorCompania y)
        {
            return x.CodigoCompania == y.CodigoCompania;
        }

        public int GetHashCode(BSDataCoaseguroPorCompania obj)
        {
            return obj.CodigoCompania.GetHashCode();
        }
    }
}

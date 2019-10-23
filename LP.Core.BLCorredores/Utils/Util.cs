using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Utils
{
    public static class Util
    {
        public static String evaluarParametros(int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {
            String fitroBusqueda = String.Concat("T", (CodigoCompania > 0 ? "C" : ""), (!CodigoRamo.Equals("0") ? "R" : ""), (CodigoRegion > 0 ? "G" : ""), (CodigoBroker > 0 ? "B" : ""));

            return fitroBusqueda;
        }


        public static String formatearFecha(String fecha)
        {
            return String.Concat(fecha.Substring(0, 4), fecha.Substring(5, 2));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEDataComparativoMensualDetalle
    {
        public String FechaActualizacion { get; set; }
        public List<BEDataCoasegPorPeriodo> DataCoasegPorPeriodo { get; set; }
        public List<BEDataCoasegPorCompania> DataCoasegPorCompania { get; set; }
        public List<BEDataCoasegPorRegion> DataCoasegPorRegion { get; set; }
        public List<BEDataCoasegPorRamo> DataCoasegPorRamo { get; set; }
         
    }
}

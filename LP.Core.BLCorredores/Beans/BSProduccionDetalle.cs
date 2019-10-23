
using LP.Core.ADCorredores.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSProduccionDetalle
    {

        public String FechaActualizacion { get; set; }
        public List<BSDataCoaseguroPorPeriodo> DataCoasegPorPeriodo { get; set; }
        public List<BSDataCoaseguroPorCompania> DataCoasegPorCompania { get; set; }
        public List<BSDataCoaseguroPorRegion> DataCoasegPorRegion { get; set; }
        public List<BSDataCoaseguroPorRamo> DataCoasegPorRamo { get; set; } 
    }
}

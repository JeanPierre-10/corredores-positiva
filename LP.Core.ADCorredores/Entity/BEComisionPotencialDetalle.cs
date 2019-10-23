using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEComisionPotencialDetalle
    {
        public String fechaActualizacion { get; set; }
        public String compania { get; set; }
        public String ramo { get; set; }
        public String broker { get; set; }
        public List<ComisionRiesgo> comisionRiesgo { get; set; }
        public List<ComisionRiesgo> comisionRiesgoCompania { get; set; }
        public List<ComisionRiesgo> comisionRiesgoRamo { get; set; }
        public List<ComisionRiesgo> comisionRiesgoBroker { get; set; }
    }
    public class ComisionRiesgo
    {
        public String periodo { get; set; }
        public String primaPEN { get; set; }
        public String comisionPEN { get; set; }
        public String primaUSD { get; set; }
        public String comisionUSD { get; set; }
        public String nombre { get; set; } = "";
    }
}


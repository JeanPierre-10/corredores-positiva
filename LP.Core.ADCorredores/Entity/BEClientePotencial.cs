using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEClientePotencial
    { 
        public String fechaActualizacion { get; set; }

        public List<BECliente> clientesPotenciales { get; set; }
    }

    public class BECliente
    {
        public String nombre { get; set; }
        public String codigoCliente { get; set; }
        public String primaSOL { get; set; }
        public String comisionSOL { get; set; }
        public String primaUSD { get; set; }
        public String comisionUSD { get; set; }
        public String cantidadPolizas { get; set; }
        public String cantidadDocumentos { get; set; }
    }
}

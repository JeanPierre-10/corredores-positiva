using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSClientePotencial
    {
        public String fechaActualizacion { get; set; }

        public List<BSCliente> clientesPotenciales { get; set; }
    }

    public class BSCliente
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

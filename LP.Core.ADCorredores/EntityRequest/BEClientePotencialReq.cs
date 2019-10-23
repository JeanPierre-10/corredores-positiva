using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.EntityRequest
{
    public class BEClientePotencialReq
    { 
        public int codigoCompania { get; set; }
        public String codigoRamo { get; set; }
        public int codigoBroker { get; set; }
        public String filtroBusqueda { get; set; }
        public String cantidadRegistros { get; set; }
        public String diasBusqueda { get; set; }
    }
}

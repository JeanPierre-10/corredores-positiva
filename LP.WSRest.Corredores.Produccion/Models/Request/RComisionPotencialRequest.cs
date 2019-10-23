using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP.WSRest.Corredores.Produccion.Models.Request
{
    public class RComisionPotencialRequest
    {
        public int CodigoCompania { get; set; } = 0;
        public String CodigoRamo { get; set; } = "0";
        public int CodigoBroker { get; set; } = 0;
        public String FiltroBusqueda { get; set; } = "15";
        public String FiltroPeriodo { get; set; } = "P";

        // Temporal
        public String moneda { get; set; } = "PEN";
    }
}
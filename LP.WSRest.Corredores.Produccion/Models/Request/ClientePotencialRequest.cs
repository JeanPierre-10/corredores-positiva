using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP.WSRest.Corredores.Produccion.Models.Request
{
    public class ClientePotencialRequest
    {
        public int CodigoCompania { get; set; } = 0;
        public String CodigoRamo { get; set; } = "";
        public int CodigoBroker { get; set; } = 0; 
        public String CantidadRegistros { get; set; } = "7";
        public String DiasBusqueda { get; set; } = "0";
    }
}
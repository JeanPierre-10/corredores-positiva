using FluentValidation.Attributes;
using LP.WSRest.Corredores.Produccion.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace LP.WSRest.Corredores.Produccion.Models.Request
{
    [Validator(typeof(ReglasMensualReq))]
    public class ProduccionRequest
    { 
        public String InicioConsulta { get; set; } = "";
        public String FinConsulta { get; set; } = "";
        public int CodigoCompania { get; set; } = 0;
        public String CodigoRamo { get; set; } = "0";
        public int CodigoRegion { get; set; } = 0;
        public int CodigoBroker { get; set; } = 0;

    }
}
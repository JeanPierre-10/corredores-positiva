using FluentValidation.Attributes;
using LP.WSRest.Corredores.Produccion.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LP.WSRest.Corredores.Produccion.Models.Request
{
    public class BrokerRequest
    {
        public String CodigoComercial { get; set; }
         
    }
}
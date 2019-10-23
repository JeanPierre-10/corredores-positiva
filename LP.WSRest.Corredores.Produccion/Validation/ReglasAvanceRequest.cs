using FluentValidation;
using LP.WSRest.Corredores.Produccion.Models;
using LP.WSRest.Corredores.Produccion.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LP.WSRest.Corredores.Produccion.Validation
{
    public class ReglasAvanceRequest : AbstractValidator<ProduccionAvanceRequest>
    {
        public ReglasAvanceRequest()
        {
            RuleFor(x => x.InicioConsulta)
                .NotNull()
                .WithMessage("Fecha de consulta no debe ser null")
                .NotEmpty()
                .WithMessage("Fecha de consulta no debe estar en blanco")
                .Must(validarFormato).WithMessage("Fecha de consulta debe tener formato yyyy-MM-dd");
        }
        private Boolean validarFormato(String fecha)
        {   if (fecha == null)
                return false;
            return Regex.Match(fecha, @"^\d{4}-\d{2}-\d{2}$").Success;
        }
    }

   
}
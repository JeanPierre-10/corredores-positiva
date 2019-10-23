using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP.WSRest.Corredores.Produccion.Models.Request
{
    /// <summary>
    /// Clase que contiene los datos del usuario a autenticar
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Usuario del aplicativo
        /// </summary>
        public String Usuario { get; set; }
        /// <summary>
        /// Clave del aplicativo
        /// </summary>
        public String Clave { get; set; }


        public override String ToString()
        {
            return this.Clave + "" + this.Usuario;
        }
    }
}
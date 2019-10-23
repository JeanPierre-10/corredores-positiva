using LP.WSRest.Corredores.Produccion.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace LP.WSRest.Corredores.Produccion.Util
{
    public class Error
    {
        public static HttpResponseMessage getErrorGenerico(String mensaje)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            message.Content = new StringContent(
            new JavaScriptSerializer().Serialize(new ErrorResponse() { Error = mensaje }), Encoding.UTF8, "application/json");
            return message;
        }
    }
}
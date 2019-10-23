using log4net;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Models;
using LP.WSRest.Corredores.Produccion.Models.Request;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Seguridad;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace LP.WSRest.Corredores.Produccion.Controllers
{
    /// <summary>
    /// AutenticacionController
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("services/lp/corredores/autenticacion")]
    public class AutenticacionController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Autenticación de cliente
        /// </summary>
        /// <param name="requestEntity">Entidad requerida para generar el token</param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ISeguridad), Description = "Successfull operation")]
        //[SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        //[SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        //[SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult Authenticate(LoginRequest requestEntity)
        {
            bool isCredentialValid = false;
            if (requestEntity == null)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                message.Content = new StringContent(
                new JavaScriptSerializer().Serialize(new ErrorResponse() { Error = "Cuerpo de mensaje no valido." }), Encoding.UTF8, "application/json");
                throw new HttpResponseException(message);
            }
            try
            {
                ISeguridad client = new ImpSeguridad();
                isCredentialValid = (client.PostAutenticacion(new Core.BLCorredores.Beans.BSAutenticacion()
                {
                    Usuario = requestEntity.Usuario,
                    Credencial = requestEntity.Clave
                }));
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
            }

            if (isCredentialValid)
            {
                AutenticacionResponse tokenEntity = new AutenticacionResponse()
                {
                    token = TokenGenerator.GenerateTokenJwt(requestEntity.Usuario)
                };
                return Ok(tokenEntity);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

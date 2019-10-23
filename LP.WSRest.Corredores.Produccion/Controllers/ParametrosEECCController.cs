using log4net;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion.Controllers
{
    [RoutePrefix("services/lp/corredores/parametrosEECC")]
    public class ParametrosEECCController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Servicio para obtiener los parametros necesarios para la consulta de Estado de Cuenta de SAP.
        /// </summary>
        /// <param name="TipoParametro"></param>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<BSParameterEECC>), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetParametersEECC(String TipoParametro = "", String IdEstado = "A")
        {
            try
            {
                TipoParametro = String.Empty.Equals(TipoParametro) ? ConfigurationManager.AppSettings["CADENABUSQUEDA"] : TipoParametro;
                IParameterEECC service = new ImpParameterEECC();
                var result = service.ObtenerParametrosEECC(TipoParametro, IdEstado);
                return Ok(result);

            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }

        }
    }
}

using log4net;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Models.Request;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion.Controllers
{
    [RoutePrefix("services/lp/corredores/comision")]
    public class ComisionController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Servicio para retornar comision
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("pendientePago")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSProduccion), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetComision([FromUri] ProduccionRequest request)
        {
            BSProduccion resultado = null;
            try
            {
                IComision client = new ImpComision();
                resultado = client.obtenerComisionPotencial(request.CodigoCompania, request.CodigoRamo, request.CodigoBroker);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }

        /// <summary>
        /// Servicio para retornar a los clientes TOP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("clientesTop")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSClientePotencial), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetClientsPotencial([FromUri] ClientePotencialRequest request)
        {
            BSClientePotencial resultado = null;
            try
            {
                IComision client = new ImpComision();
                resultado = client.obtenerClientesPotencials(request.CodigoCompania, request.CodigoRamo, request.CodigoBroker, request.CantidadRegistros, request.DiasBusqueda);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }

        /// <summary>
        /// Servicio para retornar comision de potencial riesgo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("riesgo")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSComisionRG), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetComisionPotencialRiesgo([FromUri] RComisionPotencialRequest request)
        {
            BSComisionRG resultado = null;
            try
            { 
                IComision client = new ImpComision();
                resultado = client.obtenerComisionPotencialRiesgo(request.CodigoCompania, request.CodigoRamo, request.CodigoBroker, request.FiltroBusqueda, request.FiltroPeriodo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }

        /// <summary>
        /// Servicio para retornar el detalle de comision potencial riesgo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("riesgodetallado")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSComisionRGDetalle), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetComisionPotencialRiesgoDetalle([FromUri] RComisionPotencialRequest request)
        {
            BSComisionRGDetalle resultado = null;
            try
            {
                IComision client = new ImpComision();
                resultado = client.obtenerComisionPotencialRiesgoDetalle(request.CodigoCompania, request.CodigoRamo, request.CodigoBroker, request.FiltroBusqueda, request.FiltroPeriodo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }

        // TEMPORAL
        [HttpGet]
        [Authorize]
        [Route("riesgodetalladotemporal")]
        public IHttpActionResult GetComisionPotencialRiesgoDetalleTemporal([FromUri] RComisionPotencialRequest request)
        {
            BSComisionRGDetalle resultado = null;
            try
            {
                IComision client = new ImpComision();
                resultado = client.obtenerComisionPotencialRiesgoDetalleTemporal(request.CodigoCompania, request.CodigoRamo, request.CodigoBroker, request.FiltroBusqueda, request.FiltroPeriodo, request.moneda);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }
    }
}

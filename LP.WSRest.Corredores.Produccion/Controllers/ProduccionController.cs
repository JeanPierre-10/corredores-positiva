using log4net;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Models.Request;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Util;
using LP.WSRest.Corredores.Produccion.Validation;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion.Controllers
{
    [RoutePrefix("services/lp/corredores/produccion")]
    public class ProduccionController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Servicio para retornar la producción mensual en un determinado rango de fechas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("comparativomensual")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSPrimaCoaseguro), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetProduccion([FromUri] ProduccionRequest request)
        {
            BSPrimaCoaseguro bsPrimaCoaseguro = null;
            try
            {
                INetoCoaseguro client = new ImpNetoCoaseguro();
                bsPrimaCoaseguro = client.GetPrimaCoaseguro(request.InicioConsulta, request.FinConsulta, request.CodigoCompania,
                                                  request.CodigoRamo, request.CodigoRegion, request.CodigoBroker);
                return Ok(bsPrimaCoaseguro);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }

        }


        /// <summary>
        /// Servicio para retornar el detalle de la producción
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("comparativomensualdetallado")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSProduccionDetalle), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetProduccionDetail([FromUri] ProduccionRequest request)
        {
            BSProduccionDetalle bsPrimaCoaseguro = null;
            try
            {
                IProduccionDetalle client = new ImpProduccionDetalle();
                if (request == null)
                    request = new ProduccionRequest();
                bsPrimaCoaseguro = client.GetProduccionDetalle(request.CodigoCompania,
                                                  request.CodigoRamo, request.CodigoRegion, request.CodigoBroker);
                return Ok(bsPrimaCoaseguro);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }

        }

        /// <summary>
        /// Servicio para obtener el avance de la producción
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("avanceproduccion")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSAvanceCoaseguro), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetAvanceProduction([FromUri] ProduccionAvanceRequest request)
        {
            BSAvanceCoaseguro bsAbansePrimaCoaseguro = null;
            try
            {
                INetoCoaseguro client = new ImpNetoCoaseguro();
                bsAbansePrimaCoaseguro = client.GetAvanceCoaseguro(request.InicioConsulta, request.CodigoCompania,
                                                  request.CodigoRamo, request.CodigoRegion, request.CodigoBroker);
                if(bsAbansePrimaCoaseguro.Year == "null")
                    return Ok(new BSAvanceCoaseguro());
                else
                    return Ok(bsAbansePrimaCoaseguro.Year == null ? null : bsAbansePrimaCoaseguro);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }

        }

        /// <summary>
        /// Servicio para obtener el detalle de el avance de la producción
        /// </summary>
        /// <param name="InicioConsulta"></param>
        /// <param name="FinConsulta"></param>
        /// <param name="CodigoCompania"></param>
        /// <param name="CodigoRamo"></param>
        /// <param name="CodigoRegion"></param>
        /// <param name="CodigoBroker"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("avanceproducciondetallado")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BSPrimaCoaseguro), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetAvanceProductionDetail(String InicioConsulta, String FinConsulta, int CodigoCompania,
                                                String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {
            BSPrimaCoaseguro bsPrimaCoaseguro = null;
            try
            {
                INetoCoaseguro client = new ImpNetoCoaseguro();
                bsPrimaCoaseguro = client.GetPrimaCoaseguro(InicioConsulta, FinConsulta, CodigoCompania,
                                                  CodigoRamo, CodigoRegion, CodigoBroker);
                return Ok(bsPrimaCoaseguro);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }
    }
}

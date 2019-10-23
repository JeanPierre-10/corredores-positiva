using log4net;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Models;
using LP.WSRest.Corredores.Produccion.Models.Request;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion.Controllers
{
    [RoutePrefix("services/lp/corredores/ramos")]
    public class RamosController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Obtener los ramos filtrados por el codigo de compañia
        /// </summary>
        /// <param name="codigoCompania">Codigo de la compañia asociada al ramo</param> 
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<BSBouquet>), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetAllBouquet(int codigoCompania = 0)
        {
            List<BSBouquet> listBouquet = null;
            try
            {
                RamoRequest bouquetRequest = new RamoRequest() { CodigoCampania = codigoCompania.ToString(), Flag = (codigoCompania == 0 ? "T" : "") };
                IBouquet client = new ImpBouquet();
                listBouquet = client.GetBouquet(bouquetRequest.CodigoCampania, bouquetRequest.Flag);
                return Ok(listBouquet);
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }
        }

    }
}

using log4net;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Cache;
using LP.WSRest.Corredores.Produccion.Models;
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
    [RoutePrefix("services/lp/corredores/brokers")]
    public class BrokersController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CacheItemPolicy cachePolicty = null;
        public BrokersController()
        {
            cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddMinutes(60);

        }

        /// <summary>
        /// Servicio para obtener los brokers.
        /// </summary>
        /// <returns>Lista de bokers</returns>
        [HttpGet]
        //[Authorize]
        [CacheClient(Duration = 3600)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<BSBroker>), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetBroker()
        {
            List<BSBroker> listBroker = null;
            try
            {
                var cache = MemoryCache.Default;
                if (cache.Get("dataCacheBroker") == null)
                {
                    IBroker client = new ImpBroker();
                    listBroker = client.GetBroker();
                    cache.Add("dataCacheBroker", listBroker.ToList(), cachePolicty);
                    return Ok(listBroker);
                }
                else
                {
                    IEnumerable<BSBroker> data = (IEnumerable<BSBroker>)cache.Get("dataCacheBroker");
                    return Ok(data.AsQueryable());
                }
            }
            catch (Exception ex)
            {
                log.Error("Se ha presentado el siguiente error: " + ex.Message);
                throw new HttpResponseException(Error.getErrorGenerico("A ocurrido un error al intentar procesar la informacion."));
            }

        }
    }
}

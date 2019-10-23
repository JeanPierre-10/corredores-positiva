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
    [RoutePrefix("services/lp/corredores/regiones")]
    public class RegionesController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CacheItemPolicy cachePolicty = null;
        /// <summary>
        /// Configurando el uso de cache
        /// </summary>
        public RegionesController()
        {
            cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddMinutes(60);
        }

        /// <summary>
        /// Servicio para retornar una lista de todas las regiones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CacheClient(Duration = 60)]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<BSRegion>), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetAllRegion()
        {
            var cache = MemoryCache.Default;
            List<BSRegion> listRegion = null;
            try
            {
                if (cache.Get("dataCacheRegion") == null)
                {
                    IRegion client = new ImpRegion();
                    listRegion = client.GetRegion();
                    cache.Add("dataCacheRegion", listRegion.ToList(), cachePolicty);
                    return Ok(listRegion);
                }
                else
                {
                    IEnumerable<BSRegion> data = (IEnumerable<BSRegion>)cache.Get("dataCacheRegion");
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

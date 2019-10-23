using FluentValidation.WebApi;
using log4net;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Beans;
using LP.Core.BLCorredores.Interfaces;
using LP.Core.BLCorredores.Service;
using LP.WSRest.Corredores.Produccion.Cache;
using LP.WSRest.Corredores.Produccion.Models;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Util;
using LP.WSRest.Corredores.Produccion.Validation;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;
using System.Xml.Linq;

namespace LP.WSRest.Corredores.Produccion.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("services/lp/corredores/companias")]
    public class CompaniasController : ApiController
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CacheItemPolicy cachePolicty = null;
        /// <summary>
        /// 
        /// </summary>
        public CompaniasController()
        {
            cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddMinutes(2);
        }

        /// <summary>
        /// Obtener todas las compañias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CacheClient(Duration = 120)]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<BSBells>), Description = "Successfull operation")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse), Description = "Internal Server Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(ErrorResponse), Description = "Unauthorized")]
        public IHttpActionResult GetAllBells()
        {
            var cache = MemoryCache.Default;
            List<BSBells> listBells = null;
            try
            {
                if (cache.Get("dataCacheBells") == null)
                {
                    IBellsInformation client = new ImpBellsInformation();
                    listBells = client.GetBells();
                    cache.Add("dataCacheBells", listBells.ToList(), cachePolicty);
                    return Ok(listBells);
                }
                else
                {
                    IEnumerable<BSBells> data = (IEnumerable<BSBells>)cache.Get("dataCacheBells");
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

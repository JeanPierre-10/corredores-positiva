using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LP.WSRest.Corredores.Produccion.Cache
{
    public class CacheClientAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(Duration),
                MustRevalidate = true,
                Public = true
            };
            
        }
         
        //public override void OnActionExecuting(HttpActionContext actionExecutedContext)
        //{
        //    actionExecutedContext.Response.Content = new CacheClientAttribute { };
        //}

        //public static implicit operator HttpContent(CacheClientAttribute v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
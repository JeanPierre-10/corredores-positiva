//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LP.WSRest.Corredores.Produccion
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("application/json"));
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo(String.Empty)
            //{
            //    NumberFormat = new System.Globalization.NumberFormatInfo
            //    {
            //        CurrencyDecimalDigits = 5
            //    }
            //};
        }
        protected void Application_EndRequest()
        {
            // removing excessive headers. They don't need to see this.
            Response.Headers.Remove("Server");
        }
    }
}

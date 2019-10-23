
using FluentValidation.WebApi;
using LP.WSRest.Corredores.Produccion.Models.Response;
using LP.WSRest.Corredores.Produccion.Seguridad;
using LP.WSRest.Corredores.Produccion.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.Filters.Add(new ValidateModelAttribute());
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi", //routeTemplate: "api/{controller}/{id}",
                 routeTemplate: "services/lp/corredores/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}

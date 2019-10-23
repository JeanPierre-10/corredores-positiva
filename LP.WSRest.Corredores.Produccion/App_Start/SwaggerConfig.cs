using System.Web.Http;
using WebActivatorEx;
using LP.WSRest.Corredores.Produccion;
using Swashbuckle.Application;
using System;
using System.Reflection;
using System.IO;
using LP.WSRest.Corredores.Produccion.Seguridad;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace LP.WSRest.Corredores.Produccion
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            // PRUEBA
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            // PRUEBA

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\bin\";
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.ApiKey("Token").Description("Filling bearer token here").Name("Authorization").In("header");
                    c.SingleApiVersion("v1", "LA POSITIVA - Portal Corredores")
                   .Description("Los servicios aquí descritos brindaran informacion requerida por el Portal de Corredores, donde se podra consultar los Dashboard de comparativo mensual y avance de producción de sus ingreso Neto Coaseguro.");
                    c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                    c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Employee Swagger UI");
                    c.EnableApiKeySupport("Authorization", "header");
                });
        }
    }
}

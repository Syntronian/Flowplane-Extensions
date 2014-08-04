using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FlowplaneExtensions
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.EnableCors();


            config.Routes.MapHttpRoute(
                name: "Api_Register_GetAll",
                routeTemplate: "api/register/getall",
                defaults: new { controller = "Api_Register_GetAll" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process",
                routeTemplate: "api/process/getassignees",
                defaults: new { controller = "Api_Process" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process",
                routeTemplate: "api/process/getworkspaces",
                defaults: new { controller = "Api_Process" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process",
                routeTemplate: "api/process/getprojects",
                defaults: new { controller = "Api_Process" }
            );
            
        }
    }
}

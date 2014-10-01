using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

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
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Routes.MapHttpRoute(
                name: "Api_Register_GetAll",
                routeTemplate: "api/register/getall",
                defaults: new { controller = "Api_Register", action = "GetAll" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Register_GetObjectTypeNameActivity",
                routeTemplate: "api/register/getobjecttypenameactivity",
                defaults: new { controller = "Api_Register", action = "GetObjectTypeNameActivity" }
            );
            
            config.Routes.MapHttpRoute(
                name: "Api_Register_GetObjectTypeNameEvent",
                routeTemplate: "api/register/getobjecttypenameevent",
                defaults: new { controller = "Api_Register", action = "GetObjectTypeNameEvent" }
            );



            config.Routes.MapHttpRoute(
                name: "Api_Process_GetAssignees",
                routeTemplate: "api/process/getassignees",
                defaults: new { controller = "Api_Process", action = "GetAssignees" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process_GetWorkspaces",
                routeTemplate: "api/process/getworkspaces",
                defaults: new { controller = "Api_Process", action = "GetWorkSpaces" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process_GetProjects",
                routeTemplate: "api/process/getprojects",
                defaults: new { controller = "Api_Process", action = "GetProjects" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Process_GetTasks",
                routeTemplate: "api/process/gettasks",
                defaults: new { controller = "Api_Process", action = "GetTasks" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Flow_ActivateObject",
                routeTemplate: "api/flow/activateobject",
                defaults: new { controller = "Api_Flow", action = "ActivateObject" }
            );

            config.Routes.MapHttpRoute(
                name: "Api_Flow_CreateTask",
                routeTemplate: "api/flow/completeactivity",
                defaults: new { controller = "Api_Flow", action = "CompleteActivity" }
            );
        }
    }
}

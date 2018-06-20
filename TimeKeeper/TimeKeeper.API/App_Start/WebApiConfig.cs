using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TimeKeeper.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //prvi parametar ide domen sa kojeg moze dolaziti npr www.mistral.ba
            //drugi sta mora biti u headeru npr source = ...
            //treci su akcije koje su dozvoljene npr GET i POST
            config.EnableCors(new EnableCorsAttribute("*", "*", "*", "Pagination"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //sort, filter
            config.Routes.MapHttpRoute(
                name: "PagingApi",
                routeTemplate: "api/{controller}/page/{page}/pagesize/{pagesize}",
                defaults: new
                {
                    page = RouteParameter.Optional,
                    pagesize = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "ReportApi",
                routeTemplate: "api/{controller}/{id}/{year}/{month}",
                defaults: new { year = RouteParameter.Optional, month = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = GlobalConfiguration.Configuration;
            //ukini xml - daj json
            json.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            json.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            json.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            json.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            json.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}

namespace Showcase.Server.Api
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Newtonsoft.Json.Serialization;

    using Showcase.Server.Api.Infrastructure;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
<<<<<<< HEAD
=======
                name: "ProjectsListApi",
                routeTemplate: "api/{controller}/List",
                defaults: new 
                {
                    controller = "ProjectsController",
                    pageIndex = RouteParameter.Optional 
                });
            
            config.Routes.MapHttpRoute(
>>>>>>> 85152f399c6bfcf7aae10aee29f6a7ba909f2593
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}

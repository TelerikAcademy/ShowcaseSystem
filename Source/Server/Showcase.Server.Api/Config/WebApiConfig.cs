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
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                name: "ProjectsPageApi",
                routeTemplate: "api/projects/{pageIndex}",
                defaults: new
                {
                    controller = "Projects",
                    pageIndex = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}

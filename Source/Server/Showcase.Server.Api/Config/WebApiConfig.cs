namespace Showcase.Server.Api.Config
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Newtonsoft.Json.Serialization;

    using Showcase.Server.Api.Infrastructure;
    using Showcase.Server.Api.Infrastructure.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}

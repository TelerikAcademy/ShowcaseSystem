namespace Showcase.Server.Api.Config
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Cors;
    using System.Web.Http.Routing;

    using Newtonsoft.Json.Serialization;

    using Showcase.Server.Infrastructure;
    using Showcase.Server.Infrastructure.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                "DefaultApiWithId",
                "Api/{controller}/{id}", 
                new { id = RouteParameter.Optional },
                new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                "DefaultApiWithAction",
                "Api/{controller}/{action}");

            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "Api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional },
                new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndUsername",
                "Api/{controller}/{action}/{username}");

            config.Routes.MapHttpRoute(
                "DefaultApiGet",
                "Api/{controller}",
                new { action = "Get" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                "DefaultApiPost",
                "Api/{controller}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}

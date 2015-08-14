namespace Showcase.Server.Api.Config
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Showcase.Server.Infrastructure.Formatters;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new RazorFormatter());
            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                "Files",
                "Files/{folder}/{file}",
                new { controller = "Files", action = "Get" },
                new { folder = @"\d+" });

            config.Routes.MapHttpRoute(
                "ProjectDetails",
                "api/Projects/{id}/{titleUrl}",
                new { controller = "Projects", action = "Get" },
                new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                "PagedComments",
                "api/Comments/{id}/{page}",
                new { controller = "Comments", action = "Get" },
                new { id = @"\d+", page = @"\d+" });

            config.Routes.MapHttpRoute(
                "PagedCommentsByUser",
                "api/Comments/User/{username}/{page}",
                new { controller = "Comments", action = "CommentsByUser" },
                new { page = @"\d+" });

            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "Api/{controller}/{action}/{id}",
                null,
                new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndUsername",
                "Api/{controller}/{action}/{username}");

            config.Routes.MapHttpRoute(
                "DefaultApiGetWithId",
                "Api/{controller}/{id}",
                new { action = "Get" },
                new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute(
                "DefaultApiPostWithId",
                "Api/{controller}/{id}",
                new { action = "Post" },
                new { id = @"\d+", httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                "DefaultApiWithAction",
                "Api/{controller}/{action}");

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

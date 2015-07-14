﻿namespace Showcase.Server.Api
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Showcase.Data;
    using Showcase.Data.Migrations;
    using Showcase.Server.Common.Mapping;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShowcaseDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfig.Execute(Assembly.Load("Showcase.Server.ViewModels"));
        }
    }
}

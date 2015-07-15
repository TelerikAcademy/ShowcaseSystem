﻿namespace Showcase.Server.Api
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    using Showcase.Data;
    using Showcase.Data.Migrations;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Mapping;

    public class WebApiStartup
    {
        public static void StartWebApi(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShowcaseDbContext, Configuration>());
            AutoMapperConfig.RegisterMappings(Assembly.Load(Constants.DataTransferModelsAssembly));

            var httpConfig = new HttpConfiguration();

            WebApiConfig.Register(httpConfig);

            app
                .UseNinjectMiddleware(NinjectConfig.CreateKernel)
                .UseNinjectWebApi(httpConfig);
        }
    }
}
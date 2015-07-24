﻿namespace Showcase.Server.Api.Infrastructure
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;

    using Microsoft.OData.Edm;

    using Showcase.Server.Api.Controllers;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Data.Models;

    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: GetEdmModel());
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "Showcase.Server.DataTransferModels.Project";
            builder.ContainerName = "DefaultContainer";

            builder.EntitySet<ProjectResponseSimpleModel, SearchController>();

            return builder.GetEdmModel();
        }
    }
}
namespace Showcase.Server.Api.Infrastructure
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;

    using Microsoft.OData.Edm;

    using Showcase.Server.Api.Controllers;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.Project;

    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute(
                routeName: "OData",
                routePrefix: "odata",
                model: GetEdmModel());
        }

        private static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<ProjectResponseSimpleModel, ProjectsController>();
            return modelBuilder.GetEdmModel();
        }
    }
}
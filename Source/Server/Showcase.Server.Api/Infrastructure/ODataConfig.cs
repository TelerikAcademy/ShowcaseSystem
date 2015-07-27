namespace Showcase.Server.Api.Infrastructure
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
            builder.EntitySet<ProjectResponseSimpleModel, SearchController>();
            builder.EntityType<ProjectResponseSimpleModel>().Property(x => x.ShortDate);
            builder.EntityType<ProjectResponseSimpleModel>().Property(x => x.TitleUrl);

            builder.Namespace = typeof(ProjectResponseSimpleModel).Namespace;
            builder.EnableLowerCamelCase();
            return builder.GetEdmModel();
        }
    }
}
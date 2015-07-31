namespace Showcase.Server.Api.Config
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;

    using Microsoft.OData.Edm;

    using Showcase.Server.Api.Controllers;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Infrastructure.Extensions;

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
            builder.EntitySet<ProjectSimpleResponseModel, SearchController>();
            builder.EntityType<ProjectSimpleResponseModel>().Property(x => x.ShortDate);
            builder.EntityType<ProjectSimpleResponseModel>().Property(x => x.TitleUrl);

            builder.Namespace = typeof(ProjectSimpleResponseModel).Namespace;
            builder.EnableLowerCamelCase();
            return builder.GetEdmModel();
        }
    }
}
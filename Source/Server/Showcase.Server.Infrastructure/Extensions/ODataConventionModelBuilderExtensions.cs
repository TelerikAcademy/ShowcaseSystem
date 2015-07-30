namespace Showcase.Server.Infrastructure.Extensions
{
    using System.Web.OData;
    using System.Web.OData.Builder;

    public static class ODataConventionModelBuilderExtensions
    {
        private const string Controller = "Controller";

        public static void EntitySet<TResponse, TController>(this ODataConventionModelBuilder modelBuilder)
            where TResponse : class
            where TController : ODataController
        {
            var controllerName = typeof(TController).Name.Replace(Controller, string.Empty);
            modelBuilder.EntitySet<TResponse>(controllerName);
        }
    }
}

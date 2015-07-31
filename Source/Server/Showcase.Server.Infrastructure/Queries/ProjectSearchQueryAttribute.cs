namespace Showcase.Server.Infrastructure.Queries
{
    using System.Web.OData;
    using System.Web.OData.Query;

    using Showcase.Server.Common;

    public class ProjectSearchQueryAttribute : EnableQueryAttribute
    {
        public ProjectSearchQueryAttribute()
        {
            this.MaxTop = Constants.MaxProjectsPageSize;
            this.AllowedFunctions = AllowedFunctions.Any | AllowedFunctions.SubstringOf;
            this.AllowedQueryOptions = AllowedQueryOptions.Top
                | AllowedQueryOptions.Skip
                | AllowedQueryOptions.Filter
                | AllowedQueryOptions.OrderBy
                | AllowedQueryOptions.Count;
        }
    }
}

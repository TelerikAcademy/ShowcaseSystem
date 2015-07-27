namespace Showcase.Server.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    public class SearchController : BaseODataController
    {
        private readonly IProjectsService projectsService;

        public SearchController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [HttpGet]
        [EnableQuery(MaxTop = Constants.MaxProjectsPageSize,
            AllowedQueryOptions = AllowedQueryOptions.Top | AllowedQueryOptions.Skip |
            AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Count,
            AllowedFunctions = AllowedFunctions.Any | AllowedFunctions.SubstringOf)]
        public IHttpActionResult Get()
        {
            var projects = this.projectsService
                .GetProjectsList()
                .Project()
                .To<ProjectSimpleResponseModel>();

            return Ok(projects);
        }
    }
}
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
    using Showcase.Server.Infrastructure.Queries;
    using Showcase.Services.Data.Contracts;

    public class SearchController : BaseODataController
    {
        private readonly IProjectsService projectsService;

        public SearchController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [HttpGet]
        [ProjectSearchQuery]
        public IHttpActionResult Get([FromUri]bool includeHidden = false)
        {
            var projects = this.projectsService
                .QueriedProjects(this.CurrentUser.IsAdmin, includeHidden) // TODO: make isAdmin optional parameter
                .Project()
                .To<ProjectSimpleResponseModel>();

            return this.Ok(projects);
        }
    }
}
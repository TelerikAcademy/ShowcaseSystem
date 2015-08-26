namespace Showcase.Server.Api.Controllers
{
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Infrastructure.Queries;
    using Showcase.Services.Data.Contracts;

    public class SearchController : BaseODataAuthorizationController
    {
        private readonly IProjectsService projectsService;

        public SearchController(IUsersService usersService, IProjectsService projectsService)
            : base(usersService)
        {
            this.projectsService = projectsService;
        }

        [HttpGet]
        [ProjectSearchQuery]
        public IHttpActionResult Get([FromUri]bool onlyHidden = false)
        {
            var projects = this.projectsService
                .QueriedProjects(this.CurrentUser.IsAdmin, onlyHidden)
                .Project()
                .To<ProjectSimpleResponseModel>();

            return this.Ok(projects);
        }
    }
}
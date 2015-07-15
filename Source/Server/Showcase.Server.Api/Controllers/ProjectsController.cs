namespace Showcase.Server.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Server.DataTransferModels.ProjectsPage;

    public class ProjectsController : ApiController
    {
        private IProjectsService projectService;

        public ProjectsController(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        public IHttpActionResult Get(int pageIndex = 0)
        {
            var model = this.projectService
                   .Next(pageIndex)
                   .Project()
                   .To<ProjectsPageProjectResponseModel>()
                   .ToList();

            return this.Data(model);
        }
    }
}
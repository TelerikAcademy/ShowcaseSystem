namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Common;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class ProjectsController : BaseODataController
    {
        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public IHttpActionResult Get()
        {
            var model = this.projectsService
                .LatestProjects()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        public IHttpActionResult Get(int id)
        {
            var model = this.projectsService
                .GetProjectById(id)
                .Project()
                .To<ProjectResponseModel>()
                .FirstOrDefault();

            return this.Data(model);
        }

        [EnableQuery]
        public IHttpActionResult GetList([FromODataUri] int page)
        {
            var model = this.projectsService
                .GetProjectsPage(page)
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }
    }
}
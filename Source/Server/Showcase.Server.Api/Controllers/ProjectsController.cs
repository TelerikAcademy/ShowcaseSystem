namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Common;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class ProjectsController : ApiController
    {
        private readonly IProjectsService homePageService;

        public ProjectsController(IProjectsService homePageService)
        {
            this.homePageService = homePageService;
        }

        public IHttpActionResult Get()
        {
            var model = this.homePageService
                .LatestProjects()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        public IHttpActionResult Get(int id)
        {
            var model = this.homePageService
                .GetProjectById(id)
                .Project()
                .To<ProjectResponseModel>()
                .FirstOrDefault();

            return this.Data(model);
        }
    }
}
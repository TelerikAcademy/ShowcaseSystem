namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    public class ProjectsController : ApiController
    {
        private readonly IProjectsService homePageService;

        private readonly ILikesService likesService;

        private readonly IVisitsService visitsService;

        public ProjectsController(IProjectsService homePageService, ILikesService likesService, IVisitsService visitsService)
        {
            this.homePageService = homePageService;
            this.likesService = likesService;
            this.visitsService = visitsService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.homePageService
                .LatestProjects()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var username = this.User.Identity.Name;

            this.visitsService.VisitProject(id, username);

            var model = this.homePageService
                .GetProjectById(id)
                .Project()
                .To<ProjectResponseModel>()
                .FirstOrDefault();

            return this.Data(model);
        }

        // [Authorize]
        [HttpPost]
        [Route("api/Projects/Like/{id}")]
        public IHttpActionResult Like(int id)
        {
            var username = this.User.Identity.Name;

            if (this.likesService.AllLikesForProject(id).Any(l => l.ProjectId == id && l.User.Username == username))
            {
                return this.Data(false, "You already have liked this project.");
            }

            this.likesService.LikeProject(id, username);

            return this.Ok();
        }

        // [Authorize]
        [HttpPost]
        [Route("api/Projects/DisLike/{id}")]
        public IHttpActionResult DisLike(int id)
        {
            var username = this.User.Identity.Name;

            if (!this.likesService.AllLikesForProject(id).Any(l => l.User.Username == username))
            {
                return this.Data(false, "You have not yet liked this project.");
            }

            this.likesService.DisLikeProject(id, username);

            return this.Ok();
        }
    }
}
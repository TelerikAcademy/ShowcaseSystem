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
    using Showcase.Server.DataTransferModels;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService homePageService;

        private readonly ILikesService likesService;

        private readonly IVisitsService visitsService;

        private readonly IProjectsService projectsService;

        private readonly IUsersService usersService;

        public ProjectsController(
            IProjectsService homePageService,
            ILikesService likesService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            IUsersService usersService)
        {
            this.homePageService = homePageService;
            this.likesService = likesService;
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.homePageService
                .LatestProjects()
                .Project()
                .To<ProjectResponseSimpleModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpGet]
        [Route("Popular")]
        public IHttpActionResult Popular()
        {
            var model = this.homePageService
                .MostPopular()
                .Project()
                .To<ProjectResponseSimpleModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var username = this.User.Identity.Name;

            var model = this.homePageService
                .GetProjectById(id)
                .Project()
                .To<ProjectResponseModel>()
                .FirstOrDefault();

            model.IsLiked = this.likesService.ProjectIsLikedByUser(id, username);

            return this.Data(model);
        }

        [HttpGet]
        [Route("LikedProjects/{username}")]
        public IHttpActionResult LikedProjects(string username)
        {
            var currentLoggedInUsername = this.User.Identity.Name;
            if (username != currentLoggedInUsername.ToLower())
            {
                return this.Data(false, "You are not authorized to view this user's liked projects.");
            }

            var userId = this.usersService.GetUserId(username);

            var model = this.projectsService
                .GetLikedByUser(userId)
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpPost]
        [Route("Visit/{id}")]
        public IHttpActionResult Visit(int id)
        {
            var username = this.User.Identity.Name;

            this.visitsService.VisitProject(id, username);

            return this.Ok();
        }

        //[Authorize]
        [HttpPost]
        [Route("Like/{id}")]
        public IHttpActionResult Like(int id)
        {
            var username = this.User.Identity.Name;

            if (this.likesService.ProjectIsLikedByUser(id, username))
            {
                return this.Data(false, "You already have liked this project.");
            }

            this.likesService.LikeProject(id, username);

            return this.Ok();
        }

        //[Authorize]
        [HttpPost]
        [Route("DisLike/{id}")]
        public IHttpActionResult DisLike(int id)
        {
            var username = this.User.Identity.Name;

            if (!this.likesService.ProjectIsLikedByUser(id, username))
            {
                return this.Data(false, "You have not yet liked this project.");
            }

            this.likesService.DislikeProject(id, username);

            return this.Ok();
        }
    }
}
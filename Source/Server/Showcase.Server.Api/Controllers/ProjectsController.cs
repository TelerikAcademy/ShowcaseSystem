namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Query;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.Api.Infrastructure.Validation;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly ILikesService likesService;
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IUsersService usersService;

        public ProjectsController(
            ILikesService likesService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            ITagsService tagsService,
            IUsersService usersService)
        {
            this.likesService = likesService;
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var model = this.projectsService
                .LatestProjects()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Post(ProjectRequestModel project)
        {
            var collaborators = this.usersService.GetCollaborators(project.Collaborators);
            var tags = this.tagsService.GetTags(project.Tags);

            return this.Ok();
        }

        [HttpGet]
        [Route("Popular")]
        public IHttpActionResult Popular()
        {
            var model = this.projectsService
                .MostPopular()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var username = this.User.Identity.Name;

            var model = this.projectsService
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

        // [Authorize]
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

        // [Authorize]
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

        [HttpGet]
        [Route("Search")]
        public ODataResult<ProjectSimpleResponseModel> Search(ODataQueryOptions<ProjectSimpleResponseModel> options)
        {
            options.Validate(new ODataValidationSettings()
            {
                MaxTop = 64,
                AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy |
                    AllowedQueryOptions.Skip | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Select | AllowedQueryOptions.Count
            });

            var projects = this.projectsService
                .GetProjectsList()
                .Project()
                .To<ProjectSimpleResponseModel>();

            long? count = null;
            if (options.Count != null && options.Count.Value)
            {
                count = projects.Count();
            }

            ODataQuerySettings settings = new ODataQuerySettings() { PageSize = options.Top != null ? options.Top.Value : 8 };
            projects = options.ApplyTo(projects, settings) as IQueryable<ProjectSimpleResponseModel>;

            return new ODataResult<ProjectSimpleResponseModel>(
                 projects as IEnumerable<ProjectSimpleResponseModel>, count);
        }
    }
}
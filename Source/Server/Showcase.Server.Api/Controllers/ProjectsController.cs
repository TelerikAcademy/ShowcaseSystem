namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectsService projectsService;

        private readonly ILikesService likesService;

        private readonly IVisitsService visitsService;

        public ProjectsController(IProjectsService projectsService, ILikesService likesService, IVisitsService visitsService)
        {
            this.projectsService = projectsService;
            this.likesService = likesService;
            this.visitsService = visitsService;
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
         // return this.Data(false, "You have not yet liked this project.");
            }

            this.likesService.DislikeProject(id, username);

            return this.Ok();
        }

        [HttpPost]
        [Route("Comment/{id}")]
        public IHttpActionResult Comment(int id)
        {
            return this.Ok();
        }

        [HttpGet]
        [Route("api/odata/Search")]
        public ODataResult<ProjectResponseSimpleModel> Search(ODataQueryOptions<ProjectResponseSimpleModel> options)
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
                .To<ProjectResponseSimpleModel>();

            long? count = null;
            if (options.Count != null && options.Count.Value)
            {
                count = projects.Count();
            }

            ODataQuerySettings settings = new ODataQuerySettings() { PageSize = options.Top.Value };
            projects = options.ApplyTo(projects, settings) as IQueryable<ProjectResponseSimpleModel>;

            return new ODataResult<ProjectResponseSimpleModel>(
                 projects as IEnumerable<ProjectResponseSimpleModel>, count);
        }
    }
}
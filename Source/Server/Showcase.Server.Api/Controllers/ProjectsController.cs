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
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

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

            this.visitsService.VisitProject(id, username);

            var model = this.projectsService
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

        [HttpPost]
        [Route("api/Projects/Comment/{id}")]
        public IHttpActionResult Comment(int id)
        {
            return this.Ok();
        }

        [HttpGet]
        [Route("odata/Search")]
        [EnableQuery(PageSize = 10,
            AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.Select)]
        public IQueryable<ProjectResponseSimpleModel> Search()
        {
            var model = this.projectsService
                .GetProjectsList()
                .Project()
                .To<ProjectResponseSimpleModel>();

            return model;
        }
    }
}
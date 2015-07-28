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
    using Showcase.Data.Models;
    using Showcase.Server.Api.Infrastructure.FileSystem;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.Api.Infrastructure.Validation;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : ApiController
    {
        private readonly ILikesService likesService;
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IUsersService usersService;
        private readonly IImagesService imagesService;
        private readonly IFileSystemService fileSystemService;

        public ProjectsController(
            ILikesService likesService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            ITagsService tagsService,
            IUsersService usersService,
            IImagesService imagesService,
            IFileSystemService fileSystemService)
        {
            this.likesService = likesService;
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
            this.usersService = usersService;
            this.imagesService = imagesService;
            this.fileSystemService = fileSystemService;
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
            var collaborators = this.usersService.GetCollaboratorsFromCommaSeparatedValues(project.Collaborators);
            var tags = this.tagsService.GetTagsFromCommaSeparatedValues(project.Tags);
            var processedImages = this.imagesService.ProcessImages(project.Images.Select(FileRequestModel.ToRawImage));
            processedImages.ForEach(pi => 
            {
                this.fileSystemService.SaveImageToFile(pi.ThumbnailContent, pi.UrlPath, ProcessedImage.ThumbnailImage);
                this.fileSystemService.SaveImageToFile(pi.HighResolutionContent, pi.UrlPath, ProcessedImage.HighResolutionImage);
            });

            var addedProject = this.projectsService.Add(
                Mapper.Map<Project>(project),
                collaborators, 
                tags, 
                processedImages,
                project.MainImage);

            return this.Ok(new { addedProject.Id, addedProject.Title }); // TODO: extract response model
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
                .GetProjectsList() // TODO: it is not list, it is IQueryable, remove the Hungarian notation
                .Project()
                .To<ProjectSimpleResponseModel>();

            long? count = null;
            if (options.Count != null && options.Count.Value)
            {
                count = projects.Count();
            }

            ODataQuerySettings settings = new ODataQuerySettings() { PageSize = options.Top != null ? options.Top.Value : 8 };
            projects = options.ApplyTo(projects, settings) as IQueryable<ProjectSimpleResponseModel>; // TODO: move to extension method of IQueryable

            return new ODataResult<ProjectSimpleResponseModel>(
                 projects as IEnumerable<ProjectSimpleResponseModel>, count); // TODO: move to extension method like this.Data
        }
    }
}
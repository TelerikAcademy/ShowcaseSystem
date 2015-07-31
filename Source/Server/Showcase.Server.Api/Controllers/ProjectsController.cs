namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Query;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.FileSystem;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    [RoutePrefix("api/Projects")]
    public class ProjectsController : BaseController
    {
        private readonly ILikesService likesService;
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IUsersService usersService;
        private readonly IMappingService mappingService;
        private readonly IImagesService imagesService;
        private readonly IFileSystemService fileSystemService;

        private readonly IFlagsService flagsService;

        public ProjectsController(
            ILikesService likesService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            IUsersService usersService,
            IFlagsService flagsService,
            ITagsService tagsService,
            IMappingService mappingService,
            IImagesService imagesService,
            IFileSystemService fileSystemService)
        {
            this.likesService = likesService;
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
            this.usersService = usersService;
            this.flagsService = flagsService;
            this.mappingService = mappingService;
            this.imagesService = imagesService;
            this.fileSystemService = fileSystemService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = await this.projectsService
                .LatestProjects()
                .Project()
                .To<ProjectSimpleResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ProjectRequestModel project)
        {
            var collaborators = await this.usersService.CollaboratorsFromCommaSeparatedValues(project.Collaborators);
            var tags = await this.tagsService.TagsFromCommaSeparatedValues(project.Tags);
            var processedImages = await this.imagesService.ProcessImages(project.Images.Select(FileRequestModel.ToRawImage));
            await this.fileSystemService.SaveImagesToFiles(processedImages);

            var addedProject = await this.projectsService.AddNew(
                this.mappingService.Map<Project>(project),
                collaborators, 
                tags, 
                processedImages,
                project.MainImage);

            return this.Ok(this.mappingService.Map<PostProjectResponseModel>(addedProject));
        }

        [HttpGet]
        [Route("Popular")]
        public async Task<IHttpActionResult> Popular()
        {
            var model = await this.projectsService
                .MostPopular()
                .Project()
                .To<ProjectSimpleResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var username = this.User.Identity.Name;

            var model = await this.projectsService
                .ProjectById(id)
                .Project()
                .To<ProjectResponseModel>()
                .FirstOrDefaultAsync();

            model.IsLiked = await this.likesService.ProjectIsLikedByUser(id, username); // TODO: merge in one query
            model.IsFlagged = await this.flagsService.ProjectIsFlaggedByUser(id, username); // TODO: merge in one query

            return this.Data(model);
        }

        [HttpGet]
        [Route("LikedProjects/{username}")]
        public async Task<IHttpActionResult> LikedProjects(string username)
        {
            var currentLoggedInUsername = this.User.Identity.Name;
            if (username != currentLoggedInUsername.ToLower())
            {
                return this.Data(false, "You are not authorized to view this user's liked projects.");
            }

            var userId = await this.usersService.UserIdByUsername(username);

            var model = await this.projectsService
                .LikedByUser(userId)
                .Project()
                .To<ProjectResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [HttpPost]
        [Route("Visit/{id}")]
        public async Task<IHttpActionResult> Visit(int id)
        {
            var username = this.User.Identity.Name;

            await this.visitsService.VisitProject(id, username);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("Like/{id}")]
        public async Task<IHttpActionResult> Like(int id)
        {
            var username = this.User.Identity.Name;

            if (await this.likesService.ProjectIsLikedByUser(id, username))
            {
                return this.Data(false, "You already have liked this project.");
            }

            await this.likesService.LikeProject(id, username);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("Dislike/{id}")]
        public async Task<IHttpActionResult> Dislike(int id)
        {
            var username = this.User.Identity.Name;

            if (!await this.likesService.ProjectIsLikedByUser(id, username))
            {
                return this.Data(false, "You have not yet liked this project.");
            }

            await this.likesService.DislikeProject(id, username);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("Flag/{id}")]
        public async Task<IHttpActionResult> Flag(int id)
        {
            var username = this.User.Identity.Name;

            if (await this.flagsService.ProjectIsFlaggedByUser(id, username))
            {
                return this.Data(false, "You can't flagg the same project more than once.");
            }

            await this.flagsService.FlagProject(id, username);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("Unflag/{id}")]
        public async Task<IHttpActionResult> Unflag(int id)
        {
            var username = this.User.Identity.Name;

            if (!await this.flagsService.ProjectIsFlaggedByUser(id, username))
            {
                return this.Data(false, "You have not yet flagged this project.");
            }

            await this.flagsService.UnFlagProject(id, username);

            return this.Ok();
        }        
    }
}
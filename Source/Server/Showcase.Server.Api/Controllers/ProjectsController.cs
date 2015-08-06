namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Common;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.FileSystem;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Logic.Contracts;
    using Showcase.Server.Common;

    public class ProjectsController : BaseAuthorizationController
    {
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IMappingService mappingService;
        private readonly IImagesService imagesService;
        private readonly IFileSystemService fileSystemService;
        
        public ProjectsController(
            IUsersService usersService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            ITagsService tagsService,
            IMappingService mappingService,
            IImagesService imagesService,
            IFileSystemService fileSystemService)
            : base(usersService)
        {
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
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

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, string titleUrl)
        {
            var isAuthenticated = this.CurrentUser != null;
            var username = isAuthenticated ? this.CurrentUser.UserName : null;

            var model = await this.projectsService
                .ProjectById(id, isAuthenticated ? this.CurrentUser.IsAdmin : false)
                .Project()
                .To<ProjectResponseModel>(new { username })
                .FirstOrDefaultAsync();

            if (model.TitleUrl == titleUrl)
            {
                return this.Data(model);
            }
            else
            {
                return this.Data(false, Constants.RequestedResourceWasNotFound);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ProjectRequestModel project)
        {
            var collaborators = await this.UsersService.CollaboratorsFromCommaSeparatedValues(project.Collaborators, this.CurrentUser.UserName);
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
        public async Task<IHttpActionResult> LikedProjects(string username)
        {
            if (username != this.CurrentUser.UserName.ToLower())
            {
                return this.Data(false, "You are not authorized to view this user's liked projects."); // TODO: Move to common attribute
            }

            var userId = await this.UsersService.UserIdByUsername(username);

            var model = await this.projectsService
                .LikedByUser(userId)
                .Project()
                .To<ProjectResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Visit([FromBody]int id)
        {
            await this.visitsService.VisitProject(id, this.CurrentUser.UserName);
            return this.Ok();
        }
    }
}
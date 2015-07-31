namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.FileSystem;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Logic.Contracts;

    public class ProjectsController : BaseController
    {
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IUsersService usersService;
        private readonly IMappingService mappingService;
        private readonly IImagesService imagesService;
        private readonly IFileSystemService fileSystemService;


        public ProjectsController(
            IVisitsService visitsService,
            IProjectsService projectsService,
            IUsersService usersService,
            ITagsService tagsService,
            IMappingService mappingService,
            IImagesService imagesService,
            IFileSystemService fileSystemService)
        {
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
            this.usersService = usersService;
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
        public async Task<IHttpActionResult> Get(int id)
        {
            var username = this.User.Identity.Name;

            var model = await this.projectsService
                .ProjectById(id, this.CurrentUser.IsAdmin)
                .Project()
                .To<ProjectResponseModel>(new { username })
                .FirstOrDefaultAsync();

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
            var currentLoggedInUsername = this.User.Identity.Name;
            if (username != currentLoggedInUsername.ToLower())
            {
                return this.Data(false, "You are not authorized to view this user's liked projects."); // TODO: Move to common attribute
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
        public async Task<IHttpActionResult> Visit(int id)
        {
            var username = this.User.Identity.Name;

            await this.visitsService.VisitProject(id, username);

            return this.Ok();
        }
    }
}
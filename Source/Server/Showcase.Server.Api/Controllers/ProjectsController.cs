namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Models;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.Infrastructure.Caching;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.FileSystem;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class ProjectsController : BaseAuthorizationController
    {
        private readonly IVisitsService visitsService;
        private readonly IProjectsService projectsService;
        private readonly ITagsService tagsService;
        private readonly IMappingService mappingService;
        private readonly IImagesService imagesService;
        private readonly IDownloadableFilesService downloadableFilesService;
        private readonly IFileSystemService fileSystemService;
        private readonly IProjectsCacheService projectsCacheService;

        public ProjectsController(
            IUsersService usersService,
            IVisitsService visitsService,
            IProjectsService projectsService,
            ITagsService tagsService,
            IMappingService mappingService,
            IImagesService imagesService,
            IDownloadableFilesService downloadableFilesService,
            IFileSystemService fileSystemService,
            IProjectsCacheService projectsCacheService)
            : base(usersService)
        {
            this.visitsService = visitsService;
            this.projectsService = projectsService;
            this.tagsService = tagsService;
            this.mappingService = mappingService;
            this.imagesService = imagesService;
            this.downloadableFilesService = downloadableFilesService;
            this.fileSystemService = fileSystemService;
            this.projectsCacheService = projectsCacheService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = await this.projectsCacheService.LatestProjects();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, string titleUrl)
        {
            var isAuthenticated = this.CurrentUser != null;
            var username = isAuthenticated ? this.CurrentUser.UserName : null;

            var model = await this.projectsService
                .ProjectById(id, isAuthenticated && this.CurrentUser.IsAdmin)
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
            var processedImages = await this.imagesService.ProcessImages(project.Images.Select(FileRequestModel.ToRawFile));
            var downloadableFiles = await this.downloadableFilesService.AddNew(
                project.Files != null
                ? project.Files.Select(FileRequestModel.ToRawFile)
                : new List<RawFile>());

            await this.fileSystemService.SaveImages(processedImages);
            await this.fileSystemService.SaveDownloadableFiles(downloadableFiles);

            var addedProject = await this.projectsService.AddNew(
                this.mappingService.Map<Project>(project),
                collaborators,
                tags,
                processedImages,
                project.MainImage,
                downloadableFiles);

            return this.Ok(this.mappingService.Map<PostProjectResponseModel>(addedProject));
        }

        [Authorize]
        [AuthorizeEdit]
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Edit(EditProjectRequestModel project)
        {
            var newCollaborators = await this.UsersService.CollaboratorsFromCommaSeparatedValues(project.NewCollaborators);
            var deletedCollaborators = await this.UsersService.CollaboratorsFromCommaSeparatedValues(project.DeletedCollaborators);
            var requiredTags = await this.tagsService.TagsFromCommaSeparatedValues(project.RequiredTags);
            var newUserTags = await this.tagsService.TagsFromCommaSeparatedValues(project.NewUserTags);
            var deletedUserTags = await this.tagsService.TagsFromCommaSeparatedValues(project.DeletedUserTags);
            var existingProject = await this.projectsService.ProjectByIdWithIncludedCollaboratorsTagsAndImages(project.Id).FirstOrDefaultAsync();
            var images = await this.imagesService.ImagesByUrls(project.UpdatedImageUrls);

            await this.projectsService.Edit(
                this.mappingService.Map(project, existingProject),
                newCollaborators,
                deletedCollaborators,
                requiredTags,
                newUserTags,
                deletedUserTags,
                images,
                project.UpdatedMainImageUrl);
            
            return this.Ok(this.mappingService.Map<PostProjectResponseModel>(existingProject));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Popular()
        {
            var model = await this.projectsCacheService.PopularProjects();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> LikedProjects(string username)
        {
            if (username != this.CurrentUser.UserName.ToLower() && !this.CurrentUser.IsAdmin)
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
        public async Task<IHttpActionResult> Visit(int id)
        {
            await this.visitsService.VisitProject(id);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Hide(int id)
        {
            var isOwnProject = await this.projectsService
                .ProjectById(id, this.CurrentUser.IsAdmin)
                .AnyAsync(p => p.Collaborators.Any(c => c.UserName == this.CurrentUser.UserName));

            if (!isOwnProject && !this.CurrentUser.IsAdmin)
            {
                return this.Data(false, "You are not authorized to hide this project.");
            }

            await this.projectsService.HideProject(id);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Unhide(int id)
        {
            if (!this.CurrentUser.IsAdmin)
            {
                return this.Data(false, "You must be an admin to unhide a project.");
            }

            await this.projectsService.UnhideProject(id);
            return this.Ok();
        }
    }
}
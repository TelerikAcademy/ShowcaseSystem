namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    
    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projects;
        private readonly IRepository<Image> images;
        private readonly IRepository<File> files;
        private readonly IRepository<Flag> flags;

        public ProjectsService(IRepository<Project> projects, IRepository<Image> images, IRepository<File> files, IRepository<Flag> flags)
        {
            this.projects = projects;
            this.images = images;
            this.files = files;
            this.flags = flags;
        }

        public IQueryable<Project> LatestProjects()
        {
            return this.projects
                .All()
                .Where(p => !p.IsHidden)
                .OrderByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount);
        }

        public IQueryable<Project> MostPopular()
        {
            return this.projects
                .All()
                .Where(p => !p.IsHidden && !p.Collaborators.Any(c => c.IsAdmin))
                .OrderByDescending(pr => pr.Likes.Count)
                .ThenByDescending(pr => pr.Comments.Count)
                .ThenByDescending(pr => pr.Visits.Count)
                .ThenByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount);
        }

        public IQueryable<Project> ProjectById(int id, bool isAdmin = false)
        {
            var query = this.projects.All().Where(p => p.Id == id);

            if (!isAdmin)
            {
                query = query.Where(p => !p.IsHidden);
            }

            return query;
        }

        public IQueryable<Project> ProjectByIdWithIncludedCollaboratorsTagsAndImages(int id, bool isAdmin = false)
        {
            return this.ProjectById(id, isAdmin)
                .Include(pr => pr.Collaborators)
                .Include(pr => pr.Tags)
                .Include(pr => pr.Images);
        }

        public IQueryable<Project> QueriedProjects(bool isAdmin = false, bool onlyHidden = false)
        {
            var query = this.projects
                .All()
                .Where(p => p.IsHidden == (onlyHidden && isAdmin));
            
            return query;
        }

        public IQueryable<Project> LikedByUser(int userId)
        {
            return this.projects
                .All()
                .Where(pr => !pr.IsHidden && pr.Likes.Any(l => l.UserId == userId));
        }

        public async Task<Project> AddNew(
            Project project,
            ICollection<User> collaborators,
            IEnumerable<Tag> tags,
            IEnumerable<ProcessedImage> processedImages,
            string mainImage,
            IEnumerable<File> downloadableFiles)
        {
            collaborators.ForEach(c => project.Collaborators.Add(c));
            tags.ForEach(t => project.Tags.Add(t));
            processedImages.Select(ProcessedImage.ToImage).ForEach(image => { image = this.images.Attach(image); project.Images.Add(image); });
            project.MainImageId = this.GetMainImageId(project, mainImage);
            downloadableFiles.ForEach(file => { file = this.files.Attach(file); project.Files.Add(file); });
            this.projects.Add(project);
            await this.projects.SaveChangesAsync();
            return project;
        }

        public async Task Edit(
            Project project,
            IEnumerable<User> newCollaborators,
            IEnumerable<User> deletedCollaborators,
            IEnumerable<Tag> requiredTags,
            IEnumerable<Tag> newUserTags,
            IEnumerable<Tag> deletedUserTags,
            IEnumerable<Image> updatedImages,
            string updatedMainImageUrl)
        {
            deletedCollaborators.ForEach(c => project.Collaborators.Remove(c));
            newCollaborators.ForEach(c =>
            {
                if (!project.Collaborators.Contains(c))
                {
                    project.Collaborators.Add(c);
                }
            });

            project
                .Tags
                .Where(t => t.Type != TagType.UserSubmitted)
                .ToList()
                .ForEach(t => project.Tags.Remove(t));

            requiredTags.ForEach(t => project.Tags.Add(t));

            deletedUserTags.ForEach(t => project.Tags.Remove(t));
            newUserTags.ForEach(t =>
            {
                if (!project.Tags.Contains(t))
                {
                    project.Tags.Add(t);
                }
            });

            project
                .Images
                .ToList()
                .ForEach(i => project.Images.Remove(i));

            updatedImages.ForEach(i => project.Images.Add(i));
            project.MainImage = updatedImages.FirstOrDefault(i => i.UrlPath == updatedMainImageUrl);

            this.projects.Update(project);
            await this.projects.SaveChangesAsync();
        }

        public async Task HideProject(int id)
        {
            var project = this.projects.All().FirstOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.IsHidden = true;
                await this.projects.SaveChangesAsync();
            }
        }

        public async Task UnhideProject(int id)
        {
            var project = await this.projects.All().Include(p => p.Flags).FirstOrDefaultAsync(p => p.Id == id);

            if (project != null)
            {
                if (project.Flags.Any())
                {
                    var flagsToDelete = await this.flags.All().Where(f => f.ProjectId == id).ToListAsync();
                    flagsToDelete.ForEach(f => this.flags.Delete(f));
                }

                project.IsHidden = false;

                await this.projects.SaveChangesAsync();
            }
        }

        private int GetMainImageId(Project project, string mainImage)
        {
            var id = project
                .Images
                .Where(pi => pi.OriginalFileName == mainImage)
                .Select(i => i.Id)
                .FirstOrDefault();

            if (id == 0)
            {
                id = project.Images.Select(i => i.Id).First();
            }

            return id;
        }
    }
}
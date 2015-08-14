namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public ProjectsService(IRepository<Project> projects, IRepository<Image> images, IRepository<File> files)
        {
            this.projects = projects;
            this.images = images;
            this.files = files;
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

        public IQueryable<Project> QueriedProjects(bool isAdmin = false, bool includeHidden = false)
        {
            var query = this.projects.All();
            
            if (!includeHidden || !isAdmin)
            {
                query = query.Where(p => !p.IsHidden);
            }

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

        public async Task HideProject(int id)
        {
            var project = this.projects.All().FirstOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.IsHidden = true;
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
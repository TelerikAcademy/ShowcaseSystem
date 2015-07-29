namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    
    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<Project> projects;

        public ProjectsService(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public IQueryable<Project> LatestProjects()
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount);
        }

        public IQueryable<Project> MostPopular()
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.Likes.Count)
                .ThenByDescending(pr => pr.Comments.Count)
                .ThenByDescending(pr => pr.Visits.Count)
                .ThenByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount);
        }

        public IQueryable<Project> ProjectById(int id)
        {
            return this.projects
                .All()
                .Where(p => p.Id == id);
        }

        public IQueryable<Project> QueriedProjects()
        {
            return this.projects.All();
        }

        public IQueryable<Project> LikedByUser(int userId)
        {
            return this.projects
                .All()
                .Where(pr => pr.Likes.Any(l => l.UserId == userId));
        }

        public Project Add(Project project, ICollection<User> collaborators, IEnumerable<Tag> tags, IEnumerable<ProcessedImage> processedImages, string mainImage)
        {
            collaborators.ForEach(c => project.Collaborators.Add(c));
            tags.ForEach(t => project.Tags.Add(t));
            processedImages.Select(ProcessedImage.ToImage).ForEach(pi => project.Images.Add(pi));
            project.MainImageId = this.GetMainImageId(project, mainImage);
            this.projects.Add(project);
            this.projects.SaveChanges();
            return project;
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
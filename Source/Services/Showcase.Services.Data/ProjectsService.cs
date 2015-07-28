﻿namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
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

        public IQueryable<Project> GetProjectById(int id)
        {
            return this.projects
                .All()
                .Where(p => p.Id == id);
        }

        public IQueryable<Project> GetProjectsList()
        {
            return this.projects.All();
        }

        public IQueryable<Project> GetLikedByUser(int userId)
        {
            return this.projects
                .All()
                .Where(pr => pr.Likes.Any(l => l.UserId == userId));
        }


        public Project Add(Project project, ICollection<User> collaborators, IEnumerable<Tag> tags, IEnumerable<ProcessedImage> processedImages, string mainImage)
        {
            throw new System.NotImplementedException();
        }
    }
}
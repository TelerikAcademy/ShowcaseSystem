namespace Showcase.Services.Data
{
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;
    
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
    }
}
namespace Showcase.Services.Data
{
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;

    public class ProjectsService : IProjectsService
    {
        private IRepository<Project> projectsRepo;

        public ProjectsService(IRepository<Project> projectsRepo)
        {
            this.projectsRepo = projectsRepo;
        }

        public IQueryable<Project> Next(int pageIndex = 0)
        {
            return this.projectsRepo.All()
                .OrderByDescending(p => p.CreatedOn)
                .Skip(pageIndex * Constants.HomePageLatestProjectsCount)
                .Take(Constants.HomePageLatestProjectsCount);
        }
    }
}

namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using Showcase.Services.Common;

    using System.Linq;

    public interface IProjectsService : IService
    {
        IQueryable<Project> LatestProjects();

        IQueryable<Project> GetProjectById(int id);

        IQueryable<Project> GetProjectsPage(int page);
    }
}

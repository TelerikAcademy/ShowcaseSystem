namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IProjectsService : IService
    {
        IQueryable<Project> LatestProjects();

        IQueryable<Project> GetProjectById(int id);
    }
}

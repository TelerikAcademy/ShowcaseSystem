namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IProjectsService : IService
    {
        IQueryable<Project> LatestProjects();

        IQueryable<Project> MostPopular();

        IQueryable<Project> GetProjectById(int id);

<<<<<<< HEAD
        IQueryable<Project> GetProjectsList();
=======
        IQueryable<Project> GetProjectsPage(int page);
>>>>>>> 85152f399c6bfcf7aae10aee29f6a7ba909f2593

        IQueryable<Project> GetLikedByUser(int userId);
    }
}

namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IStatisticsService : IService
    {
        IQueryable<IGrouping<int, Project>> Current();

        IQueryable<IGrouping<int, Project>> ProjectsLastSixMonths();

        IQueryable<Tag> ProjectsCountByTag();

        IQueryable<Project> TopProjects();

        IQueryable<User> TopUsers();
    }
}
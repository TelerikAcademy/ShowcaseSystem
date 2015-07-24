namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Services.Common;

    public interface IStatisticsService : IService
    {
        object Current();

        IQueryable<CountByDateModel> ProjectsLastSixMonths();

        IQueryable<CountByTagModel> ProjectsCountByTag();

        IQueryable<Project> MostLikedProjects();
    }
}
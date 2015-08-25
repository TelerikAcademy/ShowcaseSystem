namespace Showcase.Server.Infrastructure.Caching
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Services.Common;

    public interface IStatisticsCacheService : IService
    {
        Task<CurrentStatisticsResponseModel> Statistics();

        Task<LineChartResponseModel> ProjectsLastSixMonths();

        Task<IEnumerable<CountByTagModel>> ProjectsCountByTag();

        Task<IEnumerable<TopProjectResponseModel>> TopProjects();

        Task<IEnumerable<TopUserResponseModel>> TopUsers();
    }
}

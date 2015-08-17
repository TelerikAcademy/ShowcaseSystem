namespace Showcase.Server.Infrastructure.Caching
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Services.Common;

    public interface ICacheService : IService
    {
        Task<IEnumerable<ProjectSimpleResponseModel>> LatestProjects();

        Task<IEnumerable<ProjectSimpleResponseModel>> PopularProjects();

        Task<CurrentStatisticsResponseModel> Statistics();
    }
}

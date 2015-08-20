namespace Showcase.Server.Infrastructure.Caching
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.Infrastructure.Caching.Base;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;

    public class StatisticsCacheService : BaseCacheService, IStatisticsCacheService
    {
        private const string StatisticsCacheKey = "Statistics";
        private const string ProjectsLastSixMonthsCacheKey = "ProjectsLastSixMonths";
        private const string ProjectsCountByTagCacheKey = "ProjectsCountByTag";
        private const string TopProjectsCacheKey = "TopProjects";
        private const string TopUsersCacheKey = "TopUsers";

        private readonly IStatisticsService statisticsService;

        public StatisticsCacheService(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public async Task<CurrentStatisticsResponseModel> Statistics()
        {
            return await this.Get<CurrentStatisticsResponseModel>(
                StatisticsCacheKey,
                async () =>
                    await this.statisticsService
                        .Current()
                        .Project()
                        .To<CurrentStatisticsResponseModel>()
                        .FirstOrDefaultAsync(),
                this.DefaultAbsoluteExpiration);
        }

        public async Task<LineChartResponseModel> ProjectsLastSixMonths()
        {
            return await this.Get<LineChartResponseModel>(
                ProjectsLastSixMonthsCacheKey,
                async () =>
                {
                    var totalProjectsPerMonth = 0;
                    var model = new LineChartResponseModel();

                    (await this.statisticsService
                        .ProjectsLastSixMonths()
                        .Select(CountByDateModel.FromProjectGrouping)
                        .ToListAsync())
                        .ForEach(r =>
                        {
                            totalProjectsPerMonth += r.Count;
                            model.Values.Add(totalProjectsPerMonth);
                            model.Labels.Add(r.Date.Month.ToMonthName());
                        });

                    return model;
                },
                this.DefaultAbsoluteExpiration);
        }

        public async Task<IEnumerable<CountByTagModel>> ProjectsCountByTag()
        {
            return await this.Get<IEnumerable<CountByTagModel>>(
                ProjectsCountByTagCacheKey,
                async () =>
                    await this.statisticsService
                        .ProjectsCountByTag()
                        .Project()
                        .To<CountByTagModel>()
                        .ToListAsync(),
                this.DefaultAbsoluteExpiration);
        }

        public async Task<IEnumerable<TopProjectResponseModel>> TopProjects()
        {
            return await this.Get<IEnumerable<TopProjectResponseModel>>(
                TopProjectsCacheKey,
                async () =>
                    await this.statisticsService
                        .TopProjects()
                        .Project()
                        .To<TopProjectResponseModel>()
                        .ToListAsync(),
                this.DefaultAbsoluteExpiration);
        }

        public async Task<IEnumerable<TopUserResponseModel>> TopUsers()
        {
            return await this.Get<IEnumerable<TopUserResponseModel>>(
                TopUsersCacheKey,
                async () =>
                    await this.statisticsService
                        .TopUsers()
                        .Project()
                        .To<TopUserResponseModel>()
                        .ToListAsync(),
                this.DefaultAbsoluteExpiration);
        }
    }
}

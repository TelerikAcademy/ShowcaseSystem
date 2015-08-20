namespace Showcase.Server.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.Infrastructure.Caching.Base;
    using Showcase.Services.Data.Contracts;

    public class MemoryCacheService : BaseCacheService, ICacheService
    {
        private const int DefaultAbsoluteExpirationInMinutes = 15;

        private const string LatestProjectCacheKey = "LatestProjects";
        private const string PopularProjectCacheKey = "PopularProjects";
        private const string StatisticsCacheKey = "Statistics";

        private readonly IProjectsService projectsService;
        private readonly IStatisticsService statisticsService;

        public MemoryCacheService(IProjectsService projectsService, IStatisticsService statisticsService)
        {
            this.projectsService = projectsService;
            this.statisticsService = statisticsService;
        }

        public async Task<IEnumerable<ProjectSimpleResponseModel>> LatestProjects()
        {
            return await this.Get<IEnumerable<ProjectSimpleResponseModel>>(
                LatestProjectCacheKey,
                async () =>
                    await this.projectsService
                        .LatestProjects()
                        .Project()
                        .To<ProjectSimpleResponseModel>()
                        .ToListAsync(),
                DateTime.Now.AddMinutes(DefaultAbsoluteExpirationInMinutes));
        }

        public async Task<IEnumerable<ProjectSimpleResponseModel>> PopularProjects()
        {
            return await this.Get<IEnumerable<ProjectSimpleResponseModel>>(
                PopularProjectCacheKey,
                async () =>
                    await this.projectsService
                        .MostPopular()
                        .Project()
                        .To<ProjectSimpleResponseModel>()
                        .ToListAsync(),
                DateTime.Now.AddMinutes(DefaultAbsoluteExpirationInMinutes));
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
                DateTime.Now.AddMinutes(DefaultAbsoluteExpirationInMinutes));
        }
    }
}

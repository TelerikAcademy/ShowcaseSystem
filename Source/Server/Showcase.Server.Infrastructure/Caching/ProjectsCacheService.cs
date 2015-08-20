namespace Showcase.Server.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.Infrastructure.Caching.Base;
    using Showcase.Services.Data.Contracts;

    public class ProjectsCacheService : BaseCacheService, IProjectsCacheService
    {
        private const string LatestProjectCacheKey = "LatestProjects";
        private const string PopularProjectCacheKey = "PopularProjects";

        private readonly IProjectsService projectsService;

        public ProjectsCacheService(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
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
                this.DefaultAbsoluteExpiration);
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
                this.DefaultAbsoluteExpiration);
        }
    }
}

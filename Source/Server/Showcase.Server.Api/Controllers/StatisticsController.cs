namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.Infrastructure.Caching;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class StatisticsController : BaseController
    {
        private readonly IStatisticsCacheService statisticsCacheService;

        public StatisticsController(IStatisticsCacheService statisticsCacheService)
        {
            this.statisticsCacheService = statisticsCacheService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = await this.statisticsCacheService.Statistics();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ProjectsLastSixMonths()
        {
            var model = await this.statisticsCacheService.ProjectsLastSixMonths();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ProjectsCountByTag()
        {
            var model = await this.statisticsCacheService.ProjectsCountByTag();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> TopProjects()
        {
            var model = await this.statisticsCacheService.TopProjects();
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> TopUsers()
        {
            var model = await this.statisticsCacheService.TopUsers();
            return this.Data(model);
        }
    }
}
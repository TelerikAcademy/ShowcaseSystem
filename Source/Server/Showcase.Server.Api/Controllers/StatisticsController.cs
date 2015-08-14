namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;

    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var model = await this.statisticsService
                .Current()
                .Project()
                .To<CurrentStatisticsResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ProjectsLastSixMonths()
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

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ProjectsCountByTag()
        {
            var model = await this.statisticsService
                .ProjectsCountByTag()
                .Project()
                .To<CountByTagModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> TopProjects()
        {
            var model = await this.statisticsService
                .TopProjects()
                .Project()
                .To<TopProjectResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> TopUsers()
        {
            var model = await this.statisticsService
                .TopUsers()
                .Project()
                .To<TopUserResponseModel>()
                .ToListAsync();

            return this.Data(model);
        }
    }
}
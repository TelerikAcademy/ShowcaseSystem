namespace Showcase.Server.Api.Controllers
{
    using System.Web.Http;

    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class StatisticsController : BaseController
    {
        private IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public IHttpActionResult Get()
        {
            // TODO: cache statistics
            return this.Data(this.statisticsService.Current());
        }
    }
}
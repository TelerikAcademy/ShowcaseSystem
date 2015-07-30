namespace Showcase.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.DataTransferModels.Statistics;
    using Showcase.Server.DataTransferModels.User;
    using Showcase.Services.Data.Contracts;

    [RoutePrefix("api/Statistics")]
    public class StatisticsController : ApiController
    {
        private IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public async Task<IHttpActionResult> Get()
        {
            return this.Data(await this.statisticsService.Current());
        }

        [HttpGet]
        [Route("ProjectsLastSixMonths")]
        public async Task<IHttpActionResult> ProjectsLastSixMonths()
        {
            var total = 0;
            var result = new LineChartResponseModel();

            (await this.statisticsService
                .ProjectsLastSixMonths()
                .Select(CountByDateModel.FromProjectGrouping)
                .ToListAsync())
                .ForEach(r =>
                {
                    total += r.Count;
                    result.Values.Add(total);
                    result.Labels.Add(this.IntegerToMonthName(r.Date.Month));
                });

            return this.Data(result);
        }

        [HttpGet]
        [Route("ProjectsCountByTag")]
        public async Task<IHttpActionResult> ProjectsCountByTag()
        {
            var result = await this.statisticsService
                .ProjectsCountByTag()
                .Project()
                .To<CountByTagModel>()
                .ToListAsync();

            return this.Data(result);
        }

        [HttpGet]
        [Route("TopProjects")]
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
        [Route("TopUsers")]
        public async Task<IHttpActionResult> TopUsers()
        {
            var model = await this.statisticsService
                .TopUsers()
                .Project()
                .To<TopUserResponseModel>()
                .OrderByDescending(u => u.LikesCount)
                .ToListAsync();

            return this.Data(model);
        }

        // TODO: move to extension method
        private string IntegerToMonthName(int monthIndex)
        {
            switch (monthIndex)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default:
                    throw new ArgumentException("Not a valid month index");
            }
        }
    }
}
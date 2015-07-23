namespace Showcase.Server.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Server.DataTransferModels.Statistics;
    using System.Collections.Generic;

    public class StatisticsController : ApiController
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

        [HttpGet]
        [Route("api/Statistics/ProjectsLastSixMonths")]
        public IHttpActionResult ProjectsLastSixMonths()
        {
            var total = 0;
            var result = new LineChartResponseModel
            {
                Labels = new List<string>(),
                Values = new List<int>()
            };

            this.statisticsService
                .ProjectsLastSixMonths()
                .ToList()
                .ForEach(r =>
                {
                    total += r.Count;
                    result.Values.Add(total);
                    result.Labels.Add(this.IntegerToMonthName(r.Date.Month));
                });
            
            return this.Data(result);
        }

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
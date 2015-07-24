using Showcase.Server.DataTransferModels.User;

namespace Showcase.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Server.DataTransferModels.Statistics;

    [RoutePrefix("api/Statistics")]
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
        [Route("ProjectsLastSixMonths")]
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

        [HttpGet]
        [Route("ProjectsCountByTag")]
        public IHttpActionResult ProjectsCountByTag()
        {
            var result = this.statisticsService
                .ProjectsCountByTag()
                .ToList();

            return this.Data(result);
        }

        [HttpGet]
        [Route("MostLiked")]
        public IHttpActionResult MostLikedProjects()
        {
            var model = this.statisticsService
                .MostLikedProjects()
                .Project()
                .To<ProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }

        [HttpGet]
        [Route("TopUsers")]
        public IHttpActionResult TopUsers()
        {
            var model = this.statisticsService
                .TopUsers()
                .Project()
                .To<UserResponseModel>()
                .ToList();

            return this.Data(model);
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
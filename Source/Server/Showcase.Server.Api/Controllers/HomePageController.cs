namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Common.Repositories;
    using Showcase.Server.DataTransferModels.HomePage;
    using Showcase.Server.Common;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class HomePageController : ApiController
    {
        private readonly IHomePageService homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            this.homePageService = homePageService;
        }

        public IHttpActionResult Get()
        {
            var model = this.homePageService
                .LatestProjects()
                .Project()
                .To<HomePageProjectResponseModel>()
                .ToList();

            return this.Data(model);
        }
    }
}
namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Data.Common.Repositories;
    using Showcase.Server.ViewModels.HomePage;
    using Showcase.Server.Common;

    public class HomePageController : ApiController
    {
        private readonly IRepository<Project> projects;

        public HomePageController(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public IEnumerable<HomePageProjectViewModel> Get()
        {
            var model = this.projects
                .All()
                .OrderByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount)
                .Project()
                .To<HomePageProjectViewModel>()
                .ToList();

            return model;
        }
    }
}
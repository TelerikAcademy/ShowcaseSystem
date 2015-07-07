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

    public class HomePageController : ApiController
    {
        private readonly IRepository<Project> projects;

        public HomePageController(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public IEnumerable<HomePageProjectViewModel> Get()
        {
            var projectsRepo = new EfGenericRepository<Project>(new ShowcaseDbContext());

            var projects = projectsRepo.All();

            var model = projects
                .Project()
                .To<HomePageProjectViewModel>()
                .ToList();

            return model;
        }
    }
}
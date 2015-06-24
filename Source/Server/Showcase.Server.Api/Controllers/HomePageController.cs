namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Showcase.Server.ViewModels.HomePage;
    using Showcase.Data;
    using Showcase.Data.Models;
    using Showcase.Data.Common.Repositories;

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

            var model = projectsRepo.All()
                .Select(x => new HomePageProjectViewModel
                {
                    Name = x.Title,
                    Author = "Telerik Academy"
                })
                .ToList();

            return model;
            
            //return new List<HomePageProjectViewModel>
            //           {
            //               new HomePageProjectViewModel
            //                   {
            //                       Name = "Telerik Academy Learning System",
            //                       Author = "Telerik Academy"
            //                   },
            //               new HomePageProjectViewModel
            //                   {
            //                       Name = "Telerik Academy Test System",
            //                       Author = "Telerik Academy"
            //                   },
            //               new HomePageProjectViewModel
            //                   {
            //                       Name = "Telerik Academy Showcase System",
            //                       Author = "Telerik Academy"
            //                   },
            //           };
        }
    }
}

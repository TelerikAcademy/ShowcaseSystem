namespace Showcase.Server.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Showcase.Server.ViewModels.HomePage;

    public class HomePageController : ApiController
    {
        public IEnumerable<HomePageProjectViewModel> Get()
        {
            return new List<HomePageProjectViewModel>
                       {
                           new HomePageProjectViewModel
                               {
                                   Name = "Telerik Academy Learning System",
                                   Author = "Telerik Academy"
                               },
                           new HomePageProjectViewModel
                               {
                                   Name = "Telerik Academy Test System",
                                   Author = "Telerik Academy"
                               },
                           new HomePageProjectViewModel
                               {
                                   Name = "Telerik Academy Showcase System",
                                   Author = "Telerik Academy"
                               },
                       };
        }
    }
}

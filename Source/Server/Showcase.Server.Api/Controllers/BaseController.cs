namespace Showcase.Server.Api.Controllers
{
    using System.Web.Http;

    using Showcase.Data.Models;

    public class BaseController : ApiController
    {
        public BaseController()
        {
            this.CurrentUser = new User
            {
                UserName = "SomeUser",
                IsAdmin = true,
            };
        }

        protected User CurrentUser { get; set; }
    }
}
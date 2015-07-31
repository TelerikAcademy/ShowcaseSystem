namespace Showcase.Server.Api.Controllers
{
    using System.Web.OData;

    using Showcase.Data.Models;

    public class BaseODataController : ODataController
    {
        public BaseODataController()
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
namespace Showcase.Server.Api.Controllers.Base
{
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class BaseAuthorizationController : BaseController
    {
        public BaseAuthorizationController(IUsersService usersService)
        {
            this.UsersService = usersService;

            this.CurrentUser = new User
            {
                UserName = "SomeUser",
                IsAdmin = true,
            };
        }

        protected IUsersService UsersService { get; private set; }

        protected User CurrentUser { get; set; }
    }
}
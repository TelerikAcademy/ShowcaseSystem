namespace Showcase.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.User;
    using Showcase.Services.Data.Contracts;

    public class UsersController : BaseController
    {
        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
        [Route("api/users/{username}")]
        public IHttpActionResult Get(string username)
        {
            var model = this.users
                .GetByUsername(username)
                .Project()
                .To<UserResponseModel>()
                .FirstOrDefault();

            if (model != null)
            {
                return this.Data(model);
            }
            else
            {
                return this.Data(false, "The requested user was not found.");
            }
        }
    }
}
﻿namespace Showcase.Server.Api.Controllers
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

    [RoutePrefix("api/Users")]
    public class UsersController : BaseController
    {
        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
        [Route("Profile/{username}")]
        public IHttpActionResult Get(string username)
        {
            var model = this.users
                .GetByUsername(username)
                .Project()
                .To<UserResponseModel>()
                .FirstOrDefault();
<<<<<<< HEAD

=======
            
>>>>>>> 85152f399c6bfcf7aae10aee29f6a7ba909f2593
            if (model != null)
            {
                return this.Data(model);
            }
            else
            {
                return this.Data(false, "The requested user was not found.");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Identity")]
        public IHttpActionResult Identity()
        {
            var model = this.users
                .GetByUsername(this.User.Identity.Name)
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefault();

            return this.Data(model);
        }
    }
}
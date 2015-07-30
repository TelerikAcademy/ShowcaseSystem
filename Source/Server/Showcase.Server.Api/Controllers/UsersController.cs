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

    [RoutePrefix("api/Users")]
    public class UsersController : BaseController
    {
        private const int MinimumCharactersForUsernameSearch = 3;

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
                .ByUsername(username)
                .Project()
                .To<UserResponseModel>()
                .FirstOrDefault();
            
            if (model != null) // TODO: move the checking for null to this.Data 
            {
                return this.Data(model);
            }
            else
            {
                return this.Data(false, "The requested user was not found.");
            }
        }

        [HttpGet]
        [Route("RemoteProfile/{username}")]
        public async Task<IHttpActionResult> RemoteProfile(string username)
        {
            var model = await this.users.ProfileInfo(username);

            if (model != null) // TODO: move the checking for null to this.Data 
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
                .ByUsername(this.User.Identity.Name)
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefault();

            return this.Data(model);
        }

        [Authorize]
        [HttpGet]
        [Route("Search")]
        public async Task<IHttpActionResult> Search(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < MinimumCharactersForUsernameSearch)
            {
                return this.Data(false, string.Format("Username should be at least {0} symbols long", MinimumCharactersForUsernameSearch));
            }

            var model = await this.users.SearchByUsername(username);

            return this.Ok(model.Select(UserAutocompleteResponseModel.FromUserName));
        }
    }
}
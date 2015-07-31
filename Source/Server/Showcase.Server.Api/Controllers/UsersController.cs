namespace Showcase.Server.Api.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.DataTransferModels.User;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class UsersController : BaseController
    {
        private const int MinimumCharactersForUsernameSearch = 3;

        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile(string username)
        {
            var model = await this.users
                .ByUsername(username)
                .Project()
                .To<UserResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoteProfile(string username)
        {
            return this.Data(await this.users.ProfileInfo(username));
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Identity()
        {
            var model = await this.users
                .ByUsername(this.User.Identity.Name)
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Search(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < MinimumCharactersForUsernameSearch)
            {
                return this.Data(false, string.Format("Username should be at least {0} symbols long", MinimumCharactersForUsernameSearch));
            }

            var model = (await this.users.SearchByUsername(username))
                .Select(UserAutocompleteResponseModel.FromUserName);

            return this.Ok(model);
        }
    }
}
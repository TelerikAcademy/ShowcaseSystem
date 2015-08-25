namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.User;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class UsersController : BaseAuthorizationController
    {
        private const int MinimumCharactersForUsernameSearch = 3;

        public UsersController(IUsersService usersService)
            : base(usersService)
        {
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile(string username)
        {
            string visitorUsername = null;
            var isAdmin = false;
            if (this.CurrentUser != null)
            {
                visitorUsername = this.CurrentUser.UserName;
                isAdmin = this.CurrentUser.IsAdmin;
            }

            var model = await this.UsersService
                .ByUsername(username)
                .Project()
                .To<UserResponseModel>(new { username, visitorUsername, isAdmin })
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoteProfile(string username)
        {
            return this.Data(await this.UsersService.ProfileInfo(username));
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Identity()
        {
            var model = await this.UsersService
                .ByUsername(this.CurrentUser.UserName)
                .Project()
                .To<IdentityResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Search(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < MinimumCharactersForUsernameSearch)
            {
                return this.Data(false, string.Format("Username should be at least {0} symbols long", MinimumCharactersForUsernameSearch));
            }

            var model = (await this.UsersService.SearchByUsername(username))
                .Select(UserAutocompleteResponseModel.FromUserName);

            return this.Ok(model);
        }
    }
}
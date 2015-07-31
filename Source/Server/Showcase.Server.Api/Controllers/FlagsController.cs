namespace Showcase.Server.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class FlagsController : BaseAuthorizationController
    {
        private readonly IFlagsService flagsService;

        public FlagsController(IUsersService usersService, IFlagsService flagsService)
            : base(usersService)
        {
            this.flagsService = flagsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Flag(int id)
        {
            var username = this.User.Identity.Name;

            if (await this.flagsService.ProjectIsFlaggedByUser(id, username))
            {
                return this.Data(false, "You can't flag the same project more than once.");
            }

            await this.flagsService.FlagProject(id, username);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Unflag(int id)
        {
            var username = this.User.Identity.Name;

            if (!await this.flagsService.ProjectIsFlaggedByUser(id, username))
            {
                return this.Data(false, "You have not yet flagged this project.");
            }

            await this.flagsService.UnFlagProject(id, username);

            return this.Ok();
        }
    }
}
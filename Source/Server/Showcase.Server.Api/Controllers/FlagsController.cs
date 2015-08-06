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
            if (await this.flagsService.ProjectIsFlaggedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You can't flag the same project more than once.");
            }

            await this.flagsService.FlagProject(id, this.CurrentUser.UserName);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Unflag(int id)
        {
            if (!await this.flagsService.ProjectIsFlaggedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You have not yet flagged this project.");
            }

            await this.flagsService.UnFlagProject(id, this.CurrentUser.UserName);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> FlagComment(int id)
        {
            if (await this.flagsService.CommentIsFlaggedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You can't flag the same comment more than once.");
            }

            await this.flagsService.FlagComment(id, this.CurrentUser.UserName);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> UnFlagComment(int id)
        {
            if (!await this.flagsService.CommentIsFlaggedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You have not yet flagged this comment.");
            }

            await this.flagsService.UnFlagComment(id, this.CurrentUser.UserName);
            return this.Ok();
        }
    }
}
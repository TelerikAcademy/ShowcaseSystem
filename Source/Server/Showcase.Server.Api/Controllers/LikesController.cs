namespace Showcase.Server.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class LikesController : BaseAuthorizationController
    {
        private readonly ILikesService likesService;

        public LikesController(IUsersService usersService, ILikesService likesService)
            : base(usersService)
        {
            this.likesService = likesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Like(int id)
        {
            if (await this.likesService.ProjectIsLikedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You already have liked this project.");
            }

            await this.likesService.LikeProject(id, this.CurrentUser.UserName);
            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Dislike(int id)
        {
            if (!await this.likesService.ProjectIsLikedByUser(id, this.CurrentUser.UserName))
            {
                return this.Data(false, "You have not yet liked this project.");
            }

            await this.likesService.DislikeProject(id, this.CurrentUser.UserName);
            return this.Ok();
        }
    }
}
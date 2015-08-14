namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels.Comment;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Services.Data;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Logic.Contracts;

    public class CommentsController : BaseAuthorizationController
    {
        private readonly ICommentsService commentsService;
        private readonly IMappingService mappingService;

        public CommentsController(IUsersService usersService, ICommentsService commentsService, IMappingService mappingService)
            : base(usersService)
        {
            this.commentsService = commentsService;
            this.mappingService = mappingService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(int id, CommentRequestModel comment)
        {
            var postedComment = await this.commentsService.AddNew(id, comment.CommentText, this.CurrentUser.UserName);

            var model = await this.commentsService
                .CommentById(postedComment.Id)
                .Project()
                .To<CommentResponseModel>()
                .FirstOrDefaultAsync();

            return this.Data(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Edit(CommentRequestModel comment)
        {
            var edittedComment = await this.commentsService.EditComment(comment.Id, comment.CommentText, this.CurrentUser.UserName, this.CurrentUser.IsAdmin);
            var model = this.mappingService.Map<CommentResponseModel>(edittedComment);
            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, int page)
        {
            var projectCommentsCount = await this.commentsService.ProjectCommentsCount(id);
            var lastPage = this.GetLastPage(projectCommentsCount);
            var username = this.User.Identity.IsAuthenticated ? this.CurrentUser.UserName : string.Empty;

            var model = new CommentsPageResponseModel
            {
                Comments = await this.commentsService
                    .ProjectComments(id, page)
                    .Project()
                    .To<CommentResponseModel>(new { username })
                    .ToListAsync(),
                IsLastPage = page == lastPage
            };

            return this.Data(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> CommentsByUser(string username, int page)
        {
            var userCommentsCount = await this.commentsService.UserCommentsCount(username);
            var lastPage = this.GetLastPage(userCommentsCount); // TODO: extract to common logic for last page

            // TODO: Extract to attribute to check valid page if possible
            if (page < 1 || page > lastPage)
            {
                return this.Data(false, Constants.InvalidPageNumber);
            }

            var model = new CommentsPageResponseModel
            {
                Comments = await this.commentsService
                    .UserComments(username, page)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToListAsync(),
                IsLastPage = page == lastPage,
                CurrentPage = page,
                LastPage = lastPage
            };

            return this.Data(model);
        }

        private int GetLastPage(int count)
        {
            var lastPage = count % CommentsService.PageSize == 0 ? count / CommentsService.PageSize : (count / CommentsService.PageSize) + 1;
            return lastPage == 0 ? 1 : lastPage;
        }
    }
}
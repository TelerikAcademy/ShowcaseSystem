namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.Infrastructure.Validation;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Logic.Contracts;

    [RoutePrefix("api/Comments")]
    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;
        private readonly IMappingService mappingService;

        public CommentsController(ICommentsService commentsService, IUsersService usersService, IMappingService mappingService)
        {
            this.commentsService = commentsService;
            this.usersService = usersService;
            this.mappingService = mappingService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(int id, CommentRequestModel comment)
        {
            var username = this.User.Identity.Name;
            var postedComment = await this.commentsService.AddNew(id, comment.CommentText, username);

            var model = await this.commentsService
                .CommentById(postedComment.Id)
                .Project()
                .To<CommentResponseModel>()
                .FirstOrDefaultAsync();
            
            return this.Data(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateModel]
        public async Task<IHttpActionResult> Edit(int id, CommentRequestModel comment)
        {
            var username = this.User.Identity.Name;
            var edittedComment = await this.commentsService.EditComment(id, comment.CommentText, username);
            var model = this.mappingService.Map<CommentResponseModel>(edittedComment);
            return this.Data(model);
        }

        [HttpGet]
        [Route("{id}/{page}")]
        public async Task<IHttpActionResult> Get(int id, int page)
        {
            var projectCommentsCount = await this.commentsService.ProjectCommentsCount(id);
            var lastPage = this.GetLastPage(projectCommentsCount, page);

            var model = new CommentsPageResponseModel
            {
                Comments = await this.commentsService
                    .ProjectComments(id, page)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToListAsync(),
                IsLastPage = page == lastPage
            };

            return this.Data(model);
        }

        [HttpGet]
        [Route("User/{username}/{page}")]
        public async Task<IHttpActionResult> CommentsByUser(string username, int page)
        {
            var userCommentsCount = await this.commentsService.UserCommentsCount(username);
            var lastPage = this.GetLastPage(userCommentsCount, page);

            if (page < 0 || page > lastPage) // TODO: Extract to attribute to check valid page if possible
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

        private int GetLastPage(int count, int page)
        {
            var lastPage = count % CommentsService.PageSize == 0 ? count / CommentsService.PageSize : (count / CommentsService.PageSize) + 1;
            return lastPage == 0 ? 1 : lastPage;
        }
    }
}
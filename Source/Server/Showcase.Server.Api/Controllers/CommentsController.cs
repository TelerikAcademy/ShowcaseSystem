namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Infrastructure.Extensions;
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
        public async Task<IHttpActionResult> Post(int id, CommentRequestModel comment)
        {
            if (comment == null || !this.ModelState.IsValid)
            {
                return this.Data(false, "Invalid data.");
            }

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
        public IHttpActionResult Edit(int id, CommentRequestModel comment)
        {
            if (comment == null || !this.ModelState.IsValid)
            {
                return this.Data(false, "Invalid data.");
            }

            var username = this.User.Identity.Name;

            var edittedComment = this.commentsService.EditComment(id, comment.CommentText, username);

            if (edittedComment == null)
            {
                return this.Data(false, "");
            }

            var model = this.mappingService.Map<CommentResponseModel>(edittedComment);

            return this.Data(model);
        }

        [HttpGet]
        [Route("{id}/{page}")]
        public IHttpActionResult Get(int id, int page)
        {
            var projectCommentsCount = this.commentsService.ProjectCommentsCount(id);
            var lastPage = this.GetLastPage(projectCommentsCount, page);

            var model = new CommentsPageResponseModel
            {
                Comments = this.commentsService
                    .ProjectComments(id, page)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToList(),
                IsLastPage = page == lastPage
            };

            return this.Data(model);
        }

        [HttpGet]
        [Route("User/{username}/{page}")]
        public IHttpActionResult CommentsByUser(string username, int page)
        {
            var userCommentsCount = this.commentsService.UserCommentsCount(username);
            var lastPage = this.GetLastPage(userCommentsCount, page);

            if (page < 0 || page > lastPage)
            {
                return this.Data(false, "There are no more comments to load.");
            }

            var model = new CommentsPageResponseModel
            {
                Comments = this.commentsService
                    .UserComments(username, page)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToList(),
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
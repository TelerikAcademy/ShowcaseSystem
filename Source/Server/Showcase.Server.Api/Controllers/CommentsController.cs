namespace Showcase.Server.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Showcase.Data.Models;
    using Showcase.Server.Api.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    public class CommentsController : ApiController
    {
        private const int PageSize = 5;

        private readonly ICommentsService comments;

        public CommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        [HttpPost]
        public IHttpActionResult Post(int id, CommentRequestModel comment)
        {
            if (comment == null || !this.ModelState.IsValid)
            {
                return this.Data(false, "Invalid data.");
            }

            var username = this.User.Identity.Name;

            var postedComment = this.comments.PostComment(id, comment.CommentText, username);

            var model = Mapper.Map<Comment, CommentResponseModel>(postedComment);
            model.Username = username;

            return this.Data(model);
        }

        [HttpGet]
        [Route("api/Comments/{id}/{page}")]
        public IHttpActionResult Get(int id, int page)
        {
            var projectCommentsCount = this.comments.ProjectCommentsCount(id);
            var lastPage = this.GetLastPage(projectCommentsCount, page);

            var model = new CommentsPageResponseModel
            {
                Comments = this.comments
                    .GetProjectComments(id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToList(),
                IsLastPage = page == lastPage
            };

            return this.Data(model);
        }

        [HttpGet]
        [Route("api/Comments/User/{username}/{page}")]
        public IHttpActionResult CommentsByUser(string username, int page)
        {
            var userCommentsCount = this.comments.UserCommentsCount(username);
            var lastPage = this.GetLastPage(userCommentsCount, page);

            var model = new CommentsPageResponseModel
            {
                Comments = this.comments
                    .GetUserComments(username)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
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
            return count % CommentsController.PageSize == 0 ? count / CommentsController.PageSize : (count / CommentsController.PageSize) + 1;
        }
    }
}
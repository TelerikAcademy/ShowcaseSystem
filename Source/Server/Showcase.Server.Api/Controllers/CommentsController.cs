﻿namespace Showcase.Server.Api.Controllers
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

            return this.Data(model);
        }

        [Route("api/Comments/{id}/{page}")]
        public IHttpActionResult Get(int id, int page)
        {
            var model = new CommentsPageResponseModel
            {
                Comments = this.comments
                    .GetAllComments(id)
                    .Skip(page * PageSize)
                    .Take(PageSize)
                    .Project()
                    .To<CommentResponseModel>()
                    .ToList(),
                IsLastPage = this.comments.GetAllComments(id).Count() <= (page + 1) * PageSize
            };

            return this.Data(model);
        }
    }
}
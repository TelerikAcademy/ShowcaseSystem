namespace Showcase.Services.Data
{
    using System;
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        public const int PageSize = 5;

        private readonly IRepository<Comment> comments;

        private readonly IUsersService users;

        public CommentsService(IRepository<Comment> comments, IUsersService users)
        {
            this.comments = comments;
            this.users = users;
        }
        
        public Comment PostComment(int id, string commentText, string username)
        {
            var userId = this.users.UserIdByUsername(username);

            var comment = new Comment
            {
                CreatedOn = DateTime.Now,
                Content = commentText,
                ProjectId = id,
                UserId = userId
            };

            this.comments.Add(comment);
            this.comments.SaveChanges();

            return comment;
        }

        public IQueryable<Comment> CommentById(int id)
        {
            return this.comments.All().Where(c => c.Id == id);
        }

        public IQueryable<Comment> ProjectComments(int id, int page)
        {
            return this.comments
                .All()
                .Where(c => c.ProjectId == id)
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * CommentsService.PageSize)
                .Take(PageSize);
        }

        public IQueryable<Comment> UserComments(string username, int page)
        {
            return this.comments
                .All()
                .Where(c => c.User.UserName == username)
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * CommentsService.PageSize)
                .Take(PageSize);
        }

        public int UserCommentsCount(string username)
        {
            return this.comments
                .All()
                .Where(c => c.User.UserName == username)
                .Count();
        }
        
        public int ProjectCommentsCount(int id)
        {
            return this.comments
                .All()
                .Where(c => c.ProjectId == id)
                .Count();
        }
    }
}
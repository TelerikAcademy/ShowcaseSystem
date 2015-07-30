﻿namespace Showcase.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

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
        
        public async Task<Comment> AddNew(int id, string commentText, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            var comment = new Comment
            {
                CreatedOn = DateTime.Now,
                Content = commentText,
                ProjectId = id,
                UserId = userId
            };

            this.comments.Add(comment);
            await this.comments.SaveChangesAsync();

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
                .Skip(page * CommentsService.PageSize)
                .Take(PageSize);
        }

        public IQueryable<Comment> UserComments(string username, int page)
        {
            return this.comments
                .All()
                .Where(c => c.User.UserName == username)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(page * CommentsService.PageSize)
                .Take(PageSize);
        }

        public int UserCommentsCount(string username)
        {
            return this.comments
                .All()
                .Where(c => c.User.UserName == username)
                .Count();
        }
        
        public async Task<int> ProjectCommentsCount(int id)
        {
            return await this.comments
                .All()
                .Where(c => c.ProjectId == id)
                .CountAsync();
        }


        public async Task<Comment> EditComment(int id, string commentText, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            var commentToEdit = await this.comments
                .All()
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (commentToEdit == null)
            {
                // TODO: Handle error.
            }

            commentToEdit.Content = commentText;
            await this.comments.SaveChangesAsync();

            return commentToEdit;
        }
    }
}
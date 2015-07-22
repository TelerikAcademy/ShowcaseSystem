﻿namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ICommentsService : IService
    {
        Comment PostComment(int id, string commentText, string username);

        IQueryable<Comment> GetProjectComments(int id, int page);

        int ProjectCommentsCount(int id);

        IQueryable<Comment> GetUserComments(string username, int page);

        int UserCommentsCount(string username);
    }
}
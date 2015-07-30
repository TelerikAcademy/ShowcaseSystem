namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ICommentsService : IService
    {
        IQueryable<Comment> CommentById(int id);

        IQueryable<Comment> ProjectComments(int id, int page);

        IQueryable<Comment> UserComments(string username, int page);

        int ProjectCommentsCount(int id);

        int UserCommentsCount(string username);

        Comment AddNew(int id, string commentText, string username);

        Comment EditComment(int id, string commentText, string username);
    }
}
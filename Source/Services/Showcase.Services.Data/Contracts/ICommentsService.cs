namespace Showcase.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ICommentsService : IService
    {
        IQueryable<Comment> CommentById(int id);

        IQueryable<Comment> ProjectComments(int id, int page);

        IQueryable<Comment> UserComments(string username, int page);

        Task<int> ProjectCommentsCount(int id);

        Task<int> UserCommentsCount(string username);

        Task<Comment> AddNew(int id, string commentText, string username);

        Task<Comment> EditComment(int id, string commentText, string username, bool isAdmin);
    }
}
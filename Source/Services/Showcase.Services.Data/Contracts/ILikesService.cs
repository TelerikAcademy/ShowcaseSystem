namespace Showcase.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ILikesService : IService
    {
        IQueryable<Like> AllLikesForProject(int projectId);

        Task LikeProject(int projectId, string username);

        Task DislikeProject(int projectId, string username);

        Task<bool> ProjectIsLikedByUser(int projectId, string username);
    }
}
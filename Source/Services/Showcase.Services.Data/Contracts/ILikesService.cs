namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ILikesService : IService
    {
        IQueryable<Like> AllLikesForProject(int projectId);

        void LikeProject(int projectId, string username);

        void DislikeProject(int projectId, string username);

        bool ProjectIsLikedByUser(int projectId, string username);
    }
}
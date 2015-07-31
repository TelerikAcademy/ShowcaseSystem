namespace Showcase.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class LikesService : ILikesService
    {
        private readonly IUsersService users;
        private readonly IRepository<Like> likes;

        public LikesService(IRepository<Like> likes, IUsersService users)
        {
            this.likes = likes;
            this.users = users;
        }

        public IQueryable<Like> AllLikesForProject(int projectId)
        {
            return this.likes
                .All()
                .Where(l => l.ProjectId == projectId);
        }

        public async Task LikeProject(int projectId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var like = new Like
                {
                    CreatedOn = DateTime.Now,
                    ProjectId = projectId,
                    UserId = userId
                };

                this.likes.Add(like);
                await this.likes.SaveChangesAsync();
            }
        }

        public async Task DislikeProject(int projectId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var like = await this.likes
                    .All()
                    .Where(l => l.UserId == userId && l.ProjectId == projectId)
                    .FirstOrDefaultAsync();

                this.likes.Delete(like);
                await this.likes.SaveChangesAsync();
            }
        }

        public async Task<bool> ProjectIsLikedByUser(int projectId, string username)
        {
            return await this.AllLikesForProject(projectId)
                .AnyAsync(l => l.User.UserName == username);
        }
    }
}
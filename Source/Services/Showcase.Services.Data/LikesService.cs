namespace Showcase.Services.Data
{
    using System;
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Data.Common.Repositories;
    using Showcase.Services.Data.Contracts;

    public class LikesService : ILikesService
    {
        private IUsersService users;

        private IRepository<Like> likes;

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

        public void LikeProject(int projectId, string username)
        {
            var userId = this.users.GetUserId(username);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var like = new Like
                {
                    CreatedOn = DateTime.Now,
                    ProjectId = projectId,
                    UserId = userId
                };

                this.likes.Add(like);
                this.likes.SaveChanges();
            }
        }

        public void DisLikeProject(int projectId, string username)
        {
            var userId = this.users.GetUserId(username);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var like = this.likes
                    .All()
                    .Where(l => l.UserId == userId && l.ProjectId == projectId)
                    .FirstOrDefault();

                this.likes.Delete(like);
                this.likes.SaveChanges();
            }
        }
    }
}
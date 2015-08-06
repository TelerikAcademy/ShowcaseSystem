namespace Showcase.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;

    public class FlagsService : IFlagsService
    {
        private readonly IRepository<Flag> flags;
        private readonly IRepository<CommentFlag> commentFlags;
        private readonly IRepository<Project> projects;
        private readonly IRepository<Comment> comments;
        private readonly IUsersService users;

        public FlagsService(
            IRepository<Flag> flags, 
            IUsersService users, 
            IRepository<Project> projects,
            IRepository<CommentFlag> commentFlags,
            IRepository<Comment> comments)
        {
            this.flags = flags;
            this.commentFlags = commentFlags;
            this.users = users;
            this.projects = projects;
            this.comments = comments;
        }

        public async Task FlagProject(int projectId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var flag = new Flag
                {
                    CreatedOn = DateTime.Now,
                    ProjectId = projectId,
                    UserId = userId
                };

                this.flags.Add(flag);
                await this.flags.SaveChangesAsync();

                var project = await this.projects
                    .All()
                    .Include(p => p.Flags)
                    .Where(p => p.Id == projectId)
                    .FirstOrDefaultAsync();

                if (project.Flags.Count >= Constants.FlagsNeededToHideProject)
                {
                    project.IsHidden = true;
                    await this.projects.SaveChangesAsync();
                }
            }
        }

        public async Task UnFlagProject(int projectId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var flag = await this.flags
                    .All()
                    .Where(f => f.UserId == userId && f.ProjectId == projectId)
                    .FirstOrDefaultAsync();

                this.flags.Delete(flag);
                await this.flags.SaveChangesAsync();
            }
        }
        
        public async Task<bool> ProjectIsFlaggedByUser(int projectId, string username)
        {
            return await this.users
                .ByUsername(username)
                .AnyAsync(u => u.Flags.Any(f => f.ProjectId == projectId));
        }

        public async Task FlagComment(int commentId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var commentFlag = new CommentFlag
                {
                    CreatedOn = DateTime.Now,
                    CommentId = commentId,
                    UserId = userId
                };

                this.commentFlags.Add(commentFlag);
                await this.commentFlags.SaveChangesAsync();

                var comment = await this.comments
                    .All()
                    .Include(c => c.CommentFlags)
                    .Where(c => c.Id == commentId)
                    .FirstOrDefaultAsync();

                if (comment.CommentFlags.Count >= Constants.FlagsNeededToHideComment)
                {
                    comment.IsHidden = true;
                    await this.comments.SaveChangesAsync();
                }
            }
        }

        public async Task UnFlagComment(int commentId, string username)
        {
            var userId = await this.users.UserIdByUsername(username);

            if (userId != 0)
            {
                var commentFlag = await this.commentFlags
                    .All()
                    .Where(f => f.UserId == userId && f.CommentId == commentId)
                    .FirstOrDefaultAsync();

                this.commentFlags.Delete(commentFlag);
                await this.commentFlags.SaveChangesAsync();
            }
        }

        public async Task<bool> CommentIsFlaggedByUser(int commentId, string username)
        {
            return await this.users
                .ByUsername(username)
                .AnyAsync(u => u.CommentFlags.Any(f => f.CommentId == commentId));
        }
    }
}
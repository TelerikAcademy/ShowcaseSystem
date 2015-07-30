namespace Showcase.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Services.Data.Contracts;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public class FlagsService : IFlagsService
    {
        private readonly IRepository<Flag> flags;
        private readonly IRepository<Project> projects;
        private readonly IUsersService users;

        public FlagsService(IRepository<Flag> flags, IUsersService users, IRepository<Project> projects)
        {
            this.flags = flags;
            this.users = users;
            this.projects = projects;
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
    }
}
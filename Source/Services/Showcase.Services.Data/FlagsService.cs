namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public void FlagProject(int projectId, string username)
        {
            var userId = this.users.GetUserId(username);

            if (userId != 0)
            {
                var flag = new Flag
                {
                    CreatedOn = DateTime.Now,
                    ProjectId = projectId,
                    UserId = userId
                };

                this.flags.Add(flag);
                this.flags.SaveChanges();

                var project = this.projects.All().Where(p => p.Id == projectId).FirstOrDefault();

                if (project.Flags.Count >= Constants.FlagsNeededToHideProject)
                {
                    project.IsHidden = true;
                    this.projects.SaveChanges();
                }
            }
        }

        public void UnFlagProject(int projectId, string username)
        {
            var userId = this.users.GetUserId(username);

            if (userId != 0)
            {
                var flag = this.flags
                    .All()
                    .Where(f => f.UserId == userId && f.ProjectId == projectId)
                    .FirstOrDefault();

                this.flags.Delete(flag);
                this.flags.SaveChanges();
            }
        }


        public bool ProjectIsFlaggedByUser(int projectId, string username)
        {
            return this.users
                .GetByUsername(username)
                .Any(u => u.Flags.Any(f => f.ProjectId == projectId));
        }
    }
}
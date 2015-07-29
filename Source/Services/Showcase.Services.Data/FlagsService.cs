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

    public class FlagsService : IFlagsService
    {
        private readonly IRepository<Flag> flags;
        private readonly IUsersService users;

        public FlagsService(IRepository<Flag> flags, IUsersService users)
        {
            this.flags = flags;
            this.users = users;
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
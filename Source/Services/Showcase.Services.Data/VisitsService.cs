namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Showcase.Services.Data.Contracts;
    using Showcase.Data.Models;
    using Showcase.Data.Common.Repositories;

    public class VisitsService : IVisitsService
    {
        private readonly IUsersService users;

        private readonly IProjectsService projects;

        private readonly IRepository<Visit> visits;

        public VisitsService(IRepository<Visit> visits, IUsersService users, IProjectsService projects)
        {
            this.visits = visits;
            this.users = users;
            this.projects = projects;
        }

        public void VisitProject(int projectId, string username)
        {
            var visit = new Visit
            {
                CreatedOn = DateTime.Now,
                ProjectId = projectId
            };

            var userId = this.users.GetUserId(username);

            if (userId != 0)
            {
                visit.UserId = userId;
            }

            this.visits.Add(visit);
            this.visits.SaveChanges();
        }
    }
}
namespace Showcase.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class VisitsService : IVisitsService
    {
        private readonly IRepository<Visit> visits;

        public VisitsService(IRepository<Visit> visits)
        {
            this.visits = visits;
        }

        public async Task VisitProject(int projectId)
        {
            var visit = new Visit
            {
                CreatedOn = DateTime.Now,
                ProjectId = projectId
            };

            this.visits.Add(visit);
            await this.visits.SaveChangesAsync();
        }
    }
}
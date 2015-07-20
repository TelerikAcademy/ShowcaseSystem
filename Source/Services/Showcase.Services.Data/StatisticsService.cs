namespace Showcase.Services.Data
{
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<Project> projectsRepo;

        public StatisticsService(IRepository<Project> projectsRepo)
        {
            this.projectsRepo = projectsRepo;
        }

        public object Current()
        {
            // TODO: count only approved
            return this.projectsRepo
                .All()
                .GroupBy(pr => 0)
                .Select(gr => new
                {
                    TotalProjects = gr.Count(),
                    TotalViews = gr.Sum(pr => pr.Visits.Count()),
                    TotalComments = gr.Sum(pr => pr.Comments.Count()),
                    TotalLikes = gr.Sum(pr => pr.Likes.Count())
                })
                .FirstOrDefault();
        }
    }
}

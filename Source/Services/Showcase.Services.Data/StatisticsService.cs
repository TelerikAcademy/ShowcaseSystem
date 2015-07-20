namespace Showcase.Services.Data
{
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class StatisticsService : IStatisticsService
    {
        private IRepository<Project> projects;

        public StatisticsService(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public object Current()
        {
            // TODO: count only approved
            return this.projects
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

namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;

    public class StatisticsService : IStatisticsService
    {
        private IRepository<Project> projects;
        private IRepository<Tag> tags;
        private IRepository<User> users;

        public StatisticsService(IRepository<Project> projects, IRepository<Tag> tags, IRepository<User> users)
        {
            this.projects = projects;
            this.tags = tags;
            this.users = users;
        }

        public async Task<object> Current()
        {
            return await this.projects
                .All()
                .GroupBy(pr => 0)
                .Select(gr => new // TODO: create data transfer model
                {
                    TotalProjects = gr.Count(),
                    TotalViews = gr.Sum(pr => pr.Visits.Count()),
                    TotalComments = gr.Sum(pr => pr.Comments.Count()),
                    TotalLikes = gr.Sum(pr => pr.Likes.Count())
                })
                .FirstOrDefaultAsync(); 
        }

        public IQueryable<IGrouping<int, Project>> ProjectsLastSixMonths()
        {
            var todaySixMonthsAgo = DateTime.Now.AddMonths(-6);
            
            return this.projects
                .All()
                .Where(p => p.CreatedOn >= todaySixMonthsAgo)
                .GroupBy(s => s.CreatedOn.Month)
                .OrderBy(gr => gr.Key);
        }
        
        public IQueryable<Tag> ProjectsCountByTag()
        {
            return this.tags
                .All()
                .OrderByDescending(t => t.Projects.Count)
                .Take(Constants.StatisticsTopTagsCount);
        }
        
        public IQueryable<Project> TopProjects()
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.Likes.Count)
                .ThenByDescending(pr => pr.Visits.Count)
                .ThenByDescending(pr => pr.Comments.Count)
                .Take(Constants.StatisticsTopProjectsCount);
        }
        
        public IQueryable<User> TopUsers()
        {
            return this.users
                .All()
                .Take(Constants.StatisticsTopUsersCount);
        }
    }
}
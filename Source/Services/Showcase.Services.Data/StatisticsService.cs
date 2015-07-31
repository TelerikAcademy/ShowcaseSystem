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

        public IQueryable<IGrouping<int, Project>> Current()
        {
            return this.projects
                .All()
                .GroupBy(pr => 0); // Don't ask!
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
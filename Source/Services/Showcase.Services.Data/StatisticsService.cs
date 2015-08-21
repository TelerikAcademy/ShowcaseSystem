namespace Showcase.Services.Data
{
    using System;
    using System.Linq;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;

    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<Project> projects;
        private readonly IRepository<Tag> tags;
        private readonly IRepository<User> users;

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
                .Where(pr => !pr.IsHidden)
                .GroupBy(pr => 0); // Don't ask!
        }

        public IQueryable<IGrouping<int, Project>> ProjectsLastSixMonths()
        {
            var todaySixMonthsAgo = DateTime.Now.AddMonths(-6);
            
            return this.projects
                .All()
                .Where(p => p.CreatedOn >= todaySixMonthsAgo && !p.IsHidden)
                .GroupBy(s => s.CreatedOn.Month)
                .OrderBy(gr => gr.Key);
        }
        
        public IQueryable<Tag> ProjectsCountByTag()
        {
            return this.tags
                .All()
                .Where(t => t.Type != TagType.Season)
                .OrderByDescending(t => t.Projects.Count(p => !p.IsHidden))
                .Take(Constants.StatisticsTopTagsCount);
        }
        
        public IQueryable<Project> TopProjects()
        {
            return this.projects
                .All()
                .Where(p => !p.Collaborators.Any(c => c.IsAdmin) && !p.IsHidden)
                .OrderByDescending(pr => pr.Likes.Count)
                .ThenByDescending(pr => pr.Visits.Count)
                .ThenByDescending(pr => pr.Comments.Count(c => !c.IsHidden))
                .Take(Constants.StatisticsTopProjectsCount);
        }
        
        public IQueryable<User> TopUsers()
        {
            return this.users
                .All()
                .Where(u => u.Projects.Any(pr => !pr.IsHidden) && !u.IsAdmin)
                .OrderByDescending(u => u.Projects.Where(p => !p.IsHidden).Sum(pr => pr.Likes.Count))
                .Take(Constants.StatisticsTopUsersCount);
        }
    }
}
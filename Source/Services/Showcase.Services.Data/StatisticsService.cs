﻿namespace Showcase.Services.Data
{
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;
    using System;
    using Showcase.Server.DataTransferModels.Statistics;
    using System.Collections.Generic;

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

        public object Current()
        {
            // TODO: count only approved, add model
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

        public IQueryable<CountByDateModel> ProjectsLastSixMonths()
        {
            var todaySixMonthsAgo = DateTime.Now.AddMonths(-6);
            
            return this.projects
                .All()
                .Where(p => p.CreatedOn >= todaySixMonthsAgo)
                .GroupBy(s => s.CreatedOn.Month)
                .OrderBy(gr => gr.Key)
                .Select(gr => new CountByDateModel
                {
                    Date = gr.FirstOrDefault().CreatedOn,
                    Count = gr.Count()
                });
        }
        
        public IQueryable<CountByTagModel> ProjectsCountByTag()
        {
            return this.tags
                .All()
                .OrderByDescending(t => t.Projects.Count)
                .Take(6)
                .Select(t => new CountByTagModel
                {
                    Count = t.Projects.Count,
                    Tag = t.Name
                });
        }
        
        public IQueryable<Project> MostLikedProjects()
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.Likes.Count)
                .Take(50);
        }
        
        public IQueryable<User> TopUsers()
        {
            return this.users
                .All()
                .OrderByDescending(u => u.Projects.Sum(p => p.Likes.Count))
                .Take(50);
        }
    }
}
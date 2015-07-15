﻿namespace Showcase.Services.Data
{
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Contracts;

    using System.Linq;

    public class HomePageService : IHomePageService
    {
        private IRepository<Project> projects;

        public HomePageService(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public IQueryable<Project> LatestProjects()
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.CreatedOn)
                .Take(Constants.HomePageLatestProjectsCount);
        }
    }
}

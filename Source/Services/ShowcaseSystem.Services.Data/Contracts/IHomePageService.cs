namespace ShowcaseSystem.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using ShowcaseSystem.Services.Common;

    using System.Linq;

    public interface IHomePageService : IService
    {
        IQueryable<Project> LatestProjects();
    }
}

namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using Showcase.Services.Common;

    using System.Linq;

    public interface IHomePageService : IService
    {
        IQueryable<Project> LatestProjects();
    }
}

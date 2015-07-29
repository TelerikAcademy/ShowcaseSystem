namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IProjectsService : IService
    {
        IQueryable<Project> LatestProjects();

        IQueryable<Project> MostPopular();

        IQueryable<Project> GetProjectById(int id);

        IQueryable<Project> GetLikedByUser(int userId);

        IQueryable<Project> GetProjectsList();

        Project Add(Project project, ICollection<User> collaborators, IEnumerable<Tag> tags, IEnumerable<ProcessedImage> processedImages, string mainImage);
    }
}

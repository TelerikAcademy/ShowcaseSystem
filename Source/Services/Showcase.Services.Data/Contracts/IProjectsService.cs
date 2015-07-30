namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IProjectsService : IService
    {
        IQueryable<Project> LatestProjects();

        IQueryable<Project> MostPopular();

        IQueryable<Project> ProjectById(int id);

        IQueryable<Project> LikedByUser(int userId);

        IQueryable<Project> QueriedProjects();

        Task<Project> AddNew(Project project, ICollection<User> collaborators, IEnumerable<Tag> tags, IEnumerable<ProcessedImage> processedImages, string mainImage);
    }
}

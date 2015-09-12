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

        IQueryable<Project> ProjectById(int id, bool isAdmin = false);

        IQueryable<Project> ProjectByIdWithIncludedCollaboratorsTagsAndImages(int id, bool isAdmin = false);

        IQueryable<Project> LikedByUser(int userId);

        IQueryable<Project> QueriedProjects(bool isAdmin = false, bool includeHidden = false);

        Task<Project> AddNew(
            Project project,
            ICollection<User> collaborators,
            IEnumerable<Tag> tags,
            IEnumerable<ProcessedImage> processedImages,
            string mainImage,
            IEnumerable<File> downloadableFiles);

        Task Edit(
            Project project,
            IEnumerable<User> newCollaborators,
            IEnumerable<User> deletedCollaborators,
            IEnumerable<Tag> requiredTags,
            IEnumerable<Tag> newUserTags,
            IEnumerable<Tag> deletedUserTags,
            IEnumerable<Image> updatedImages,
            string updatedMainImageUrl);

        Task HideProject(int id);

        Task UnhideProject(int id);
    }
}

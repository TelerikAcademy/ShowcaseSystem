﻿namespace Showcase.Services.Data.Contracts
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

        IQueryable<Project> ProjectById(int id, bool isAdmin);

        IQueryable<Project> LikedByUser(int userId);

        IQueryable<Project> QueriedProjects(bool isAdmin, bool includeHidden = false);

        Task<Project> AddNew(
            Project project,
            ICollection<User> collaborators,
            IEnumerable<Tag> tags,
            IEnumerable<ProcessedImage> processedImages,
            string mainImage,
            IEnumerable<File> downloadableFiles);

        Task HideProject(int id);
    }
}

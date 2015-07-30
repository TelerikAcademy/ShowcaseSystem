namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IFileSystemService : IService
    {
        Task SaveImagesToFiles(IEnumerable<ProcessedImage> images);
    }
}

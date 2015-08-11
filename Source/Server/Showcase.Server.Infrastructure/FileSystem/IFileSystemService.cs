namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IFileSystemService : IService
    {
        Task SaveImages(IEnumerable<ProcessedImage> images);

        Task SaveDownloadableFiles(IEnumerable<File> files);
    }
}

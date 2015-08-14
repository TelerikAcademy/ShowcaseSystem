namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    using DownloadableFile = Showcase.Data.Models.File;

    public interface IFileSystemService : IService
    {
        Task SaveImages(IEnumerable<ProcessedImage> images);

        Task SaveDownloadableFiles(IEnumerable<DownloadableFile> files);

        FileStream GetFileStream(string filePath, string fileExtension);
    }
}

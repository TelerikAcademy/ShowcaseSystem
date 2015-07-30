namespace Showcase.Server.Api.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Services.Data.Models;

    public interface IFileSystemService
    {
        Task SaveImagesToFiles(IEnumerable<ProcessedImage> images);
    }
}

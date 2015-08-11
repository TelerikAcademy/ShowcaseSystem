namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Models;

    using DownloadableFile = Showcase.Data.Models.File;

    public class FileSystemService : IFileSystemService
    {
        private const string ImagesServerPath = "~/Images/{0}_{1}.jpg";
        private const string DownloadableFilesServerPath = "~/DownloadableFiles/{0}.{1}";

        public async Task SaveImages(IEnumerable<ProcessedImage> images)
        {
            await images.ForEachAsync(async image =>
            {
                await this.SaveFile(image.ThumbnailContent, string.Format(ImagesServerPath, image.UrlPath, ProcessedImage.ThumbnailImage));
                await this.SaveFile(image.HighResolutionContent, string.Format(ImagesServerPath, image.UrlPath, ProcessedImage.HighResolutionImage));
            });
        }

        public async Task SaveDownloadableFiles(IEnumerable<DownloadableFile> files)
        {
            await files.ForEachAsync(async file =>
            {
                await this.SaveFile(file.Content, string.Format(DownloadableFilesServerPath, file.UrlPath, file.FileExtension));
            });
        }

        private async Task SaveFile(byte[] content, string path)
        {
            await Task.Run(async () =>
            {
                var filePath = HostingEnvironment.MapPath(path);
                var fileInfo = new FileInfo(filePath);
                fileInfo.Directory.Create();
                using (var fileWriter = new FileStream(filePath, FileMode.CreateNew))
                {
                    await fileWriter.WriteAsync(content, 0, content.Length);
                }
            });
        }
    }
}
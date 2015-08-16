namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
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
                await this.SaveFile(file.Content, this.GetDownloadableFilePath(file.UrlPath, file.FileExtension));
            });
        }

        public FileStream GetFileStream(string filePath, string fileExtension)
        {
            var downloadableFilePath = this.GetDownloadableFilePath(filePath, fileExtension);
            var serverFilePath = HostingEnvironment.MapPath(downloadableFilePath);
            return new FileStream(serverFilePath, FileMode.Open);
        }

        private async Task SaveFile(byte[] content, string path)
        {
            await Task.Run(async () =>
            {
                var filePath = HostingEnvironment.MapPath(path);
                //// TODO: filePath can be null
                var fileInfo = new FileInfo(filePath);
                fileInfo.Directory.Create();
                using (var fileWriter = new FileStream(filePath, FileMode.CreateNew))
                {
                    await fileWriter.WriteAsync(content, 0, content.Length);
                }
            });
        }

        private string GetDownloadableFilePath(string filePath, string fileExtension)
        {
            return string.Format(DownloadableFilesServerPath, filePath, fileExtension);
        }
    }
}
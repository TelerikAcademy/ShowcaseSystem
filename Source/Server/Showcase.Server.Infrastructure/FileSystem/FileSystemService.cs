namespace Showcase.Server.Infrastructure.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Models;

    public class FileSystemService : IFileSystemService
    {
        private const string ImagesServerPath = "~/Images/{0}_{1}.jpg";

        public async Task SaveImagesToFiles(IEnumerable<ProcessedImage> images)
        {
            await images.ForEachAsync(async image =>
            {
                await this.SaveImageToFile(image.ThumbnailContent, image.UrlPath, ProcessedImage.ThumbnailImage);
                await this.SaveImageToFile(image.HighResolutionContent, image.UrlPath, ProcessedImage.HighResolutionImage);
            });
        }

        private async Task SaveImageToFile(byte[] imageContent, string path, string resolution)
        {
            await Task.Run(async () =>
            {
                var filePath = HostingEnvironment.MapPath(string.Format(ImagesServerPath, path, resolution));
                var fileInfo = new FileInfo(filePath);
                fileInfo.Directory.Create();
                using (var fileWriter = new FileStream(filePath, FileMode.CreateNew))
                {
                    await fileWriter.WriteAsync(imageContent, 0, imageContent.Length);
                }
            });
        }
    }
}
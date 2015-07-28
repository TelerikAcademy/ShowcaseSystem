namespace Showcase.Server.Api.Infrastructure.FileSystem
{
    using System.IO;
    using System.Web.Hosting;

    public class FileSystemService : IFileSystemService
    {
        private const string ImagesServerPath = "~/Images/{0}_{1}.jpg";

        public void SaveImageToFile(byte[] imageContent, string path, string resolution)
        {
            var filePath = HostingEnvironment.MapPath(string.Format(ImagesServerPath, path, resolution));
            var fileInfo = new FileInfo(filePath);
            fileInfo.Directory.Create();
            File.WriteAllBytes(filePath, imageContent);
        }
    }
}
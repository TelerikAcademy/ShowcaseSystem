namespace Showcase.Server.Api.Infrastructure.FileSystem
{
    public interface IFileSystemService
    {
        public void SaveImageToFile(byte[] imageContent, string path, string resolution);
    }
}

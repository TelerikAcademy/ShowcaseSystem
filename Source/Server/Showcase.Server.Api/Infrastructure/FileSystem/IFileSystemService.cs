namespace Showcase.Server.Api.Infrastructure.FileSystem
{
    public interface IFileSystemService
    {
        void SaveImageToFile(byte[] imageContent, string path, string resolution);
    }
}

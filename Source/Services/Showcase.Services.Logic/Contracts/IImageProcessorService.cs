namespace Showcase.Services.Logic.Contracts
{
    using System.Threading.Tasks;

    using Showcase.Services.Common;

    public interface IImageProcessorService : IService
    {
        Task<byte[]> Resize(byte[] originalImage, int width);
    }
}

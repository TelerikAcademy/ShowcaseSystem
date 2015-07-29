namespace Showcase.Services.Logic.Contracts
{
    using Showcase.Services.Common;

    public interface IImageProcessorService : IService
    {
        byte[] Resize(byte[] originalImage, int width);
    }
}

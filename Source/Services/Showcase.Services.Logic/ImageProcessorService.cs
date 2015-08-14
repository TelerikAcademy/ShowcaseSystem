namespace Showcase.Services.Logic
{
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    using ImageProcessor;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Formats;

    using Showcase.Services.Common;
    using Showcase.Services.Logic.Contracts;

    public class ImageProcessorService : IImageProcessorService
    {
        public Task<byte[]> Resize(byte[] originalImage, int width)
        {
            return Task.Run(() =>
            {
                using (var originalImageStream = new MemoryStream(originalImage))
                {
                    using (var resultImage = new MemoryStream())
                    {
                        using (var imageFactory = new ImageFactory())
                        {
                            var createdImage = imageFactory
                                .Load(originalImageStream);

                            if (createdImage.Image.Width > width)
                            {
                                createdImage = createdImage
                                    .Resize(new ResizeLayer(new Size(width, 0), ResizeMode.Max));
                            }

                            createdImage
                                .Format(new JpegFormat { Quality = Constants.ImageQuality })
                                .Save(resultImage);
                        }

                        return resultImage.GetBuffer();
                    }
                }
            });
        }
    }
}

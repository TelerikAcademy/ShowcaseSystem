namespace Showcase.Services.Logic
{
    using System.Drawing;
    using System.IO;

    using ImageProcessor;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Formats;

    using Showcase.Services.Common;
    using Showcase.Services.Logic.Contracts;

    public class ImageProcessorService : IImageProcessorService
    {
        public byte[] Resize(byte[] originalImage, int width)
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
                                .Resize(new ResizeLayer(new Size(width, 0), resizeMode: ResizeMode.Max));
                        }

                        createdImage = createdImage.Format(new JpegFormat { Quality = Constants.ImageQuality });
                        createdImage.Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }
    }
}

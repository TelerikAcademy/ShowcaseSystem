namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Base;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class ImagesService : FileInfoService, IImagesService
    {
        private readonly IImageProcessorService imageProcessor;

        public ImagesService(IObjectFactory objectFactory, IImageProcessorService imageProcessorService)
            : base(objectFactory)
        {
            this.imageProcessor = imageProcessorService;
        }

        public async Task<IEnumerable<ProcessedImage>> ProcessImages(IEnumerable<RawFile> rawImages)
        {
            var processedImages = await rawImages.ForEachAsync(async rawImage => 
            {
                var image = await this.SaveFileInfo<Image>(rawImage);

                var thumbnailContent = await this.imageProcessor.Resize(rawImage.Content, ProcessedImage.ThumbnailImageWidth);
                var highContent = await this.imageProcessor.Resize(rawImage.Content, ProcessedImage.HighResolutionWidth);

                return ProcessedImage.FromImage(image, thumbnailContent, highContent);
            });

            return processedImages;
        }
    }
}

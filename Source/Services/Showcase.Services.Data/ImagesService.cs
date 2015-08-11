namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class ImagesService : IImagesService
    {
        private readonly IObjectFactory objectFactory;
        private readonly IRepository<Image> images;
        private readonly IImageProcessorService imageProcessor;

        public ImagesService(IObjectFactory objectFactory, IRepository<Image> images, IImageProcessorService imageProcessorService)
        {
            this.objectFactory = objectFactory;
            this.images = images;
            this.imageProcessor = imageProcessorService;
        }

        public async Task<IEnumerable<ProcessedImage>> ProcessImages(IEnumerable<RawFile> rawImages)
        {
            var processedImages = await rawImages.ForEachAsync(async rawImage => 
            {
                var image = new Image { OriginalFileName = rawImage.OriginalFileName, FileExtension = rawImage.FileExtension };
                var imagesContext = this.objectFactory.GetInstance<ShowcaseDbContext>();
                imagesContext.Images.Add(image);
                await imagesContext.SaveChangesAsync();

                image.UrlPath = image.Id.ToUrlPath();
                await imagesContext.SaveChangesAsync();

                var thumbnailContent = await this.imageProcessor.Resize(rawImage.Content, ProcessedImage.ThumbnailImageWidth);
                var highContent = await this.imageProcessor.Resize(rawImage.Content, ProcessedImage.HighResolutionWidth);

                return ProcessedImage.FromImage(image, thumbnailContent, highContent);
            });

            return processedImages;
        }
    }
}

namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;
    using Showcase.Services.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> images;
        private readonly IImageProcessorService imageProcessorService;

        public ImagesService(IRepository<Image> images, IImageProcessorService imageProcessorService)
        {
            this.images = images;
            this.imageProcessorService = imageProcessorService;
        }

        public IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawImage> rawImages)
        {
            var result = new List<ProcessedImage>();

            rawImages.ForEach(rawImage =>
            {
                var image = new Image { OriginalFileName = rawImage.OriginalFileName, FileExtension = rawImage.FileExtension };

                this.images.Add(image);
                this.images.SaveChanges();

                image.UrlPath = this.GenerateImageUrlPath(image.Id);
                this.images.SaveChanges();

                var thumbnailContent = this.imageProcessorService.Resize(rawImage.Content, ProcessedImage.ThumbnailImageWidth);
                var highContent = this.imageProcessorService.Resize(rawImage.Content, ProcessedImage.HighResolutionWidth);

                result.Add(ProcessedImage.FromImage(image, thumbnailContent, highContent));
            });

            return result;
        }

        private string GenerateImageUrlPath(int imageId)
        {
            return string.Format("{0}/{1}", imageId % 1000, imageId.ToMd5Hash());
        }
    }
}

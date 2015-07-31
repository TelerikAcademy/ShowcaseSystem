namespace Showcase.Services.Data.Models
{
    using System;

    using Showcase.Data.Models;
    using Showcase.Services.Logic;
    using Showcase.Services.Logic.Contracts;

    public class ProcessedImage : Image
    {
        private static IMappingService mappingService;

        static ProcessedImage()
        {
            mappingService = ObjectFactory.Get<IMappingService>();
        }

        public const int ThumbnailImageWidth = 260;
        public const string ThumbnailImage = "tmbl";

        public const int HighResolutionWidth = 1360;
        public const string HighResolutionImage = "high";

        public static Func<ProcessedImage, Image> ToImage
        {
            get { return pi => mappingService.Map<Image>(pi); }
        }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }

        public static ProcessedImage FromImage(Image image, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            var result = mappingService.Map<ProcessedImage>(image);
            result.ThumbnailContent = thumbnailContent;
            result.HighResolutionContent = highResolutionContent;
            return result;
        }
    }
}

namespace Showcase.Services.Data.Models
{
    using System;

    using Showcase.Data.Models;
    using Showcase.Services.Logic;
    using Showcase.Services.Logic.Contracts;

    public class ProcessedImage : Image
    {
        public const int ThumbnailImageWidth = 260;
        public const string ThumbnailImage = "tmbl";

        public const int HighResolutionWidth = 1360;
        public const string HighResolutionImage = "high";

        private static readonly IMappingService MappingService;
        
        static ProcessedImage()
        {
            MappingService = ObjectFactory.Get<IMappingService>();
        }

        public static Func<ProcessedImage, Image> ToImage
        {
            get { return pi => MappingService.Map<Image>(pi); }
        }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }

        public static ProcessedImage FromImage(Image image, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            var result = MappingService.Map<ProcessedImage>(image);
            result.ThumbnailContent = thumbnailContent;
            result.HighResolutionContent = highResolutionContent;
            return result;
        }
    }
}

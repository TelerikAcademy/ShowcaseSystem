namespace Showcase.Services.Data.Models
{
    using System;

    using Showcase.Data.Models;

    public class ProcessedImage : Image
    {
        public const int ThumbnailImageWidth = 260;
        public const string ThumbnailImage = "tmbl";
        
        public const int HighResolutionWidth = 1360;
        public const string HighResolutionImage = "high";

        public static Func<ProcessedImage, Image> ToImage
        {
            get
            {
                return pi => new Image
                {
                    Id = pi.Id,
                    OriginalFileName = pi.OriginalFileName,
                    FileExtension = pi.FileExtension,
                    UrlPath = pi.UrlPath
                };
            }
        }

        public static ProcessedImage FromImage(Image image, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            return new ProcessedImage // TODO: move to AutoMapper
            {
                Id = image.Id,
                OriginalFileName = image.OriginalFileName,
                FileExtension = image.FileExtension,
                UrlPath = image.UrlPath,
                ThumbnailContent = thumbnailContent,
                HighResolutionContent = highResolutionContent
            };
        }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }
    }
}

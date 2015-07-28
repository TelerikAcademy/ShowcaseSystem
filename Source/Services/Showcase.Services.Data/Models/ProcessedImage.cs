namespace Showcase.Services.Data.Models
{
    using Showcase.Data.Models;

    public class ProcessedImage : Image
    {
        public const int ThumbnailImageWidth = 260;
        public const int HighResolutionWidth = 1360;

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

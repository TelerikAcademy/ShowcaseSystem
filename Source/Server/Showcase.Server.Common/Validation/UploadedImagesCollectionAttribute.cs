namespace Showcase.Server.Common.Validation
{
    using System.Collections.Generic;

    using Showcase.Server.Common.Validation.Base;

    public class UploadedImagesCollectionAttribute : UploadedFilesCollectionAttribute
    {
        private const int MinImageSize = 30720;

        private readonly List<string> allowedImageExtensions = new List<string> { "jpg", "jpeg", "gif", "png", "bmp" };

        public UploadedImagesCollectionAttribute()
        {
            this.AllowedExtensions = this.allowedImageExtensions;
            this.MinFileSize = MinImageSize;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(
                value,
                image => this.AllowedExtensions.Contains(image.FileExtension.ToLower()),
                image => this.ValidateBase64StringSize(image.Base64Content));
        }
    }
}

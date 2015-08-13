namespace Showcase.Server.Common.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Server.Common.Models;

    public class UploadedImagesCollectionAttribute : ValidationAttribute
    {
        private const int MinImageSize = 51200;
        private const int MaxImageSize = 10485760;

        private readonly List<string> AllowedImageExtensions = new List<string> { "jpg", "jpeg", "gif", "png", "bmp" };

        public override bool IsValid(object value)
        {
            var collectionOfImages = value as ICollection<FileRequestModel>;
            if (collectionOfImages != null)
            {
                foreach (var image in collectionOfImages)
                {
                    if (!this.AllowedImageExtensions.Contains(image.FileExtension.ToLower())) return false;
                    if (!this.ValidateBase64StringSize(image.Base64Content)) return false;
                }

                return true;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, MinImageSize / 1024.0, MaxImageSize / 1024.0 / 1024);
        }

        private bool ValidateBase64StringSize(string content)
        {
            var trimmedContent = content.Trim('=');
            var fileSize = trimmedContent.Length * 3 / 4.0;
            if (fileSize < MinImageSize || fileSize > MaxImageSize)
            {
                return false;
            }

            return true;
        }
    }
}

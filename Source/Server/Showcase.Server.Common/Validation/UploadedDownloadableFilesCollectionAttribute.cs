namespace Showcase.Server.Common.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Server.Common.Validation.Base;
    using Showcase.Services.Common.Extensions;

    public class UploadedDownloadableFilesCollectionAttribute : UploadedFilesCollectionAttribute
    {
        private const char WhiteSpace = ' ';

        private readonly List<string> allowedImageExtensions = new List<string> { "pptx", "ppt", "pdf", "txt", "doc", "docx", "zip", "rar", "7z" };

        public UploadedDownloadableFilesCollectionAttribute()
        {
            this.AllowedExtensions = this.allowedImageExtensions;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(
                value,
                file => this.AllowedExtensions.Contains(file.FileExtension.ToLower()),
                file => this.ValidateBase64StringSize(file.Base64Content),
                file => this.ContainsOnlyEnglishLettersAndWhiteSpace(file.OriginalFileName));
        }

        private bool ContainsOnlyEnglishLettersAndWhiteSpace(string fileName)
        {
            var symbolsWithoutWhiteSpace = string.Join(string.Empty, fileName.Split(new[] { WhiteSpace }, StringSplitOptions.RemoveEmptyEntries));
            return symbolsWithoutWhiteSpace.All(symbol => symbol.IsEnglishLetter());
        }
    }
}

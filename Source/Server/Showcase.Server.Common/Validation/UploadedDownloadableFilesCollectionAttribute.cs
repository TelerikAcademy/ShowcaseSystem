namespace Showcase.Server.Common.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Server.Common.Validation.Base;
    using Showcase.Services.Common.Extensions;

    public class UploadedDownloadableFilesCollectionAttribute : UploadedFilesCollectionAttribute
    {
        private const char WhiteSpace = ' ';

        private readonly List<string> AllowedImageExtensions = new List<string> { "pptx", "ppt", "pdf", "txt", "doc", "docx", "zip", "rar", "7z" };

        public UploadedDownloadableFilesCollectionAttribute()
        {
            this.AllowedExtensions = this.AllowedImageExtensions;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value,
                file => this.AllowedExtensions.Contains(file.FileExtension.ToLower()),
                file => this.ValidateBase64StringSize(file.Base64Content),
                file => this.ContainsOnlyEnglishLettersAndWhiteSpace(file.OriginalFileName));
        }

        private bool ContainsOnlyEnglishLettersAndWhiteSpace(string fileName)
        {
            var symbolsWithoutWhiteSpace = string.Join(string.Empty, fileName.Split(new[] { WhiteSpace }, StringSplitOptions.RemoveEmptyEntries));
            foreach (var symbol in symbolsWithoutWhiteSpace)
            {
                if (!symbol.IsEnglishLetter()) return false;
            }

            return true;
        }
    }
}

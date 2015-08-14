namespace Showcase.Server.Common.Validation.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Showcase.Server.Common.Models;

    public abstract class UploadedFilesCollectionAttribute : ValidationAttribute
    {
        protected UploadedFilesCollectionAttribute()
        {
            this.MinFileSize = 0;
            this.MaxFileSize = Constants.MaxUploadedFileSize;
        }

        protected int MinFileSize { get; set; }

        protected int MaxFileSize { get; set; }

        protected List<string> AllowedExtensions { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.MinFileSize / 1024.0, this.MaxFileSize / 1024.0 / 1024);
        }

        protected bool IsValid(object value, params Func<FileRequestModel, bool>[] validations)
        {
            var collectionOfFiles = value as ICollection<FileRequestModel>;
            if (collectionOfFiles != null)
            {
                return !collectionOfFiles
                    .SelectMany(file => validations, (file, validation) => new { file, validation })
                    .Where(x => !x.validation(x.file))
                    .Select(x => x.file)
                    .Any();
            }

            return true;
        }

        protected bool ValidateBase64StringSize(string content)
        {
            var trimmedContent = content.Trim('=');
            var fileSize = trimmedContent.Length * 3 / 4.0;
            if (fileSize < this.MinFileSize || fileSize > this.MaxFileSize)
            {
                return false;
            }

            return true;
        }
    }
}

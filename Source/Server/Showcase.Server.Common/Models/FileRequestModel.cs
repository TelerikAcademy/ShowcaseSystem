namespace Showcase.Server.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Services.Data.Models;

    public class FileRequestModel
    {
        public static Func<FileRequestModel, RawFile> ToRawFile
        {
            get
            {
                return file => new RawFile
                {
                    OriginalFileName = file.OriginalFileName,
                    FileExtension = file.FileExtension,
                    Content = file.ByteArrayContent
                };
            }
        }

        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        [Required]
        public string Base64Content { get; set; }

        public byte[] ByteArrayContent
        {
            get
            {
                return Convert.FromBase64String(this.Base64Content);
            }
        }
    }
}

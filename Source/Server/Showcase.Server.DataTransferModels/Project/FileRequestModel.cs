namespace Showcase.Server.DataTransferModels.Project
{
    using Showcase.Data.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FileRequestModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxImageOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxImageFileExtensionLength)]
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

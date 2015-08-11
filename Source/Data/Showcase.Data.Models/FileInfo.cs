namespace Showcase.Data.Models
{
    using Showcase.Data.Common;
    using System.ComponentModel.DataAnnotations;

    public abstract class FileInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }
    }
}

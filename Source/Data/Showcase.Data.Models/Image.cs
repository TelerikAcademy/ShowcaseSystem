namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Showcase.Data.Common;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxImageOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        public string UrlPath { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxImageFileExtensionLength)]
        public string FileExtension { get; set; }

        public int? ProjectId { get; set; }

        [InverseProperty("Images")]
        public virtual Project Project { get; set; }
    }
}
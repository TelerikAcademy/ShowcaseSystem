namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalFilename { get; set; }

        [Required]
        public string UrlPath { get; set; }

        [Required]
        public string FileExtension { get; set; }

        public int? ProjectId { get; set; }

        [InverseProperty("Images")]
        public virtual Project Project { get; set; }
    }
}
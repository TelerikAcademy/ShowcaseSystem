namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image
    {
        public int Id { get; set; }

        public string OriginalFilename { get; set; }

        public string Url { get; set; }

        public string FileExtension { get; set; }

        public int? ProjectId { get; set; }

        [InverseProperty("Images")]
        public virtual Project Project { get; set; }
    }
}
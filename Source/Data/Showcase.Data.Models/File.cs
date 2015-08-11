namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Showcase.Data.Common;

    [Table("Files")]
    public class File : FileInfo
    {
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [NotMapped]
        public byte[] Content { get; set; }
    }
}

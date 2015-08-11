namespace Showcase.Data.Models
{
    using Showcase.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Files")]
    public class File : FileInfo
    {
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}

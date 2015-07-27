namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Common;
    
    public class Comment : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinCommentContentLength)]
        [MaxLength(ValidationConstants.MaxCommentContentLength)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
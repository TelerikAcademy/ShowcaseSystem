namespace Showcase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Data.Common.Models;
    
    public class Comment : AuditInfo
    {
        private ICollection<CommentFlag> commentFlags;

        public Comment()
        {
            this.commentFlags = new HashSet<CommentFlag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinCommentContentLength)]
        [MaxLength(ValidationConstants.MaxCommentContentLength)]
        public string Content { get; set; }

        public bool IsHidden { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<CommentFlag> CommentFlags
        {
            get { return this.commentFlags; }
            set { this.commentFlags = value; }
        }
    }
}
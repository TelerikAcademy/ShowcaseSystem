namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;

    public class CommentFlag : AuditInfo
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;
    
    public class Comment : AuditInfo
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
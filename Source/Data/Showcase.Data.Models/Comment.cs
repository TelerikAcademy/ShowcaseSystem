namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;
    
    public class Comment : AuditInfo
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }
    }
}
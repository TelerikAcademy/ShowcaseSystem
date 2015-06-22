namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;
    
    public class Like : AuditInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }
    }
}
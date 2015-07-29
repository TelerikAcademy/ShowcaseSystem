namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;

    public class Flag : AuditInfo
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
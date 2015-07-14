namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;

    public class Visit : AuditInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public string IP { get; set; }

        public virtual Project Project { get; set; }
    }
}
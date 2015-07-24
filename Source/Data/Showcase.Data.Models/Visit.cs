namespace Showcase.Data.Models
{
    using Showcase.Data.Common.Models;

    public class Visit : AuditInfo
    {
        public int Id { get; set; }

        public int ProjectId { get; set; } // TODO: add sessionId or something else

        public virtual Project Project { get; set; }
    }
}
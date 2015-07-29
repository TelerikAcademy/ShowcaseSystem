namespace Showcase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common.Models;
    
    public class Like : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
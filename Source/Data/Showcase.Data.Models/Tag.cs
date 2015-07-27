namespace Showcase.Data.Models
{
    using Showcase.Data.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        private ICollection<Project> projects;

        public Tag()
        {
            this.projects = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinTagNameLength)]
        [MaxLength(ValidationConstants.MaxTagNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ValidationConstants.TagColorLength)]
        [MaxLength(ValidationConstants.TagColorLength)]
        public string ForegroundColor { get; set; }

        [Required]
        [MinLength(ValidationConstants.TagColorLength)]
        [MaxLength(ValidationConstants.TagColorLength)]
        public string BackgroundColor { get; set; }

        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }
    }
}
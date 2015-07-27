﻿namespace Showcase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;

    public class Tag
    {
        private ICollection<Project> projects;

        public Tag()
        {
            this.projects = new HashSet<Project>();
            this.ForegroundColor = "111111"; // TODO: change to correct and move to constant
            this.BackgroundColor = "555555"; // TODO: change to correct and move to constant
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
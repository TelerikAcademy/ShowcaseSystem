namespace Showcase.Data.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<Project> projects;

        public Tag()
        {
            this.projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }
    }
}
namespace Showcase.Data.Models
{
    using System.Collections.Generic;

    using Showcase.Data.Common.Models;

    public class Project : AuditInfo
    {
        public Project()
        {
            this.Visits = new HashSet<Visit>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public int CategoryId { get; set; }

        public int SeasonId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Season Season { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
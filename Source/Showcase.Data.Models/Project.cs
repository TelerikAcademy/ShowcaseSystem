namespace Showcase.Data.Models
{
    using System.Collections.Generic;

    using Showcase.Data.Common.Models;

    public class Project : AuditInfo
    {
        private ICollection<Visit> visits;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Tag> tags;
        private ICollection<Image> images;

        public Project()
        {
            this.visits = new HashSet<Visit>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public int? MainImageId { get; set; }

        public virtual Image MainImage { get; set; }

        public virtual ICollection<Visit> Visits
        {
            get { return this.visits; }
            set { this.visits = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
    }
}
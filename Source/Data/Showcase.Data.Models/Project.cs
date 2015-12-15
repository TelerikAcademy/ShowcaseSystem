namespace Showcase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Data.Common.Models;

    public class Project : AuditInfo
    {
        private ICollection<Visit> visits;
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        private ICollection<File> files;
        private ICollection<User> collaborators;
        private ICollection<Flag> flags;

        public Project()
        {
            this.visits = new HashSet<Visit>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
            this.files = new HashSet<File>();
            this.collaborators = new HashSet<User>();
            this.flags = new HashSet<Flag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinProjectTitleLength)]
        [MaxLength(ValidationConstants.MaxProjectTitleLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinProjectDescriptionLength)]
        [MaxLength(ValidationConstants.MaxProjectDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxProjectUrlLength)]
        public string RepositoryUrl { get; set; }

        [MaxLength(ValidationConstants.MaxProjectUrlLength)]
        public string LiveDemoUrl { get; set; }

        [MinLength(ValidationConstants.MinProjectEmbedVideoSource, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectEmbedVideoSource, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string VideoEmbedSource { get; set; }

        public bool IsHidden { get; set; }

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

        public virtual ICollection<File> Files
        {
            get { return this.files; }
            set { this.files = value; }
        }

        public virtual ICollection<User> Collaborators
        {
            get { return this.collaborators; }
            set { this.collaborators = value; }
        }

        public virtual ICollection<Flag> Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }
    }
}
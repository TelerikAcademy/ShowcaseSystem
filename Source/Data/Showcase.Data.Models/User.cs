namespace Showcase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Showcase.Data.Common;

    public class User
    {
        private ICollection<Project> projects;
        private ICollection<Like> likes;
        private ICollection<Comment> comments;
        private ICollection<Flag> flags;
        private ICollection<CommentFlag> commentFlags;

        public User()
        {
            this.projects = new HashSet<Project>();
            this.likes = new HashSet<Like>();
            this.comments = new HashSet<Comment>();
            this.flags = new HashSet<Flag>();
            this.commentFlags = new HashSet<CommentFlag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxUserUserNameLength)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        public string AvatarUrl { get; set; }

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

        public virtual ICollection<Flag> Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public virtual ICollection<CommentFlag> CommentFlags
        {
            get { return this.commentFlags; }
            set { this.commentFlags = value; }
        }

        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }
    }
}
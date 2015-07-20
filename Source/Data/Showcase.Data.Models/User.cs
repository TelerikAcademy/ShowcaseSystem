namespace Showcase.Data.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
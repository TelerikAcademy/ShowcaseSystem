namespace Showcase.Data.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
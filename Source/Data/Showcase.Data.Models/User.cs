namespace Showcase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User
    {
        public string Id { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid().ToString(); // TODO: change to int
            this.Projects = new HashSet<Project>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
        }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        public string AvatarUrl { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
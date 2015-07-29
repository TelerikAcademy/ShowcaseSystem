namespace Showcase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Showcase.Data.Common;

    public class User
    {
        public User()
        {
            this.Projects = new HashSet<Project>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
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

        public ICollection<Project> Projects { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
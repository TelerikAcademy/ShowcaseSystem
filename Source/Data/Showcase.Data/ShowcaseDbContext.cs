namespace Showcase.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Models;

    public class ShowcaseDbContext : DbContext
    {
        public ShowcaseDbContext()
            : base("name=ShowcaseSystem")
        {
        }

        public virtual IDbSet<Comment> Comments { get; set; }
                       
        public virtual IDbSet<Like> Likes { get; set; }
                       
        public virtual IDbSet<Project> Projects { get; set; }
                       
        public virtual IDbSet<Tag> Tags { get; set; }
                       
        public virtual IDbSet<Visit> Visits { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<User> Users { get; set; }

        public static ShowcaseDbContext Create()
        {
            return new ShowcaseDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
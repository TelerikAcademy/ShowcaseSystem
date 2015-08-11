namespace Showcase.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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

        public virtual IDbSet<File> Files { get; set; }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Flag> Flags { get; set; }

        public virtual IDbSet<CommentFlag> CommentFlags { get; set; }

        public static ShowcaseDbContext Create()
        {
            return new ShowcaseDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentFlag>()
                .HasRequired(c => c.User)
                .WithMany(c => c.CommentFlags)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            var changedAudits = this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in changedAudits)
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
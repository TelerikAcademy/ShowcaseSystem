namespace Showcase.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Showcase.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ShowcaseDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ShowcaseDbContext context)
        {
            this.SeedProjects(context);
        }

        private void SeedProjects(ShowcaseDbContext context)
        {
            if (context.Projects.Any())
            {
                return;
            }

            var image = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a1",
            };

            for (int i = 0; i < 10; i++)
            {
                var project = new Project
                {
                    CreatedOn = DateTime.Now,
                    Title = "Seed Project " + i,
                    Content = "Seed Project " + i + " Content",
                };

                project.Images.Add(image);

                context.Projects.Add(project);

                context.SaveChanges();

                project.MainImage = context.Images.First();

                context.SaveChanges();
            }
        }
    }
}

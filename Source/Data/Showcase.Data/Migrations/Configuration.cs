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
            this.SeedTags(context);
            this.SeedUsers(context);
            this.SeedProjects(context);
        }

        private void SeedTags(ShowcaseDbContext context)
        {
            if (context.Tags.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Name = "Seeded Tag " + i,
                    BackgroundColor = "lightgrey",
                    ForegroundColor = "black"
                };

                context.Tags.Add(tag);
            }

            context.SaveChanges();
        }

        private void SeedUsers(ShowcaseDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                var user = new User
                {
                    Username = "SeededUser" + i
                };

                context.Users.Add(user);
            }

            context.SaveChanges();
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

            var image2 = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a2",
            };

            var image3 = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a3",
            };

            var image4 = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a4",
            };

            var image5 = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a5",
            };

            var image6 = new Image
            {
                OriginalFilename = "Sample image",
                FileExtension = "jpg",
                Url = "/content/epona/images/demo/portfolio/a6",
            };

            var comment = new Comment
            {
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreatedOn = DateTime.Now,
                User = context.Users.FirstOrDefault()
            };

            var comment2 = new Comment
            {
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreatedOn = DateTime.Now,
                User = context.Users.FirstOrDefault()
            };

            var comment3 = new Comment
            {
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreatedOn = DateTime.Now,
                User = context.Users.FirstOrDefault()
            };

            for (int i = 0; i < 10; i++)
            {
                var project = new Project
                {
                    CreatedOn = DateTime.Now,
                    Title = "Seed Project " + i,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                };

                for (int j = 1; j <= 5; j++)
                {
                    project.Collaborators.Add(context.Users.FirstOrDefault(u => u.Id == j));
                    project.Tags.Add(context.Tags.FirstOrDefault(u => u.Id == j));
                }

                if (i == 0)
                {
                    project.Images.Add(image);
                    project.Images.Add(image2);
                    project.Images.Add(image3);
                    project.Images.Add(image4);
                    project.Images.Add(image5);
                    project.Images.Add(image6);

                    project.Comments.Add(comment);
                    project.Comments.Add(comment2);
                    project.Comments.Add(comment3);
                }

                context.Projects.Add(project);
                context.SaveChanges();

                project.MainImage = context.Images.First();

                context.SaveChanges();
            }
        }
    }
}

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
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ShowcaseDbContext context)
        {
            if (context.Seasons.Any())
            {
                return;
            }

            context.Seasons.Add(new Season
                {
                    Name = "Telerik Academy Season 2015/2016"
                });

            context.SaveChanges();

            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.Add(new Category
                {
                    Name = "First seeded Category"
                });
            
            if (context.Projects.Any())
            {
                return;
            }
            
            context.Projects.Add(new Project
            {
                CreatedOn = DateTime.Now,
                Title = "Seed Project 1",
                Content = "Seed Project 1 Content",
                SeasonId = 1,
                CategoryId = 1
            });

            context.Projects.Add(new Project
            {
                CreatedOn = DateTime.Now,
                Title = "Seed Project 2",
                Content = "Seed Project 2 Content",
                SeasonId = 1,
                CategoryId = 1
            });

            context.Projects.Add(new Project
            {
                CreatedOn = DateTime.Now,
                Title = "Seed Project 3",
                Content = "Seed Project 3 Content",
                SeasonId = 1,
                CategoryId = 1
            });

            context.SaveChanges();
        }
    }
}

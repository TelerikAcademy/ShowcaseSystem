namespace Showcase.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ShowcaseDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ShowcaseDbContext context)
        {
            this.SeedTags(context);
        }

        private void SeedTags(ShowcaseDbContext context)
        {
            var languages = new[] { "C#", "JavaScript", "Objective C", "Java", "HTML", "CSS", "XAML", "XML" };
            
            for (int i = 0; i < languages.Length; i++)
            {
                var tag = new Tag
                {
                    Name = languages[i],
                    BackgroundColor = "9933cc",
                    ForegroundColor = "FFFFFF",
                    Type = TagType.Language
                };

                context.Tags.AddOrUpdate(t => t.Name, tag);
            }

            var technologies = new[] { "jQuery", "NodeJs", "AngularJS", "WPF", "ASP.NET", "ASP.NET MVC", "ASP.NET Web API", "ASP.NET SignalR", "ASP.NET Web Forms", "Android", "iOS", "KendoUI" };

            for (int i = 0; i < technologies.Length; i++)
            {
                var tag = new Tag
                {
                    Name = technologies[i],
                    BackgroundColor = "993333",
                    ForegroundColor = "FFFFFF",
                    Type = TagType.Technology
                };

                context.Tags.AddOrUpdate(t => t.Name, tag);
            }

            var seasons = new[] { "2011/2012", "2012/2013", "2013/2014", "2014/2015", "2015/2016", "2016/2017", "2017/2018" };

            for (int i = 0; i < seasons.Length; i++)
            {
                var tag = new Tag
                {
                    Name = "Season " + seasons[i],
                    BackgroundColor = "009999",
                    ForegroundColor = "FFFFFF",
                    Type = TagType.Season
                };

                context.Tags.AddOrUpdate(t => t.Name, tag);
            }

            context.SaveChanges();
        }
    }
}

namespace Showcase.Data.Migrations
{
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
            if (context.Tags.Any())
            {
                return;
            }

            var languages = new[] { "C#", "JavaScript", "Objective C", "Java", "HTML", "CSS", "XAML", "XML" };
            
            foreach (string language in languages)
            {
                context.Tags.AddOrUpdate(t => t.Name, this.NewTag(language, "9933cc", "FFFFFF", TagType.Language));
            }

            var technologies = new[] { "jQuery", "NodeJs", "AngularJS", "WPF", "ASP.NET", "ASP.NET MVC", "ASP.NET Web API", "ASP.NET SignalR", "ASP.NET Web Forms", "Android", "iOS", "KendoUI" };

            foreach (string technology in technologies)
            {
                context.Tags.AddOrUpdate(t => t.Name, this.NewTag(technology, "993333", "FFFFFF", TagType.Technology));
            }

            var seasons = new[] { "2011/2012", "2012/2013", "2013/2014", "2014/2015", "2015/2016", "2016/2017", "2017/2018" };

            foreach (string season in seasons)
            {
                context.Tags.AddOrUpdate(t => t.Name, this.NewTag("Season " + season, "009999", "FFFFFF", TagType.Season));
            }

            context.SaveChanges();
        }

        private Tag NewTag(string name, string backgroundColor, string foregroundColor, TagType tagType)
        {
            return new Tag
            {
                Name = name,
                BackgroundColor = backgroundColor,
                ForegroundColor = foregroundColor,
                Type = tagType
            };
        }
    }
}

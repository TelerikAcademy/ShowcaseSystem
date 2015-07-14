namespace Showcase.Server.ViewModels.HomePage
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    using AutoMapper;

    public class HomePageProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, HomePageProjectViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Author, opt => opt.UseValue<string>("Telerik Academy"));
        }
    }
}

namespace Showcase.Server.ViewModels.HomePage
{
    using System;
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    using AutoMapper;

    public class HomePageProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string MainImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, HomePageProjectViewModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.MainImage.Url + "." + pr.MainImage.FileExtension))
                .ForMember(pr => pr.Author, opt => opt.UseValue<string>("Telerik Academy"));
        }
    }
}

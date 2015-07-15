namespace Showcase.Server.DataTransferModels.HomePage
{
    using System;

    using AutoMapper;

    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class HomePageProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string MainImageUrl { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public string ShortDate
        {
            get
            {
                return this.CreatedOn.ToShortDateString();
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, HomePageProjectResponseModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.Url + "." + pr.MainImage.FileExtension))
                .ForMember(pr => pr.Author, opt => opt.UseValue<string>("Telerik Academy"));
        }
    }
}

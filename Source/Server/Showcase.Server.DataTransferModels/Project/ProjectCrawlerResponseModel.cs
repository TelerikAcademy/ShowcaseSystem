namespace Showcase.Server.DataTransferModels.Project
{
    using AutoMapper;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class ProjectCrawlerResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string HostUrl { get; set; }

        public string MainImageUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectCrawlerResponseModel>()
                 .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.UrlPath));
        }
    }
}

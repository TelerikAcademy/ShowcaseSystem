namespace Showcase.Server.DataTransferModels.ProjectsPage
{
    using System;

    using AutoMapper;

    using Newtonsoft.Json;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class ProjectsPageProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string MainImageURL { get; set; }

        public string ShortDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectsPageProjectResponseModel>()
                .ForMember(pr => pr.Title, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.MainImageURL, opt => opt.MapFrom(pr => pr.MainImage.Url + "." + pr.MainImage.FileExtension));

        }

    }
}

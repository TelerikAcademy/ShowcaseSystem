namespace Showcase.Server.DataTransferModels.Project
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    using AutoMapper;
    using Newtonsoft.Json;

    public class ProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string MainImageUrl { get; set; }

        public string Content { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<string> Collaborators { get; set; }

        public string ShortDate
        {
            get
            {
                return this.CreatedOn.ToShortDateString();
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, ProjectResponseModel>()
                .ForMember(pr => pr.Name, opt => opt.MapFrom(pr => pr.Title))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.Url + "." + pr.MainImage.FileExtension))
                .ForMember(pr => pr.Author, opt => opt.UseValue<string>("Telerik Academy"))
                .ForMember(pr => pr.Collaborators, opt => opt.MapFrom(pr => pr.Collaborators.Select(c => c.Username).OrderBy(c => c)));
        }
    }
}

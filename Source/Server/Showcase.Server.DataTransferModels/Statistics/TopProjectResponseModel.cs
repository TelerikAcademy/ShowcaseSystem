namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Linq;

    using AutoMapper;
    using MissingFeatures;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TopProjectResponseModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string MainImageUrl { get; set; }
        
        public int Likes { get; set; }

        public int Visits { get; set; }

        public int Comments { get; set; }

        public string TitleUrl
        {
            get
            {
                return this.Title.ToUrl();
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, TopProjectResponseModel>()
                .ForMember(pr => pr.Likes, opt => opt.MapFrom(pr => pr.Likes.Count))
                .ForMember(pr => pr.Visits, opt => opt.MapFrom(pr => pr.Visits.Count))
                .ForMember(pr => pr.Comments, opt => opt.MapFrom(pr => pr.Comments.Where(c => !c.IsHidden).Count()))
                .ForMember(pr => pr.MainImageUrl, opt => opt.MapFrom(pr => pr.MainImage.UrlPath));
        }
    }
}
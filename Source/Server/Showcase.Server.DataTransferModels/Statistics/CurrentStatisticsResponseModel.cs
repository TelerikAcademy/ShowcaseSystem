namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class CurrentStatisticsResponseModel : IHaveCustomMappings
    {
        public int TotalProjects { get; set; }

        public int TotalViews { get; set; }

        public int TotalComments { get; set; }

        public int TotalLikes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<IGrouping<int, Project>, CurrentStatisticsResponseModel>()
                .ForMember(st => st.TotalProjects, opt => opt.MapFrom(gr => gr.Count()))
                .ForMember(st => st.TotalViews, opt => opt.MapFrom(gr => gr.Sum(pr => pr.Visits.Count())))
                .ForMember(st => st.TotalComments, opt => opt.MapFrom(gr => gr.Sum(pr => pr.Comments.Where(c => !c.IsHidden).Count())))
                .ForMember(st => st.TotalLikes, opt => opt.MapFrom(gr => gr.Sum(pr => pr.Likes.Count())));
        }
    }
}

namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TopUserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        public int ProjectsCount { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, TopUserResponseModel>()
                .ForMember(u => u.ProjectsCount, opt => opt.MapFrom(u => u.Projects.Count(pr => !pr.IsHidden)))
                .ForMember(u => u.LikesCount, opt => opt.MapFrom(u => ((int?)u.Projects.Where(pr => !pr.IsHidden).Sum(pr => pr.Likes.Count)) ?? 0));
        }
    }
}
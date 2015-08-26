namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class CountByTagModel : IHaveCustomMappings
    {
        public string Tag { get; set; }

        public int TagId { get; set; }

        public int Count { get; set; }

        public bool IsUserSubmitted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Tag, CountByTagModel>()
                .ForMember(m => m.IsUserSubmitted, opt => opt.MapFrom(t => t.Type == TagType.UserSubmitted))
                .ForMember(m => m.Tag, opt => opt.MapFrom(t => t.Name))
                .ForMember(m => m.TagId, opt => opt.MapFrom(t => t.Id))
                .ForMember(m => m.Count, opt => opt.MapFrom(t => t.Projects.Count()));
        }
    }
}
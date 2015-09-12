namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MissingFeatures;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    using Showcase.Server.DataTransferModels.Tag;

    public class EditProjectResponseModel : PostProjectResponseModel, IMapFrom<Project>, IHaveCustomMappings
    {
        public IEnumerable<CollaboratorResponseModel> Collaborators { get; set; }

        public IEnumerable<TagResponseModel> Tags { get; set; }

        public override void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Project, EditProjectResponseModel>()
                .ForMember(pr => pr.TitleUrl, opt => opt.MapFrom(pr => pr.Title.ToUrl()))
                .ForMember(pr => pr.Tags, opt => opt.MapFrom(pr => pr.Tags.OrderBy(t => t.Type)));
            base.CreateMappings(configuration);
        }
    }
}

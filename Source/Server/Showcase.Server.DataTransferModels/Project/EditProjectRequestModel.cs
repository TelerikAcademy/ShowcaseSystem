namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Showcase.Data.Common;
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    using Showcase.Server.Common.Validation;
    using Showcase.Server.DataTransferModels.Tag;

    public class EditProjectRequestModel : BaseProjectRequestModel, IMapFrom<Project>, IHaveCustomMappings, IValidatableObject
    {
        public int Id { get; set; }

        public ICollection<CollaboratorResponseModel> Collaborators { get; set; }

        [Collaborators]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectCollaboratorsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        public string NewCollaborators { get; set; }

        [Collaborators]
        public string DeletedCollaborators { get; set; }

        public ICollection<TagResponseModel> Tags { get; set; }

        public string RequiredTags { get; set; }

        public string NewUserTags { get; set; }

        public string DeletedUserTags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<EditProjectRequestModel, Project>()
                .ForMember(p => p.Collaborators, opt => opt.Ignore())
                .ForMember(p => p.Tags, opt => opt.Ignore());
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var totalCollaborators = 0;
            if (this.Collaborators != null)
            {
                totalCollaborators += this.Collaborators.Count;
            }

            if (!string.IsNullOrWhiteSpace(this.NewCollaborators))
            {
                totalCollaborators += this.NewCollaborators.Split(',').Length;
            }

            if (totalCollaborators < ValidationConstants.TotalMinProjectCollaboratorsLength
                || totalCollaborators > ValidationConstants.MaxProjectCollaboratorsAndTagsLength)
            {
                yield return new ValidationResult(string.Format(
                    "Total project collaborators must be between {0} and {1}.",
                    ValidationConstants.TotalMinProjectCollaboratorsLength,
                    ValidationConstants.MaxProjectCollaboratorsAndTagsLength));
            }
        }
    }
}

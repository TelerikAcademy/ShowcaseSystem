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

        [RequiredTags]
        public string RequiredTags { get; set; }

        [OnlyEnglish]
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
            var totalCollaborators = this.TotalCollaborators();

            if (totalCollaborators < ValidationConstants.TotalMinProjectCollaboratorsAndUserTagsLength
                || ValidationConstants.MaxProjectCollaboratorsAndTagsLength < totalCollaborators)
            {
                yield return new ValidationResult(string.Format(
                    "Total project collaborators must be between {0} and {1}.",
                    ValidationConstants.TotalMinProjectCollaboratorsAndUserTagsLength,
                    ValidationConstants.MaxProjectCollaboratorsAndTagsLength));
            }

            var totalTags = this.TotalTags();

            if (totalTags < ValidationConstants.TotalMinProjectCollaboratorsAndUserTagsLength
                || ValidationConstants.MaxProjectCollaboratorsAndTagsLength < totalTags + this.RequiredTags.Split(',').Length)
            {
                yield return new ValidationResult(string.Format(
                    "Total project tags must be between {0} and {1}.",
                    ValidationConstants.TotalMinProjectCollaboratorsAndUserTagsLength,
                    ValidationConstants.MaxProjectCollaboratorsAndTagsLength));
            }
        }

        private int TotalCollaborators()
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

            return totalCollaborators;
        }

        private int TotalTags()
        {
            var totalTags = 0;
            if (this.Tags != null)
            {
                totalTags += this.Tags.Count;
            }

            if (!string.IsNullOrWhiteSpace(this.RequiredTags))
            {
                totalTags += this.RequiredTags.Split(',').Length;
            }

            if (!string.IsNullOrWhiteSpace(this.NewUserTags))
            {
                totalTags += this.NewUserTags.Split(',').Length;
            }

            return totalTags;
        }
    }
}

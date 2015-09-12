namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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

        [CollectionCount(ValidationConstants.MinProjectImages, ValidationConstants.MaxProjectImages, ErrorMessage = ValidationConstants.ProjectImagesCountErrorMessage)]
        [UpdatedImagesCollection]
        public ICollection<string> UpdatedImageUrls { get; set; }

        [Required(ErrorMessage = ValidationConstants.MainImageErrorMessage)]
        public string UpdatedMainImageUrl { get; set; }

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
                || ValidationConstants.MaxProjectCollaboratorsAndTagsLength < totalTags)
            {
                yield return new ValidationResult(string.Format(
                    "Total project tags must be between {0} and {1}.",
                    ValidationConstants.TotalMinProjectCollaboratorsAndUserTagsLength,
                    ValidationConstants.MaxProjectCollaboratorsAndTagsLength));
            }

            if (!this.UpdatedImageUrls.Contains(this.UpdatedMainImageUrl))
            {
                yield return new ValidationResult("Main image could not be found in updated images.");
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
            var totalTags = new List<string>();
            if (this.Tags != null)
            {
                totalTags.AddRange(this.Tags.Select(t => t.Name));
            }

            if (!string.IsNullOrWhiteSpace(this.RequiredTags))
            {
                totalTags.AddRange(this.RequiredTags.Split(','));
            }

            if (!string.IsNullOrWhiteSpace(this.NewUserTags))
            {
                totalTags.AddRange(this.NewUserTags.Split(','));
            }

            return totalTags.Distinct().Count();
        }
    }
}

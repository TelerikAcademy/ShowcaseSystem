namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Common;
    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Mapping;
    using Showcase.Server.Common.Models;
    using Showcase.Server.Common.Validation;

    public class ProjectRequestModel : BaseProjectRequestModel, IMapFrom<Project>, IHaveCustomMappings, IValidatableObject
    {
        [Collaborators]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectCollaboratorsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        public string Collaborators { get; set; }

        [Required]
        [RequiredTags]
        [OnlyEnglish]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectTagsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        [TagNamesLength(ValidationConstants.MinTagNameLength, ValidationConstants.MaxTagNameLength)]
        public string Tags { get; set; }

        [CollectionCount(ValidationConstants.MinProjectImages, ValidationConstants.MaxProjectImages, ErrorMessage = ValidationConstants.ProjectImagesCountErrorMessage)]
        [NestedObjects]
        [UploadedImagesCollection(ErrorMessage = ValidationConstants.InvalidFileErrorMessage)]
        public ICollection<FileRequestModel> Images { get; set; }

        [CollectionCount(ValidationConstants.MinProjectFiles, ValidationConstants.MaxProjectFiles)]
        [NestedObjects]
        [UploadedDownloadableFilesCollection(ErrorMessage = ValidationConstants.InvalidFileErrorMessage)]
        public ICollection<FileRequestModel> Files { get; set; }

        [Required(ErrorMessage = ValidationConstants.MainImageErrorMessage)]
        public string MainImage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Images.All(i => i.OriginalFileName != this.MainImage))
            {
                yield return new ValidationResult(ValidationConstants.MainImageDoesNotExistErrorMessage);
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ProjectRequestModel, Project>()
                .ForMember(m => m.Collaborators, opt => opt.Ignore())
                .ForMember(m => m.Tags, opt => opt.Ignore())
                .ForMember(m => m.Images, opt => opt.Ignore())
                .ForMember(m => m.MainImage, opt => opt.Ignore())
                .ForMember(m => m.Files, opt => opt.Ignore());
        }
    }
}

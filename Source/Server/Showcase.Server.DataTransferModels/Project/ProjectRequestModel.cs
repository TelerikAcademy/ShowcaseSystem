namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using Showcase.Data.Common;
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    using Showcase.Server.Common.Models;
    using Showcase.Server.Common.Validation;

    public class ProjectRequestModel : IMapFrom<Project>, IHaveCustomMappings, IValidatableObject
    {
        private string liveDemoUrl;

        [Required]
        [MinLength(ValidationConstants.MinProjectTitleLength, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectTitleLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [OnlyEnglish]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinProjectDescriptionLength, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectDescriptionLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [OnlyEnglish]
        public string Description { get; set; }

        [Collaborators]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectCollaboratorsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        public string Collaborators { get; set; }

        [Required]
        [RequiredTags]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectTagsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        [TagNamesLength(ValidationConstants.MinTagNameLength, ValidationConstants.MaxTagNameLength)]
        public string Tags { get; set; }

        [Display(Name = "Repository URL")]
        [Required]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [Url(ErrorMessage = ValidationConstants.UrlErrorMessage)]
        public string RepositoryUrl { get; set; }

        [Display(Name = "Live Demo URL")]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [NullableUrlAttribute(ErrorMessage = ValidationConstants.UrlErrorMessage)]
        public string LiveDemoUrl
        {
            get
            {
                return this.liveDemoUrl;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }

                this.liveDemoUrl = value;
            }
        }

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
            if (!this.Images.Any(i => i.OriginalFileName == this.MainImage))
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

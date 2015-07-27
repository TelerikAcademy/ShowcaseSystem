namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Showcase.Data.Common;
    using Showcase.Server.Common.Validation;

    public class ProjectRequestModel : IValidatableObject
    {
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

        [Required]
        [Collaborators]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectCollaboratorsAndTagsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        public string Collaborators { get; set; }

        [Required]
        [CommaSeparatedCollectionLength(ValidationConstants.MinProjectCollaboratorsAndTagsLength, ValidationConstants.MaxProjectCollaboratorsAndTagsLength)]
        public string Tags { get; set; }

        [Display(Name = "Repository URL")]
        [Required]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [Url(ErrorMessage = ValidationConstants.UrlErrorMessage)]
        public string RepositoryUrl { get; set; }

        [Display(Name = "Live Demo URL")]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [Url(ErrorMessage = ValidationConstants.UrlErrorMessage)]
        public string LiveDemoUrl { get; set; }

        [CollectionLength(ValidationConstants.MinProjectImages, ValidationConstants.MaxProjectImages, ErrorMessage = ValidationConstants.ProjectImagesCountErrorMessage)]
        public ICollection<FileRequestModel> Images { get; set; }

        [Required(ErrorMessage = ValidationConstants.MainImageErrorMessage)]
        public string MainImage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!this.Images.Any(i => i.OriginalFileName == this.MainImage))
            {
                yield return new ValidationResult(ValidationConstants.MainImageDoesNotExistErrorMessage);
            }
        }
    }
}

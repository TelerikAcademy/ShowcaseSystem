namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Server.Common.Validation;

    public class ProjectRequestModel
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
        public string Collaborators { get; set; }

        [Required]
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

        public IEnumerable<FileRequestModel> Images { get; set; }
    }
}

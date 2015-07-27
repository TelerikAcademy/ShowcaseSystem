namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;

    public class ProjectRequestModel
    {
        [Required]
        [MinLength(ValidationConstants.MinProjectTitleLength, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectTitleLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinProjectDescriptionLength, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectDescriptionLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string Description { get; set; }

        [Required]
        public string Collaborators { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string RepositoryUrl { get; set; }

        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string LiveDemoUrl { get; set; }

        public IEnumerable<FileRequestModel> Images { get; set; }
    }
}

namespace Showcase.Server.DataTransferModels.Project
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Validation;

    public class BaseProjectRequestModel
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

        [MinLength(ValidationConstants.MinProjectEmbedVideoSource, ErrorMessage = ValidationConstants.MinLengthErrorMessage)]
        [MaxLength(ValidationConstants.MaxProjectEmbedVideoSource, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        public string VideoEmbedSource { get; set; }
        
        [Display(Name = Constants.RepositoryUrlDisplayName)]
        [Required]
        [MaxLength(ValidationConstants.MaxProjectUrlLength, ErrorMessage = ValidationConstants.MaxLengthErrorMessage)]
        [Url(ErrorMessage = ValidationConstants.UrlErrorMessage)]
        public string RepositoryUrl { get; set; }

        [Display(Name = Constants.LiveDemoUrlDisplayName)]
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
    }
}

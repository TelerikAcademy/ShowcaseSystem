namespace Showcase.Server.DataTransferModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;
    using Showcase.Server.Common;
    using Showcase.Server.Common.Validation;

    public class CommentRequestModel
    {
        public int Id { get; set; }

        [Display(Name = Constants.CommentTextDisplayName)]
        [Required]
        [MinLength(ValidationConstants.MinCommentContentLength)]
        [MaxLength(ValidationConstants.MaxCommentContentLength)]
        [OnlyEnglish]
        public string CommentText { get; set; }
    }
}
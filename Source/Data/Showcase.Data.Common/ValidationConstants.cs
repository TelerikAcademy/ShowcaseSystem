namespace Showcase.Data.Common
{
    public class ValidationConstants
    {
        // User
        public const int MaxUserUserNameLength = 50;

        // Project
        public const int MinProjectTitleLength = 3;
        public const int MaxProjectTitleLength = 20;
        public const int MinProjectDescriptionLength = 100;
        public const int MaxProjectDescriptionLength = 500;
        public const int MaxProjectUrlLength = 200;

        // Comment
        public const int MinCommentContentLength = 10;
        public const int MaxCommentContentLength = 500;

        // Tag
        public const int MinTagNameLength = 2;
        public const int MaxTagNameLength = 20;
        public const int TagColorLength = 6;

        // Image
        public const int MaxImageOriginalFileNameLength = 255;
        public const int MaxImageFileExtensionLength = 4;

        // Error messages
        public const string MinLengthErrorMessage = "The {0} field must be at least {1} characters long";
        public const string MaxLengthErrorMessage = "The {0} field cannot be more than {1} characters long";
    }
}

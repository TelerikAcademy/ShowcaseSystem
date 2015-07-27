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
        public const int MinProjectCollaboratorsAndTagsLength = 2;
        public const int MaxProjectCollaboratorsAndTagsLength = 20;
        public const int MinProjectImages = 1;
        public const int MaxProjectImages = 10;

        // Comment
        public const int MinCommentContentLength = 10;
        public const int MaxCommentContentLength = 500;

        // Tag
        public const int MinTagNameLength = 2;
        public const int MaxTagNameLength = 20;
        public const int TagColorLength = 6;

        // File
        public const int MaxOriginalFileNameLength = 255;
        public const int MaxFileExtensionLength = 4;

        // Error messages
        public const string MinLengthErrorMessage = "The {0} field must be at least {1} characters long";
        public const string MaxLengthErrorMessage = "The {0} field cannot be more than {1} characters long";
        public const string OnlyEnglishErrorMessage = "The {0} field must contain only English letters";
        public const string UrlErrorMessage = "The {0} field is not a valid URL";
        public const string CollaboratorsErrorMessage = "The {0} field contains invalid usernames";
        public const string CommaSeparatedCollectionLengthErrorMessage = "The {0} field must have between {1} and {2} entries";
        public const string MainImageErrorMessage = "You must select main image for your project";
        public const string MainImageDoesNotExistErrorMessage = "Main image does not exists in your uploaded images";
        public const string ProjectImagesCountErrorMessage = "You must select between {0} and {1} images";
    }
}

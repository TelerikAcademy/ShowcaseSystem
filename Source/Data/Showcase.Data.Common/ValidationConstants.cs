namespace Showcase.Data.Common
{
    public class ValidationConstants
    {
        // User
        public const int MaxUserUserNameLength = 50;

        // Project
        public const int MinProjectTitleLength = 2;
        public const int MaxProjectTitleLength = 70;
        public const int MinProjectDescriptionLength = 70;
        public const int MaxProjectDescriptionLength = 2500;
        public const int MaxProjectUrlLength = 200;
        public const int MinProjectCollaboratorsLength = 0;
        public const int MinProjectTagsLength = 4;
        public const int MaxProjectCollaboratorsAndTagsLength = 20;
        public const int TotalMinProjectCollaboratorsAndUserTagsLength = 1;
        public const int MinProjectImages = 3;
        public const int MaxProjectImages = 10;
        public const int MinProjectFiles = 0;
        public const int MaxProjectFiles = 5;
        public const int MaxProjectEmbedVideoSource = 150;
        public const int MinProjectEmbedVideoSource = 10;

        // Comment
        public const int MinCommentContentLength = 2;
        public const int MaxCommentContentLength = 500;

        // Tag
        public const int MinTagNameLength = 2;
        public const int MaxTagNameLength = 30;
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
        public const string InvalidFileErrorMessage = "The {0} contains invalid file extension or size (between {1}KB and {2}MB)";
        public const string MainImageErrorMessage = "You must select main image for your project";
        public const string MainImageDoesNotExistErrorMessage = "You must select main image for your project from the uploaded images";
        public const string ProjectImagesCountErrorMessage = "You must select between {0} and {1} images";
        public const string InvalidUpdatedImagesErrorMessage = "Updated images have invalid URLs.";
        public const string RequiredTagsErrorMessage = "You must select season and at least one used language or technology";
        public const string TagNamesLengthErrorMessage = "Tag names must be between {0} and {1} characters";
    }
}

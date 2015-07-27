namespace Showcase.Data.Common
{
    public class ValidationConstants
    {
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
    }
}

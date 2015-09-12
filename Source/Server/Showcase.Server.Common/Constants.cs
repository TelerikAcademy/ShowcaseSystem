namespace Showcase.Server.Common
{
    public class Constants
    {
        public const string DataTransferModelsAssembly = "Showcase.Server.DataTransferModels";
        public const string InfrastructureAssembly = "Showcase.Server.Infrastructure";
        public const string DataServicesAssembly = "Showcase.Services.Data";
        public const string LogicServicesAssembly = "Showcase.Services.Logic";

        public const string ShortDateFormat = "dd MMM yyyy";
        public const int MaxProjectsPageSize = 128;

        public const int MaxUploadedFileSize = 10485760;

        public const string RequestCannotBeEmpty = "Request cannot be empty";
        public const string RequestedResourceWasNotFound = "The requested resource was not found";
        public const string EditingProjectIsNotAllowed = "You are not allowed to edit this project";
        public const string NotAuthorized = "You are not authorized for this operation";
        public const string InvalidPageNumber = "Invalid page number";

        public const string CommentTextDisplayName = "Comment";
        public const string RepositoryUrlDisplayName = "Repository URL";
        public const string LiveDemoUrlDisplayName = "Live Demo URL";
    }
}
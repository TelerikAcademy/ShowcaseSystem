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
        public const string InvalidPageNumber = "Invalid page number";
    }
}
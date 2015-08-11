namespace Showcase.Server.DataTransferModels.File
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class FileInfoResponseModel : IMapFrom<FileInfo>
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }
    }
}

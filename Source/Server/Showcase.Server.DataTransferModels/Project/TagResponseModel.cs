namespace Showcase.Server.DataTransferModels.Project
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TagResponseModel : IMapFrom<Tag>
    {
        public string Name { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }
    }
}
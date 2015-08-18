namespace Showcase.Server.DataTransferModels.Tag
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TagResponseModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }
    }
}
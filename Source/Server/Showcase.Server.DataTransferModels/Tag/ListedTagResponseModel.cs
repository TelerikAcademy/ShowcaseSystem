namespace Showcase.Server.DataTransferModels.Tag
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class ListedTagResponseModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

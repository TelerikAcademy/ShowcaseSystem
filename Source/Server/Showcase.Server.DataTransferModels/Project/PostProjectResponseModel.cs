namespace Showcase.Server.DataTransferModels.Project
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class PostProjectResponseModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

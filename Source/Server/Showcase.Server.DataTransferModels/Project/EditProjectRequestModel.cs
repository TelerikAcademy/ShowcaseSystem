namespace Showcase.Server.DataTransferModels.Project
{
    using Showcase.Server.Common.Mapping;
    using Showcase.Data.Models;

    public class EditProjectRequestModel : BaseProjectRequestModel, IMapFrom<Project>
    {
        public int Id { get; set; }
    }
}

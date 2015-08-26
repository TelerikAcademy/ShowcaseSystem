namespace Showcase.Server.DataTransferModels.Project
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class CollaboratorResponseModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
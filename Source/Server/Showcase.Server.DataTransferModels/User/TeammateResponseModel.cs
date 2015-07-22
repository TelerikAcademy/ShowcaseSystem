namespace Showcase.Server.DataTransferModels.User
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class TeammateResponseModel : IMapFrom<User>
    {
        public string Username { get; set; }

        public string AvatarUrl { get; set; }
    }
}
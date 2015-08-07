namespace Showcase.Server.DataTransferModels.User
{
    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    
    public class IdentityResponseModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        public string AvatarUrl { get; set; }
    }
}

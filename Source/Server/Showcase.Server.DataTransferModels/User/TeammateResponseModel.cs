namespace Showcase.Server.DataTransferModels.User
{
    using AutoMapper;
    using MissingFeatures;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;
    
    public class CollaboratorResponseModel : IMapFrom<User>
    {
        public string Username { get; set; } // TODO: UserName?

        public string AvatarUrl { get; set; }
    }
}
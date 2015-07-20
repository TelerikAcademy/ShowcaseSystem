namespace Showcase.Server.DataTransferModels.User
{
    using System;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class LikeResponseModel : IMapFrom<Like>
    {
        public int ProjectId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
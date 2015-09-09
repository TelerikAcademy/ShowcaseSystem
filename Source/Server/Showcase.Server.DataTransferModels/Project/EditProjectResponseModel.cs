namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;

    using Showcase.Data.Models;
    using Showcase.Server.Common.Mapping;

    public class EditProjectResponseModel : PostProjectResponseModel, IMapFrom<Project>
    {
        public IEnumerable<CollaboratorResponseModel> Collaborators { get; set; }
    }
}

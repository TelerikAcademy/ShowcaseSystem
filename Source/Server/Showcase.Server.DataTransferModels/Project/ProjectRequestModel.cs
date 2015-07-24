namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;

    public class ProjectRequestModel
    {
        public string Title { get; set; }

        public IEnumerable<FileRequestModel> Images { get; set; }
    }
}

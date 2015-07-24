namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;

    public class ProjectRequestModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Collaborators { get; set; }

        public string Tags { get; set; }

        public string RepositoryUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public IEnumerable<FileRequestModel> Images { get; set; }
    }
}

namespace Showcase.Server.DataTransferModels.Project
{
    using System.Collections.Generic;

    public class CommentsPageResponseModel
    {
        public bool IsLastPage { get; set; }

        public IEnumerable<CommentResponseModel> Comments { get; set; }
    }
}
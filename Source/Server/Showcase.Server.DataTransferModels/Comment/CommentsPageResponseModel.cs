namespace Showcase.Server.DataTransferModels.Comment
{
    using System.Collections.Generic;

    public class CommentsPageResponseModel
    {
        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public bool IsLastPage { get; set; }

        public IEnumerable<CommentResponseModel> Comments { get; set; }
    }
}
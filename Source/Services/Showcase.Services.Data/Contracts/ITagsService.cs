namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ITagsService : IService
    {
        IQueryable<Tag> SearchByName(string name);

        IEnumerable<Tag> GetTags(string tags);
    }
}

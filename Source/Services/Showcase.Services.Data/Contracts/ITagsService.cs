namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Services.Common;
    using Showcase.Data.Models;

    public interface ITagsService : IService
    {
        IQueryable<Tag> SearchByName(string name);
    }
}

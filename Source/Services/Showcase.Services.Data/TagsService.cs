namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tags;

        public TagsService(IRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable<Tag> SearchByName(string name)
        {
            return this.tags
                .All()
                .Where(t => t.Name.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Tag> GetTags(string tags)
        {
            throw new System.NotImplementedException();
        }
    }
}

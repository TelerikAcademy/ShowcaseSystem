namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<IEnumerable<Tag>> TagsFromCommaSeparatedValues(string tags)
        {
            var existingTagIds = new List<int>();
            var newTagNames = new List<string>();

            tags.Split(',')
                .ToList()
                .ForEach(tag => 
                {
                    int tagId;
                    if (int.TryParse(tag, out tagId))
                    {
                        existingTagIds.Add(tagId);
                    }
                    else
                    {
                        newTagNames.Add(tag);
                    }
                });

            var resultTags = await this.tags
                .All()
                .Where(t => existingTagIds.Contains(t.Id))
                .ToListAsync();

            newTagNames.ForEach(tagName => resultTags.Add(new Tag { Name = tagName }));

            return resultTags;
        }
    }
}

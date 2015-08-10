﻿namespace Showcase.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Models;
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

        public async Task<bool> AllRequiredTagsArePresent(IEnumerable<int> tagsIds)
        {
            var exactlyOneSeasonTagIsPresent = await this.tags
                .All()
                .CountAsync(t => t.Type == TagType.Season && tagsIds.Contains(t.Id)) == 1;

            var languageOrTechnologyTagIsPresent = await this.tags
                .All()
                .AnyAsync(t => (t.Type == TagType.Technology || t.Type == TagType.Language) && tagsIds.Contains(t.Id));

            return exactlyOneSeasonTagIsPresent && languageOrTechnologyTagIsPresent;
        }

        public IQueryable<Tag> SeasonTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Season);
        }

        public IQueryable<Tag> LanguageAndTechnologyTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Language || t.Type == TagType.Technology);
        }
    }
}
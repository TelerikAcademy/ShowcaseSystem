namespace Showcase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Common.Models;
    using Showcase.Data.Common.Repositories;
    using Showcase.Data.Models;
    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private const char TagSeparator = ',';

        private readonly IRepository<Tag> tags;

        public TagsService(IRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable<Tag> SearchByName(string name)
        {
            return this.tags
                .All()
                .Where(t => t.Type == TagType.UserSubmitted && t.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task<IEnumerable<Tag>> TagsFromCommaSeparatedValues(string tagsAsCommaSeparatedValues)
        {
            if (string.IsNullOrWhiteSpace(tagsAsCommaSeparatedValues))
            {
                return Enumerable.Empty<Tag>();
            }

            var existingTagIds = new HashSet<int>();
            var tagNames = new HashSet<string>();

            tagsAsCommaSeparatedValues.Split(new[] { TagSeparator }, StringSplitOptions.RemoveEmptyEntries)
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
                        tagNames.Add(tag.ToLower());
                    }
                });

            var resultTags = await this.tags
                .All()
                .Where(t => existingTagIds.Contains(t.Id) || tagNames.Contains(t.Name))
                .ToListAsync();

            (await this.tags
                .All()
                .Where(t => tagNames.Contains(t.Name.ToLower()))
                .Select(t => t.Name.ToLower())
                .ToListAsync())
                .ForEach(t => tagNames.Remove(t));

            tagNames.ForEach(tagName => resultTags.Add(new Tag { Name = tagName, Type = TagType.UserSubmitted }));

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

        public async Task<bool> AllRequiredTagsArePresent(IEnumerable<string> tagNames)
        {
            var tagIds = await this.tags
                .All()
                .Where(t => tagNames.Contains(t.Name))
                .Select(t => t.Id)
                .ToListAsync();

            return await this.AllRequiredTagsArePresent(tagIds);
        }

        public IQueryable<Tag> SeasonTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Season);
        }

        public IQueryable<Tag> LanguageAndTechnologyTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Language || t.Type == TagType.Technology);
        }

        public IQueryable<Tag> LanguageTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Language);
        }

        public IQueryable<Tag> TechnologyTags()
        {
            return this.tags.All().Where(t => t.Type == TagType.Technology);
        }
    }
}
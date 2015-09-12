namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface ITagsService : IService
    {
        IQueryable<Tag> SearchByName(string name);

        Task<IEnumerable<Tag>> TagsFromCommaSeparatedValues(string tagsAsCommaSeparatedValues);

        Task<bool> AllRequiredTagsArePresent(IEnumerable<int> tagsIds);

        Task<bool> AllRequiredTagsArePresent(IEnumerable<string> tagNames);

        IQueryable<Tag> SeasonTags();

        IQueryable<Tag> LanguageAndTechnologyTags();

        IQueryable<Tag> TechnologyTags();

        IQueryable<Tag> LanguageTags();
    }
}
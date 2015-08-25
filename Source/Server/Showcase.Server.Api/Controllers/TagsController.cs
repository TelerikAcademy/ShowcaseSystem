namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Tag;
    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Services.Data.Contracts;

    public class TagsController : BaseController
    {
        private const int MinimumCharactersForNameSearch = 2;

        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Search(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < MinimumCharactersForNameSearch)
            {
                return this.Data(false, string.Format("Name should be at least {0} symbols long", MinimumCharactersForNameSearch));
            }

            var model = await this.tagsService
                .SearchByName(name)
                .Project()
                .To<ListedTagResponseModel>()
                .ToListAsync();

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> AllSeasonTags()
        {
            var model = await this.tagsService
                .SeasonTags()
                .Project()
                .To<ListedTagResponseModel>()
                .ToListAsync();

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> AllLanguageAndTechnologyTags()
        {
            var model = await this.tagsService
                .LanguageAndTechnologyTags()
                .Project()
                .To<ListedTagResponseModel>()
                .ToListAsync();

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> AllLanguageTags()
        {
            var model = await this.tagsService
                .LanguageTags()
                .Project()
                .To<ListedTagResponseModel>()
                .ToListAsync();

            return this.Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> AllTechnologyTags()
        {
            var model = await this.tagsService
                .TechnologyTags()
                .Project()
                .To<ListedTagResponseModel>()
                .ToListAsync();

            return this.Ok(model);
        }
    }
}
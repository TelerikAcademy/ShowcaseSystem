﻿namespace Showcase.Server.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Infrastructure.Extensions;
    using Showcase.Server.DataTransferModels.Tag;
    using Showcase.Services.Data.Contracts;

    [RoutePrefix("api/Tags")]
    public class TagsController : BaseController
    {
        private const int MinimumCharactersForNameSearch = 2;

        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        [Authorize]
        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < MinimumCharactersForNameSearch)
            {
                return this.Data(false, string.Format("Name should be at least {0} symbols long", MinimumCharactersForNameSearch));
            }

            var model = this.tagsService
                .SearchByName(name)
                .Project()
                .To<TagAutocompleteResponseModel>()
                .ToList();

            return this.Ok(model);
        }
    }
}
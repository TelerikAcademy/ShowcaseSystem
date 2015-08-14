namespace Showcase.Server.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;

    using Showcase.Server.Api.Controllers.Base;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    public class CrawlersController : BaseController
    {
        private readonly IProjectsService projectsService;

        public CrawlersController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var model = await this.projectsService
                .ProjectById(id)
                .Project()
                .To<ProjectCrawlerResponseModel>()
                .FirstOrDefaultAsync();

            model.HostUrl = this.Request.RequestUri.Authority;

            return this.Ok(model);
        }
    }
}
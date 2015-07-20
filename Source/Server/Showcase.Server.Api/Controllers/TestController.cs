using System.Web.Http;
namespace Showcase.Server.Api.Controllers
{
    public class TestController : BaseController
    {
        [Authorize]
        public IHttpActionResult Test()
        {
            var user = this.User.Identity.Name;

            return this.Ok(user);
        }
    }
}
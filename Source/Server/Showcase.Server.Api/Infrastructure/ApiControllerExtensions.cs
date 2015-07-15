namespace Showcase.Server.Api.Infrastructure
{
    using Newtonsoft.Json;
    using System.Net;
    using System.Text;
    using System.Web.Http;
    using System.Web.Http.Results;

    public static class ApiControllerExtensions
    {
        public static FormattedContentResult<ResultObject<T>> Data<T>(this ApiController apiController, T data) where T : class
        {
            return new FormattedContentResult<ResultObject<T>>(HttpStatusCode.OK, new ResultObject<T>(data), new BrowserJsonFormatter(), null, apiController);
        }

        public static FormattedContentResult<ResultObject<object>> Data(this ApiController apiController, bool success, string errorMessage, object data = null)
        {
            return new FormattedContentResult<ResultObject<object>>(HttpStatusCode.OK, new ResultObject<object>(success, errorMessage, data), new BrowserJsonFormatter(), null, apiController);
        }
    }
}
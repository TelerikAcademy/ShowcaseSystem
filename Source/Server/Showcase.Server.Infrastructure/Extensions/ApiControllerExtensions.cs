namespace Showcase.Server.Infrastructure.Extensions
{
    using System.Net;
    using System.Text;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Newtonsoft.Json;

    using Showcase.Server.Common;
    using Showcase.Server.Infrastructure.Formatters;

    public static class ApiControllerExtensions
    {
        public static FormattedContentResult<ResultObject> Data(this ApiController apiController, object data)
        {
            if (data == null)
            {
                return apiController.Data(false, Constants.RequestedResourceWasNotFound, data);
            }

            return new FormattedContentResult<ResultObject>(HttpStatusCode.OK, new ResultObject(data), new BrowserJsonFormatter(), null, apiController);
        }

        public static FormattedContentResult<ResultObject> Data(this ApiController apiController, bool success, string errorMessage, object data = null)
        {
            return new FormattedContentResult<ResultObject>(HttpStatusCode.OK, new ResultObject(success, errorMessage, data), new BrowserJsonFormatter(), null, apiController);
        }
    }
}
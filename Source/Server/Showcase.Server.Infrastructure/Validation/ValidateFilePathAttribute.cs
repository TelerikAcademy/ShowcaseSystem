namespace Showcase.Server.Infrastructure.Validation
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Showcase.Services.Common.Extensions;
    using Showcase.Services.Common;

    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateFilePathAttribute : ActionFilterAttribute
    {
        private const string FileNotFoundErrorMessage = "File not found";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var routeData = actionContext.RequestContext.RouteData.Values;

            var folder = int.Parse(routeData.FirstOrDefault(a => a.Key == "folder").Value as string);
            var file = routeData.FirstOrDefault(a => a.Key == "file").Value as string;

            if (string.IsNullOrWhiteSpace(file))
            {
                throw new ArgumentException(FileNotFoundErrorMessage);
            }

            var fileHashLength = Constants.FileHashLength;
            var id = file.Substring(fileHashLength);
            if (id.ToMd5Hash().Substring(0, fileHashLength) != file.Substring(0, fileHashLength))
            {
                throw new InvalidOperationException(FileNotFoundErrorMessage);
            }

            var idAsInt = int.Parse(id);
            if (idAsInt % Constants.SavedFilesSubfoldersCount != folder)
            {
                throw new InvalidOperationException(FileNotFoundErrorMessage);
            }

            actionContext.ActionArguments["file"] = idAsInt;
        }
    }
}

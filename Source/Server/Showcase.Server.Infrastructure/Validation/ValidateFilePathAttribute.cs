namespace Showcase.Server.Infrastructure.Validation
{
    using System;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Showcase.Services.Common;
    using Showcase.Services.Common.Extensions;

    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateFilePathAttribute : ActionFilterAttribute
    {
        private const string FileNotFoundErrorMessage = "File not found";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var routeData = actionContext.RequestContext.RouteData.Values;

            //// TODO: routeData.FirstOrDefault() result can be null
            var folder = int.Parse(routeData.FirstOrDefault(a => a.Key == "folder").Value as string);
            var file = routeData.FirstOrDefault(a => a.Key == "file").Value as string;

            if (string.IsNullOrWhiteSpace(file))
            {
                throw new ArgumentException(FileNotFoundErrorMessage);
            }

            const int FileHashLength = Constants.FileHashLength;
            var id = file.Substring(FileHashLength);
            if (id.ToMd5Hash().Substring(0, FileHashLength) != file.Substring(0, FileHashLength))
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

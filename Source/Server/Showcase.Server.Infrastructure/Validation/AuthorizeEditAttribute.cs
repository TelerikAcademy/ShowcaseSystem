namespace Showcase.Server.Infrastructure.Validation
{
    using System;
    using System.Linq;

    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Ninject;

    using Showcase.Data.Models;
    using Showcase.Server.Common;
    using Showcase.Server.DataTransferModels.Project;
    using Showcase.Services.Data.Contracts;

    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeEditAttribute : ActionFilterAttribute
    {
        [Inject]
        public IUsersService UsersService { private get; set; }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var bindedProject = actionContext.ActionArguments.FirstOrDefault(a => a.Key == "project");
            if (bindedProject.Value == null)
            {
                this.CreateErrorResponseMessage(actionContext, Constants.RequestCannotBeEmpty);
            }

            var project = bindedProject.Value as EditProjectRequestModel;
            var currentIdentity = HttpContext.Current.User;
            if (!currentIdentity.Identity.IsAuthenticated)
            {
                this.CreateErrorResponseMessage(actionContext, Constants.NotAuthorized);
            }

            var userName = currentIdentity.Identity.Name;
            var isAllowed = await this.UsersService.UserIsCollaboratorInProject(project.Id, userName) || await this.UsersService.UserIsAdmin(userName);
            if (!isAllowed)
            {
                this.CreateErrorResponseMessage(actionContext, Constants.EditingProjectIsNotAllowed);
            }
        }

        private void CreateErrorResponseMessage(HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request.CreateResponse(new ResultObject(false, message));
        }
    }
}

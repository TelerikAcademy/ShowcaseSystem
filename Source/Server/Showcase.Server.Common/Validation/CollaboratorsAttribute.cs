namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    using Ninject;

    using Showcase.Data.Common;
    using Showcase.Services.Data.Contracts;
    using System.Threading.Tasks;

    public class CollaboratorsAttribute : ValidationAttribute
    {
        public CollaboratorsAttribute()
        {
            this.ErrorMessage = ValidationConstants.CollaboratorsErrorMessage;
        }

        [Inject]
        public IRemoteDataService RemoteData { private get; set; }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString != null)
            {
                var collaborators = valueAsString.Split(',');
                var task = Task.Run(async () => await this.RemoteData.UsersExist(collaborators));
                return task.Result;
            }

            return true;
        }
    }
}

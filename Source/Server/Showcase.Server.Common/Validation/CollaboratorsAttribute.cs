namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    using Ninject;

    using Showcase.Data.Common;
    using Showcase.Services.Data.Contracts;

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
            var collaborators = (value as string).Split(',');
            return this.RemoteData.UsersExist(collaborators);
        }
    }
}

namespace Showcase.Server.Common.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Ninject;

    using Showcase.Data.Common;
    using Showcase.Services.Data.Contracts;

    public class UpdatedImagesCollectionAttribute : ValidationAttribute
    {
        public UpdatedImagesCollectionAttribute()
        {
            this.ErrorMessage = ValidationConstants.InvalidUpdatedImagesErrorMessage;
        }

        [Inject]
        public IImagesService ImagesService { private get; set; }

        public override bool IsValid(object value)
        {
            var valueAsCollection = value as ICollection<string>;
            if (valueAsCollection != null)
            {
                return Task.Run(async () => await this.ImagesService.ValidateImageUrls(valueAsCollection)).Result;
            }

            return true;
        }
    }
}

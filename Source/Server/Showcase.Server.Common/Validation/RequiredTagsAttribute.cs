namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Ninject;

    using Showcase.Data.Common;
    using Showcase.Services.Data.Contracts;

    public class RequiredTagsAttribute : ValidationAttribute
    {
        public RequiredTagsAttribute()
        {
            this.ErrorMessage = ValidationConstants.RequiredTagsErrorMessage;
        }

        [Inject]
        public ITagsService TagsService { private get; set; }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString != null)
            {
                int currentTagId;
                var separatedValues = valueAsString.Split(',');
                var tagsIds = separatedValues.Where(t => int.TryParse(t, out currentTagId)).Select(int.Parse).ToList();
                Task<bool> task;
                if (tagsIds.Count > 0)
                {
                    task = Task.Run(async () => await this.TagsService.AllRequiredTagsArePresent(tagsIds));
                }
                else
                {
                    task = Task.Run(async () => await this.TagsService.AllRequiredTagsArePresent(separatedValues));
                }

                return task.Result;
            }

            return true;
        }
    }
}

namespace Showcase.Server.Common.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
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
                var tagsIds = valueAsString.Split(',').Where(t => int.TryParse(t, out currentTagId)).Select(t => int.Parse(t)).ToList();
                var task = Task.Run(async () => await this.TagsService.AllRequiredTagsArePresent(tagsIds));
                return task.Result;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            var seasonTags = string.Join(", ", this.TagsService.SeasonTags().Select(t => t.Name));
            var languageAndTechnologyTags = string.Join(", ", this.TagsService.LanguageAndTechnologyTags().Select(t => t.Name));

            return string.Format(this.ErrorMessage, name, seasonTags, languageAndTechnologyTags);
        }
    }
}

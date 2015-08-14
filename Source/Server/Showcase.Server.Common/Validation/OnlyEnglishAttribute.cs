namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Showcase.Data.Common;
    using Showcase.Services.Common.Extensions;

    public class OnlyEnglishAttribute : ValidationAttribute
    {
        public OnlyEnglishAttribute()
        {
            this.ErrorMessage = ValidationConstants.OnlyEnglishErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString != null)
            {
                return valueAsString.All(symbol => !char.IsLetter(symbol) || symbol.IsEnglishLetter());
            }

            return true;
        }
    }
}

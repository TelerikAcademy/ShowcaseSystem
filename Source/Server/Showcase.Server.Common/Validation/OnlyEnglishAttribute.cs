namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;

    public class OnlyEnglishAttribute : ValidationAttribute
    {
        public OnlyEnglishAttribute()
        {
            base.ErrorMessage = ValidationConstants.OnlyEnglishErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString != null)
            {
                foreach (var symbol in valueAsString)
                {
                    if (char.IsLetter(symbol))
                    {
                        if ((symbol < 65 || 90 < symbol) && (symbol < 97 || 122 < symbol))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(base.ErrorMessage, name);
        }
    }
}

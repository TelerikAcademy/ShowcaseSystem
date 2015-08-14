namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    using Showcase.Data.Common;

    public class CommaSeparatedCollectionLengthAttribute : ValidationAttribute
    {
        private readonly int minimumLength;
        private readonly int maximumLength;

        public CommaSeparatedCollectionLengthAttribute(int minimumLength, int maximumLength)
        {
            this.minimumLength = minimumLength;
            this.maximumLength = maximumLength;
            this.ErrorMessage = ValidationConstants.CommaSeparatedCollectionLengthErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString != null)
            {
                var collection = valueAsString.Split(',');
                if (collection.Length < this.minimumLength || this.maximumLength < collection.Length)
                {
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, name, this.minimumLength, this.maximumLength);
        }
    }
}

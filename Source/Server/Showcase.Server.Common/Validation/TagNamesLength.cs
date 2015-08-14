namespace Showcase.Server.Common.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Showcase.Data.Common;

    public class TagNamesLength : ValidationAttribute
    {
        private readonly int minimumLength;
        private readonly int maximumLength;

        public TagNamesLength(int minimumLength, int maximumLength)
        {
            this.minimumLength = minimumLength;
            this.maximumLength = maximumLength;
            this.ErrorMessage = ValidationConstants.TagNamesLengthErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (!string.IsNullOrWhiteSpace(valueAsString))
            {
                int result;
                var tags = valueAsString
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(t => !int.TryParse(t, out result))
                    .ToList();

                return tags.All(tag => this.minimumLength <= tag.Length && tag.Length <= this.maximumLength);
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.minimumLength, this.maximumLength);
        }
    }
}

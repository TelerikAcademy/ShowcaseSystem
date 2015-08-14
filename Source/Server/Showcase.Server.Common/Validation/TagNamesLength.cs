namespace Showcase.Server.Common.Validation
{
    using Showcase.Data.Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TagNamesLength : ValidationAttribute
    {
        private int minimumLength;
        private int maximumLength;

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

                foreach (var tag in tags)
                {
                    if (tag.Length < this.minimumLength || tag.Length > this.maximumLength)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.minimumLength, this.maximumLength);
        }
    }
}

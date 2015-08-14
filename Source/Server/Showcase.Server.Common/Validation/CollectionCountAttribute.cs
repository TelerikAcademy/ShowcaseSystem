namespace Showcase.Server.Common.Validation
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class CollectionCountAttribute : ValidationAttribute
    {
        private readonly int minimumEntries;
        private readonly int maximumEntries;

        public CollectionCountAttribute(int minimumEntries, int maximumEntries)
        {
            this.minimumEntries = minimumEntries;
            this.maximumEntries = maximumEntries;
        }

        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                if (collection.Count < this.minimumEntries || this.maximumEntries < collection.Count)
                {
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.minimumEntries, this.maximumEntries);
        }
    }
}

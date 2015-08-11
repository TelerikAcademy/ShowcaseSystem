namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    public class NullableUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (string.IsNullOrWhiteSpace(valueAsString))
            {
                return true;
            }

            return new UrlAttribute().IsValid(value);
        }
    }
}

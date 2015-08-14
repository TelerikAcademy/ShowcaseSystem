namespace Showcase.Server.Common.Validation
{
    using System.ComponentModel.DataAnnotations;

    public class NullableUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            return string.IsNullOrWhiteSpace(valueAsString) 
                || new UrlAttribute().IsValid(value);
        }
    }
}

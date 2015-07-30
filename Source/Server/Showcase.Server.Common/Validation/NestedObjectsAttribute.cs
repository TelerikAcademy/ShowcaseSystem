namespace Showcase.Server.Common.Validation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class NestedObjectsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = value as IEnumerable;
            if (collection != null)
            {
                var results = new List<ValidationResult>();
                foreach (var nestedObject in collection)
                {
                    Validator.TryValidateObject(nestedObject, new ValidationContext(nestedObject, null, validationContext.Items), results);
                }

                return results.FirstOrDefault() ?? ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}

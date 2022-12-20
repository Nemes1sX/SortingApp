using System.ComponentModel.DataAnnotations;

namespace SortingApp.Models.FormRequest.CustomRules
{
    public class TwoInteger : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var arrayFormRequest = (int[])validationContext.ObjectInstance;

            return (arrayFormRequest.Length >= 2 && arrayFormRequest != null)
                ? ValidationResult.Success
                : new ValidationResult("Array must be at least 2 input");
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Validaciones
{
    public class PrimeraLetraMayusculaAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString())) 
            {
                return ValidationResult.Success;
            }
        }
    }
}

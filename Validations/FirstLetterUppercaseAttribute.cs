using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class FirstLetterUppercaseAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null || string.IsNullOrEmpty(value.ToString()) )
            {
                return ValidationResult.Success;
            }

            var firstletter = value.ToString()[0].ToString();
            if(firstletter != firstletter.ToUpper())
            {
                return new ValidationResult("First letter should be uppercase.");
            }

            return ValidationResult.Success;
        }
    }
}

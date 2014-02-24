using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public static class MyValidationExtension
{
    public static bool Validate(this object obj, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(obj);
            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
}
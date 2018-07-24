using FluentValidation.Results;
using System.Collections.Generic;

namespace App.Extensions
{
    public static class ValidationResultExtensions
    {
        public static string[] GetErrors(this ValidationResult validationResult)
        {
            var result = new List<string>();
            if (validationResult != null && validationResult.Errors != null)
                foreach (var erro in validationResult.Errors)
                    result.Add(erro.ErrorMessage);

            return result.ToArray();
        }
    }
}
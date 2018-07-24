using FluentValidation;
using App.Services.Dtos;

namespace App.Validators
{
    public class AppValidator : AbstractValidator<TodoDto>
    {
        public AppValidator()
        {
            RuleFor(x => x.Text).NotNull().WithMessage("Text field is required");
        }
    }
}
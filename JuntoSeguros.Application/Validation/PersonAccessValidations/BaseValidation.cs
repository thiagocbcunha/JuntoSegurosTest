using FluentValidation;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Validation.PersonAccessValidations;

public class BaseValidation<T> : AbstractValidator<T>
    where T : BaseCommand
{
    public BaseValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .EmailAddress()
            .WithMessage("Email is invalid.");
    }
}

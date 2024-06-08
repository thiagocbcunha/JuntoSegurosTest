using FluentValidation;
using JuntoSeguros.Application.Command.PersonAccessCommands;

namespace JuntoSeguros.Application.Validation.PersonAccessValidations;

public class ChangePasswordValidation : BaseValidation<ChangePasswordCommand>
{
    public ChangePasswordValidation()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)
            .MaximumLength(20)
            .WithMessage("Passoword is invalid.");
    }
}
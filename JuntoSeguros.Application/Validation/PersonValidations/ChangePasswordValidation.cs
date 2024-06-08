using FluentValidation;
using JuntoSeguros.Application.Command.PersonCommands;

namespace JuntoSeguros.Application.Validation.PersonValidations;

public class CreatePersonValidation : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonValidation()
    {
        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .Length(11)
            .Must(BeValidCpf)
            .WithMessage("Document is invalid.");

    }

    private bool BeValidCpf(string cpf)
    {
        int soma;
        int resto;
        string tempCpf;
        string digito;
        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }
}

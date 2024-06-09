using JuntoSeguros.Domain.Enums;

namespace JuntoSeguros.Application.Command.PersonCommands;

public class CreatePersonCommand(string name, string document, DateTime birthDate, GenderEnum gender) : BaseCommand(name, document, birthDate, gender)
{
}